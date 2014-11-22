using UnityEngine;
using System.Collections;

public class pruebas : MonoBehaviour
{

    public float speed = 2;

    void FixedUpdate()
    {
        var moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        moveDirection = Camera.main.transform.TransformDirection(moveDirection);
        moveDirection.y = 0;
        rigidbody.MovePosition(rigidbody.position + moveDirection * speed * Time.deltaTime);

        transform.LookAt(transform.position + (rigidbody.velocity.normalized * 1));

    }
}
