using UnityEngine;
using System.Collections;

public class ExplodeBehaviour : LifeBehaviour {

    public int damage;
    public float force = 50f;
    public float radius = 10f;

    public override void OnRespawn() { }

    public override void OnDead()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider col in colliders)
        {
            if (col.rigidbody != null)
            {
                GameManager.Instance.CreateHitbox(transform, radius, damage, 1f);
                col.rigidbody.AddExplosionForce(force, transform.position, radius);
            }
        }
    }
}
