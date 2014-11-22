using UnityEngine;
using System.Collections;

public class Fan : MonoBehaviour {

    public int force = 50;

	void OnTriggerStay (Collider other) {
        if (other.rigidbody != null)
        {
            other.rigidbody.AddForce(transform.forward * force);
            Debug.Log(other.name);
        }
	}
}
