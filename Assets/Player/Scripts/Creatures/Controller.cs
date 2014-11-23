using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]

public class Controller : MonoBehaviour
{
    public Creature _Creature = null;
    public AnimationCurve AgilityToSpeed;

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
    GameObject groundObject { get; set; }

    void Awake()
    {
        _Creature = transform.root.GetComponent<Creature>();
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
        RefreshUI();
    }

    void FixedUpdate()
    {
        if (PauseMenu.isPaused) return;

        
        
        if (grounded)
        {
            // Calculate how fast we should be moving
            if (targetOrientation == Vector3.zero) return;

            RaycastHit hit;
            int layermask = 1 << LayerMask.NameToLayer("Floor");
            if (Physics.Raycast(transform.position, transform.up * -1, out hit, layermask))
            {
                Transform rampa;
                if (hit.transform.name == "Rampa")
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

            if (!attacking)
                LookAt(targetOrientation);

            //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(targetOrientation), Time.deltaTime);

            float _angle = Vector3.Angle(targetOrientation, transform.forward);

            if (_angle < pivotAngleStop)
            {
                if (_Creature != null && _Creature._Weapon != null && _Creature._Weapon.weaponMode == WeaponMode.AI)
                {
                    Debug.Log("fuerza que le vamos a aplicar = " + targetOrientation);
                }
                rigidbody.AddForce(targetOrientation * movementSpeed, ForceMode.VelocityChange);
            }
                
        }

        // We apply gravity manually for more tuning control
        rigidbody.AddForce(new Vector3(0, -floatiness * rigidbody.mass, 0));

        grounded = false;
        targetOrientation = Vector3.zero;
    }

    public void OnInputAxis(Vector3 direction)
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

    void OnCollisionStay(Collision other)
    {
        grounded = true;
        groundObject = other.gameObject;
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