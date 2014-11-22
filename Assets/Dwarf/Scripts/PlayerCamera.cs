using UnityEngine;
using System.Collections;

public class PlayerCamera : MonoBehaviour
{
    public SpringPosition SpringComponent;

    public Transform target;

    public float distance = 10;


    float damping = 3;

    Vector3 offset;

    void Start()
    {

    }

    void LateUpdate()
    {
        Vector3 _desiredPosition = target.position - (transform.forward * distance);

        transform.position = Vector3.Lerp(transform.position, _desiredPosition, Time.deltaTime * damping);

        //transform.position = _desiredPosition;

        //SpringComponent.target = _desiredPosition;
        //SpringComponent.enabled = true;
    }
}
