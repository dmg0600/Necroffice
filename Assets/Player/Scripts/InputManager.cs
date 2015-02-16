using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class InputManager : MonoBehaviour
{
    GameObject Subject;

    [SerializeField]
    GameObject RotateRef;

    int floorLayer;

    bool isInputEnable = true;

    public void EnableInput(bool isEnable)
    {
        isInputEnable = isEnable;
    }

    void Awake() 
    {
        Subject = this.transform.root.gameObject;
        floorLayer = 1 << LayerMask.NameToLayer("Floor");
    }

    void Update()
    {
        if (PauseMenu.isPaused) return;

        if (isInputEnable)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit, Mathf.Infinity, floorLayer))
                {
                    Subject.BroadcastMessage("OnInputMouseClick", hit.point);
                }
            }

            Vector3 axis = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;

            if (axis != Vector3.zero)
            {
                Subject.BroadcastMessage("OnInputAxis", axis);
            }
        }
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
