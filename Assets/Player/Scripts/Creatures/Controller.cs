﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]

public class Controller : MonoBehaviour
{
    private Creature _Creature = null;
    public AnimationCurve AgilityToSpeed;
    public Animator _Animator;

    //Parametrization
    float movementSpeed = 0f;
    float floatiness = Defines.Gravity;
    float pivotSpeed = 8;
    float pivotAngleStop = 45;

    bool smooth = true;

    //States... ifs... ~~
    bool grounded = false;
    bool attacking = false;

    //Controller
    Vector3 targetOrientation = Vector3.zero;
    Transform graphic;
    GameObject groundObject { get; set; }

    void Start()
    {
        _Creature = transform.GetComponent<Creature>();
        if(_Creature.IsPlayer())
        {
            InputManager.Instance.registerAxis("Horizontal", OnInputXAxis);
            InputManager.Instance.registerAxis("Vertical", OnInputZAxis);
        }
            
    }

    void RefreshVariables()
    {
        float Multiplier = AgilityToSpeed.Evaluate(_Creature._Stats.Agility.Get01Value());

        movementSpeed = Multiplier * 1.5f;
    }

    void RefreshUI()
    {
        PlayerUI.Instance.Refresh(_Creature);
    }

    void Update()
    {
        if (PauseMenu.isPaused) return;

        RefreshVariables();

        if (_Creature.IsPlayer())
            RefreshUI();

        RefreshAnimation();
    }

    void RefreshAnimation()
    {
        _Animator.SetInteger("Attack", 0);

        if ((Mathf.Abs(rigidbody.velocity.z) > 0.2f) || (Mathf.Abs(rigidbody.velocity.x) > 0.2f) && grounded)
            _Animator.SetBool("StartWalking", true);
        else
            _Animator.SetBool("StartWalking", false);

    }

    void FixedUpdate()
    {
        if (PauseMenu.isPaused) return;

        Vector3 movementForce = Vector3.zero;

        if (grounded)
        {
            // Calculate how fast we should be moving
            if (targetOrientation == Vector3.zero) return;

            RaycastHit hit;
            int layermask = 1 << LayerMask.NameToLayer("Floor");
            if (Physics.Raycast(transform.position, transform.up * -1, out hit, 1000f, layermask))
            {
                Transform rampa;
                if (hit.transform.name.Equals( "Rampa" ) )
                    rampa = hit.transform;
                else
                    rampa = hit.transform.FindChild("Rampa");

                if (rampa != null)
                {
                    int orientation = Vector3.Angle(targetOrientation, rampa.forward) > 90 ? -1 : 1;
                    Quaternion rotate = Quaternion.LookRotation(rampa.forward * -1);
                    targetOrientation = rotate * targetOrientation;
                }
            }

            Vector3 cameraDirection = transform.position - Camera.main.transform.position;
            cameraDirection.y = 0;

            Vector3 forwardDirection = cameraDirection * targetOrientation.z;

            Vector3 strafeDirection = new Vector3(cameraDirection.z, 0, cameraDirection.x * -1) * targetOrientation.x;

            Vector3 finalDirection = forwardDirection + strafeDirection;

            if (!attacking)
                LookAt(finalDirection.normalized);

            //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(targetOrientation), Time.deltaTime);

            float _angle = Vector3.Angle(targetOrientation, transform.forward);

            movementForce = finalDirection.normalized * movementSpeed;

            rigidbody.AddForce(movementForce * 50f, ForceMode.Acceleration);
        }

        rigidbody.AddForce(new Vector3(0, -floatiness * 2, 0) + movementForce, ForceMode.Acceleration);

        targetOrientation = Vector3.zero;
    }

    public void OnInputXAxis(string axe, float value)
    {
        targetOrientation.x = value;
    }

    public void OnInputZAxis(string axe, float value)
    {
        targetOrientation.z = value;
    }

    void LookAt(Vector3 direction, bool forze = false)
    {
        Quaternion rotation = Quaternion.LookRotation(direction);

        if (smooth && !forze)
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.fixedDeltaTime * pivotSpeed); //Look at the rotation smoothly
        else
            transform.rotation = rotation; //Just look at

    }

    void OnCollisionExit(Collision other)
    {
        if (other.transform.gameObject.layer != LayerMask.NameToLayer("Floor")) return;

        grounded = true;
        groundObject = other.gameObject;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.transform.gameObject.layer != LayerMask.NameToLayer("Floor")) return;

        grounded = true;
        groundObject = other.gameObject;
    }

    void OnAttackStart(Vector3 clickPoint)
    {
        attacking = true;
        Vector3 objetiveDirection = clickPoint - transform.position;
        objetiveDirection.y = 0;

        LookAt(objetiveDirection, true);

        //lo de siempre, es para enganchar algo mas cosas al
        //evento de OnAttack. Tambien se llamar a pincho al de _Creature y ya

        //_Creature.OnAttack(objetiveDirection);
        BroadcastMessage("OnAttack", objetiveDirection);
    }

    void OnAttackEnd()
    {
        //Debug.Log("OnAttackEnd");
        attacking = false;
    }
}