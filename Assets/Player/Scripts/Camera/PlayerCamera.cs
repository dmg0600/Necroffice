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

    float fixYAngle = 50.0f;

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
        }
    }

    void RotateControls()
    {
        _x += Input.GetAxis("CameraHorizontal");

        if (Input.GetButton("Fire2"))
            _x += Input.GetAxis("Mouse X") * _xSpeed;

        Rotate(_x);
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

    // Default time in seconds for which to detect double clicking of a key.
    public const float DefaultTimeThreshold = 0.5f;

    private static string _multiClickAnchorKey;
    private static float _multiClickAnchorTime;
    private static int _multiClickCount;

    public static void CancelMultiClick()
    {
        _multiClickAnchorKey = null;
    }

    public static int GetMultiClickKeyCount(string key, float timeThreshold)
    {
        if (Input.GetButtonDown(key))
        {
            // Do we need to cancel the last multi-click operation for this key?
            if (_multiClickAnchorKey == key)
                if (Time.time - _multiClickAnchorTime > timeThreshold)
                    CancelMultiClick();

            _multiClickAnchorTime = Time.time;

            // Has button been pressed for first time?
            if (_multiClickAnchorKey != key)
            {
                _multiClickAnchorKey = key;
                _multiClickCount = 1;
            }
            else
            {
                // Okay, so this is a multi-click operation!
                ++_multiClickCount;
            }
            return _multiClickCount;
        }
        return 0;
    }

    public static int GetMultiClickKeyCount(string key)
    {
        return GetMultiClickKeyCount(key, DefaultTimeThreshold);
    }

    public static bool HasDoubleClickedKey(string key, float timeThreshold)
    {
        return GetMultiClickKeyCount(key, timeThreshold) == 2;
    }

    public static bool HasDoubleClickedKey(string key)
    {
        return HasDoubleClickedKey(key, DefaultTimeThreshold);
    }
}
