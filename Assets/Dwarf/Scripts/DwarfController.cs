using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]

public class DwarfController : MonoBehaviour
{
    [SerializeField]
    private float speed = 10.0f;
    [SerializeField]
    private float gravity = 10.0f;
    [SerializeField]
    private float maxVelocityChange = 10.0f;
    [SerializeField]
    private float lookSpeed = 10.0f;
    
    private bool grounded = false;

    Transform graphic;

    void Awake()
    {
        graphic = transform.root.FindChild("Graphic");
    }

    void FixedUpdate()
    {
        if (grounded)
        {
            // Calculate how fast we should be moving
            Vector3 targetOrientation = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;

            if (targetOrientation == Vector3.zero) return;

            // Apply a force that attempts to reach our target velocity
            Vector3 velocity = rigidbody.velocity;
            Vector3 velocityChange = (targetOrientation - velocity);
            velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
            velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
            velocityChange.y = 0;
            velocityChange.Normalize();

            //Debug.DrawRay(transform.position, targetOrientation, Color.red, .5f);

            transform.rotation = Quaternion.LookRotation(targetOrientation);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(targetOrientation), Time.fixedDeltaTime * lookSpeed);

            rigidbody.AddForce(transform.forward * speed, ForceMode.VelocityChange);
        }

        // We apply gravity manually for more tuning control
        rigidbody.AddForce(new Vector3(0, -gravity * rigidbody.mass, 0));

        grounded = false;
    }

    void OnCollisionStay()
    {
        grounded = true;
    }
}