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
        if (Owner == null) return;
        Life _life = other.GetComponent<Life>();
        if (_life != null)
        {
            Creature _creature = other.GetComponent<Creature>();
            if (_creature != null)
            {
                if (_creature.gameObject == Owner.gameObject)
                    return;
            }

            other.SendMessage("OnDamage", this);
        }
    }
}
