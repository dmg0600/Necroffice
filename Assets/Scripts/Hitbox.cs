using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Hitbox : MonoBehaviour
{
    [HideInInspector]
    public Creature Owner = null;

    public int Damage = 1;

    public float Radius
    {
        get
        {
            return transform.localScale.x;
        }

        set
        {
            transform.localScale = Vector3.one * Radius;
        }

    }

    [HideInInspector]
    public float Duration = -1;

    public List<InteractiveObject.Properties> Properties = new List<InteractiveObject.Properties>();


    public void SetVelocity(Vector3 velocity)
    {
        rigidbody.velocity = velocity;
    }

    public void Begin()
    {
        if (Duration > 0)
        {
            Invoke("End", Duration);
        }
    }

    public void End()
    {
        Destroy(gameObject);
    }

    public void OnTriggerEnter(Collider other)
    {
        Life _life = other.GetComponent<Life>();
        if (_life != null)
        {
            Creature _creature = other.GetComponent<Creature>();
            if (_creature != null)
            {
                if (Owner == null)
                    return;

                if (_creature.gameObject == Owner.gameObject)
                    return;
            }
<<<<<<< HEAD
=======

>>>>>>> e4908d6805e42fa21409bfbb55a56bf4e28201fc
            other.SendMessage("OnDamage", this);
        }
    }
}
