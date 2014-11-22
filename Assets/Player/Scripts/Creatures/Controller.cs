﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]

public class Controller : MonoBehaviour
{
    //Parametrization
    float movementSpeed = 1;
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

    void Awake()
    {
        graphic = transform.root.FindChild("Graphic");
    }


    void RefreshVariables()
    {
        speed = stats
    }

    void FixedUpdate()
    {
        if (grounded)
        {
            // Calculate how fast we should be moving
            //Vector3 targetOrientation = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;

            if (targetOrientation == Vector3.zero) return;

            if (!attacking)
                LookAt(targetOrientation);


            //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(targetOrientation), Time.deltaTime);

            float _angle = Vector3.Angle(targetOrientation, transform.forward);
            if (_angle < pivotAngleStop)
                rigidbody.AddForce(targetOrientation * movementSpeed, ForceMode.VelocityChange);
        }

        // We apply gravity manually for more tuning control
        rigidbody.AddForce(new Vector3(0, -floatiness * rigidbody.mass, 0));

        grounded = false;
        targetOrientation = Vector3.zero;
    }

    void OnInputAxis(Vector3 direction)
    {
        targetOrientation = direction;
    }

    public void OnInputMouseClick(object o)
    {
        Vector3 objetive = (Vector3)o;
        if (objetive != null)
            OnAttackStarts(objetive);
    }

    void LookAt(Vector3 direction, bool forze = false)
    {
        Quaternion rotation = Quaternion.LookRotation(direction);

        if (smooth && !forze)
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.fixedDeltaTime * pivotSpeed); //Look at the rotation smoothly
        else
            transform.rotation = rotation; //Just look at

    }

    void OnCollisionStay()
    {
        grounded = true;
    }

    void OnAttackStarts(Vector3 objetive)
    {
        attacking = true;
        Vector3 objetiveDirection = objetive - transform.position;
        objetiveDirection.y = 0;

        LookAt(objetiveDirection, true);

        //<HACK>
        StartCoroutine(Attacking());
        //<//HACK>
    }

    //<HACK>
    IEnumerator Attacking() { yield return new WaitForSeconds(Random.Range(2, 4)); OnAttackEnds(); }
    //<//HACK>

    void OnAttackEnds()
    {
        attacking = false;
    }
}