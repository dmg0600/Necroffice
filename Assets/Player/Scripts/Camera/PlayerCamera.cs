using UnityEngine;
using System.Collections;

public class PlayerCamera : MonoBehaviour
{
    public Transform target;
    public float distance = 10;
    float currentDistance;

    //Control the speed of zooming and dezooming.
    public float _zoomSpeed = 30.0f;
    public float _zoomMax = 15.0f;
    public float _zoomMin = 1.0f;

    float fixYAngle = 50.0f;

    //The speed of the camera. Control how fast the camera will rotate.
    public float _xSpeed = 1f;
    public float _ySpeed = 1f;

    //The position of the cursor on the screen. Used to rotate the camera.
    private float _x = 0.0f;
    private float _y = 0.0f;

    float damping = 3;
    Vector3 offset;

    bool _enableMouseRotation = false;

    void Start() 
    {
        Debug.Log("Start");
        distance = Mathf.Clamp(distance, _zoomMin, _zoomMax);

        InputManager.Instance.registerAxis("Mouse ScrollWheel", Zoom);
        InputManager.Instance.registerAxis("CameraHorizontal", joystickRotation);
        InputManager.Instance.registerAxis("Mouse X", mouseRotation);
        InputManager.Instance.RegisterKeyDown("Fire2", rotateButtonDown);
        InputManager.Instance.RegisterKeyUp("Fire2", rotateButtonUp);
    }

    void LateUpdate()
    {
        if (target)
        {
            Vector3 _desiredPosition = target.position - (transform.forward * distance);
            transform.position = Vector3.Lerp(transform.position, _desiredPosition, Time.deltaTime * damping);

            currentDistance = (transform.position - target.position).magnitude;

            Rotate(_x);
        }
    }

    void rotateButtonUp(string key)
    {
        _enableMouseRotation = false;
    }

    void rotateButtonDown(string key)
    {
        _enableMouseRotation = true;
    }

    public void mouseRotation(string axe, float value)
    {
        _x += value;
    }

    public void joystickRotation(string axe, float value)
    {
        _x += value;
    }

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

    /**
     * Zoom or dezoom depending on the input of the mouse wheel.
     */
    void Zoom(string axe, float value)
    {
        if (value < 0.0f)
        {
            this.ZoomOut();
        }
        else if (value > 0.0f)
        {
            this.ZoomIn();
        }
    }

    /**
     * Reduce the distance from the camera to the target and
     * update the position of the camera (with the Rotate function).
     */
    void ZoomIn()
    {
        distance -= _zoomSpeed;
        distance = Mathf.Clamp(distance, _zoomMin, _zoomMax);
        
        //this.Rotate(_x);
    }

    /**
     * Increase the distance from the camera to the target and
     * update the position of the camera (with the Rotate function).
     */
    void ZoomOut()
    {
        distance += _zoomSpeed;
        distance = Mathf.Clamp(distance, _zoomMin, _zoomMax);

        //this.Rotate(_x);
    }
}
