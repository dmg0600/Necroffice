using UnityEngine;
using System.Collections;

public class Hitbox : MonoBehaviour
{
    [HideInInspector]
    public Creature Owner = null;

    [HideInInspector]
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
        Creature _creature = other.transform.root.GetComponent<Creature>();
        if (_creature != null)
        {
            if (_creature == Owner)
                return;
            else
                _creature.SendMessage("OnDamage", this);
        }
        else
        {
            InteractiveObject _iObject = other.transform.GetComponent<InteractiveObject>();
            if (_iObject == null)
                return;
            else
            {
                //Debug.Log("Envio OnDamage a " + other.name);
                _iObject.SendMessage("OnDamage", this);
            }
        }
    }
}
