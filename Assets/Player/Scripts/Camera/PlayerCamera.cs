using UnityEngine;
using System.Collections;

public class PlayerCamera : MonoBehaviour
{
    public Transform target;
    public float distance = 10;
    float currentDistance;

    //The default distance of the camera from the target.
    public float _distance = 20.0f;

    //Control the speed of zooming and dezooming.
    public float _zoomStep = 1.0f;

    float fixYAngle = 55.0f;

    //The speed of the camera. Control how fast the camera will rotate.
    public float _xSpeed = 1f;
    public float _ySpeed = 1f;

    //The position of the cursor on the screen. Used to rotate the camera.
    private float _x = 0.0f;
    private float _y = 0.0f;

    float damping = 3;
    Vector3 offset;


    void LateUpdate()
    {
        if (target)
        {
            Vector3 _desiredPosition = target.position - (transform.forward * distance);
            transform.position = Vector3.Lerp(transform.position, _desiredPosition, Time.deltaTime * damping);

            currentDistance = (transform.position - target.position).magnitude;

            this.RotateControls();
            //this.Zoom();
        }
    }

    /**
     * Rotate the camera when the first button of the mouse is pressed.
     * 
     */
    void RotateControls()
    {
        _x += Input.GetAxis("CameraHorizontal");

        if (Input.GetButton("Fire2"))
        {
            _x += Input.GetAxis("Mouse X") * _xSpeed;
        }

        this.Rotate(_x);
    }

    /**
     * Transform the cursor mouvement in rotation and in a new position
     * for the camera.
     */
    void Rotate(float x)
    {
        //Transform angle in degree in quaternion form used by Unity for rotation.
        Quaternion rotation = Quaternion.Euler(fixYAngle, x, 0.0f);
        Vector3 _distanceVector = new Vector3(0.0f, 0.0f, -currentDistance);

        //The new position is the target position + the distance vector of the camera
        //rotated at the specified angle.
        Vector3 position = rotation * _distanceVector + target.position;

        //Update the rotation and position of the camera.
        transform.rotation = rotation;
        transform.position = position;
    }

    ///**
    // * Zoom or dezoom depending on the input of the mouse wheel.
    // */
    //void Zoom()
    //{
    //    if (Input.GetAxis("Mouse ScrollWheel") < 0.0f)
    //    {
    //        this.ZoomOut();
    //    }
    //    else if (Input.GetAxis("Mouse ScrollWheel") > 0.0f)
    //    {
    //        this.ZoomIn();
    //    }

    //}

    ///**
    // * Reduce the distance from the camera to the target and
    // * update the position of the camera (with the Rotate function).
    // */
    //void ZoomIn()
    //{
    //    _distance -= _zoomStep;
    //    _distanceVector = new Vector3(0.0f, 0.0f, -_distance);
    //    this.Rotate(_x, _y);
    //}

    ///**
    // * Increase the distance from the camera to the target and
    // * update the position of the camera (with the Rotate function).
    // */
    //void ZoomOut()
    //{
    //    _distance += _zoomStep;
    //    _distanceVector = new Vector3(0.0f, 0.0f, -_distance);
    //    this.Rotate(_x, _y);
    //}
}
