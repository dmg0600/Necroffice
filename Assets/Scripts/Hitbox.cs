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
}
