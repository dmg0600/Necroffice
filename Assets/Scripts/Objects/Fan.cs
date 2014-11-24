using UnityEngine;
using System.Collections;

public class Fan : MonoBehaviour {

    public int force = 50;

    public AudioClip[] fan;

    void Start()
    {
        audio.clip = fan[Random.Range(0, fan.Length)];
        audio.Play();
    }

	void OnTriggerStay (Collider other) {
        if (other.rigidbody != null)
        {
            other.rigidbody.AddForce(transform.forward * force);
        }
	}
}
