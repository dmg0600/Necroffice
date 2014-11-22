using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]

public class DwarfController : MonoBehaviour
{
    float speed = 10.0f;
    float gravity = 10.0f;
    float maxVelocityChange = 10.0f;



    private bool grounded = false;

    void Awake()
    {
        rigidbody.freezeRotation = true;
        rigidbody.useGravity = false;
    }

    void FixedUpdate()
    {
        if (grounded)
        {
            // Calculate how fast we should be moving
            Vector3 targetOrientation = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            targetOrientation = transform.TransformDirection(targetOrientation).normalized;

            // Apply a force that attempts to reach our target velocity
            //Vector3 velocity = rigidbody.velocity;
            //Vector3 velocityChange = (targetOrientation - velocity);
            //velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
            //velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
            //velocityChange.y = 0;
            //rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);

            Debug.Log(targetOrientation);

            transform.LookAt(transform.position + targetOrientation);
        }

        grounded = false;
    }

    void OnCollisionStay()
    {
        grounded = true;
    }
}