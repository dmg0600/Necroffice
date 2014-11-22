using UnityEngine;
using System.Collections;

public class Spikes : MonoBehaviour {

    public int force = 100;
    private float distance = 2f;

    void OnTriggerStay(Collider other)
    {
        if (other.rigidbody != null)
        {
            float angle = Random.Range(0, Mathf.PI * 2f);

            Vector3 result = Vector3.zero;
            
            result.z = (int)Mathf.Round(transform.position.z + distance * Mathf.Sin(angle));
            result.x = (int)Mathf.Round(transform.position.x + distance * Mathf.Cos(angle));

            Quaternion rotate = Quaternion.LookRotation(transform.up);

            result = rotate * result;

            result.z += other.transform.forward.z * 2;

            other.rigidbody.AddForce(result.normalized * force);
        }
    }
}
