using UnityEngine;
using System.Collections;

public class LittleTrap : MonoBehaviour {

    public Rigidbody puerta1;
    public Rigidbody puerta2;

    void OnTriggerEnter(Collider other)
    {
        if (other.rigidbody != null && other.tag != "LittleTrap")
        {
            puerta1.isKinematic = false;
            puerta2.isKinematic = false;
        }
    }
}
