using UnityEngine;
using System.Collections;

public class PlayerCamera : MonoBehaviour
{
    public Transform target;
    public float distance = 10;



    float damping = 3;
    Vector3 offset;

    void LateUpdate()
    {
        Vector3 _desiredPosition = target.position - (transform.forward * distance);
        transform.position = Vector3.Lerp(transform.position, _desiredPosition, Time.deltaTime * damping);
    }
}
