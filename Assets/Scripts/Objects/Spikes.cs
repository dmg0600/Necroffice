using UnityEngine;
using System.Collections;

public class Spikes : MonoBehaviour {

    public int force = 100;

    public GameObject particles;

    private float distance = 1f;

    void OnTriggerEnter(Collider other)
    {
        if (other.rigidbody != null)
        {
            float angle = Random.Range(0, Mathf.PI * 2f);

            Vector3 result = Vector3.zero;
            
            result.z = (int)Mathf.Round(transform.position.z + distance * Mathf.Sin(angle));
            result.x = (int)Mathf.Round(transform.position.x + distance * Mathf.Cos(angle));

            Quaternion rotate = Quaternion.LookRotation(result);

            //result = rotate * result;

            //result.z += other.transform.forward.z * 2;
            
            //other.rigidbody.AddForce(result.normalized * force, ForceMode.Impulse);

            //Vector3 otro = Quaternion.LookRotation(transform.forward) * other.rigidbody.velocity;
            Vector3 aleatorio = new Vector3(Random.Range(-0.3f, .3f), 0f, Random.Range(-0f, .3f));
            Debug.Log(aleatorio);
            Vector3 direction = transform.forward + aleatorio;
            other.rigidbody.AddForce(direction * force, ForceMode.Impulse);

            if (particles)
                Instantiate(particles, other.transform.position, Quaternion.identity);
        }
    }
}
