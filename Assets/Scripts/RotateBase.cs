using UnityEngine;
using System.Collections;

public class RotateBase : MonoBehaviour
{
    public float Yspeed = 1;

    void Update()
    {
        transform.Rotate(Vector3.up, Yspeed);
    }
}
