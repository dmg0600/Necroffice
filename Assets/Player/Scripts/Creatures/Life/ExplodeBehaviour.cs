using UnityEngine;
using System.Collections;

public class ExplodeBehaviour : MonoBehaviour
{
    public int damage = 0;
    public float force = 50f;
    public float radius = 10f;

    public void Detonate()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider col in colliders)
        {
            InteractiveObject _iObject = col.GetComponent<InteractiveObject>();
            if (_iObject != null)
            {
                if (col.rigidbody != null)
                {
                    GameManager.Instance.CreateHitbox(transform, radius, damage, 1f, "Explosion (" + gameObject.name + ")");

                    col.rigidbody.AddExplosionForce(force, transform.position, radius);

                    GameManager.Instance.DestroyWithParticle("Explosion", col.gameObject);
                }
            }
        }

    }
}
