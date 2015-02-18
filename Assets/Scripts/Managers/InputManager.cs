using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class InputManager : SingletonComponent<InputManager>
{
    public delegate void KeyEvent(string K);
    public delegate void JoystikEvent(Vector3 axis);

    private List<string> keys;
    private Dictionary<string, KeyEvent> keyDownEvents, keyUpEvents;
    private event JoystikEvent keyEventHandler;

    bool isInputEnable = true;

    public void EnableInput(bool isEnable)
    {
        isInputEnable = isEnable;
    }

    void Awake()
    {
        keyDownEvents = new Dictionary<string, KeyEvent>();
        keyUpEvents = new Dictionary<string, KeyEvent>();
        keys = new List<string>();
    }

    void Update()
    {
        if (PauseMenu.isPaused || !isInputEnable) return;

        foreach (string key in keys)
        {
            if (Input.GetButtonDown(key))
                OnKeyDown(key);

            if (Input.GetButtonUp(key))
                OnKeyUp(key);
        }

        Vector3 axis = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;

        if (axis != Vector3.zero && keyEventHandler != null)
        {
            keyEventHandler(axis);
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

    #region Registration
    public void RegisterKeyDown(string K, KeyEvent kEvent)
    {
        if (keyDownEvents.ContainsKey(K))
            keyDownEvents[K] += kEvent;
        else
        {
            if (!keys.Contains(K)) keys.Add(K);
            keyDownEvents.Add(K, kEvent);
        }
    }

    public void RegisterKeyUp(string K, KeyEvent kEvent)
    {
        if (keyUpEvents.ContainsKey(K))
            keyUpEvents[K] += kEvent;
        else
        {
            if (!keys.Contains(K)) keys.Add(K);
            keyUpEvents.Add(K, kEvent);
        }
    }

    public void UnregisterKeyDown(string K, KeyEvent kEvent, bool removeKey)
    {
        if (keyDownEvents.ContainsKey(K))
        {
            keyDownEvents[K] -= kEvent;
            if (keyDownEvents[K] == null)
                keyDownEvents.Remove(K);
        }
        if (removeKey) RemoveKey(K);
    }

    public void UnregisterKeyUp(string K, KeyEvent kEvent, bool removeKey)
    {
        if (keyUpEvents.ContainsKey(K))
        {
            keyUpEvents[K] -= kEvent;
            if (keyUpEvents[K] == null)
                keyUpEvents.Remove(K);
        }
        if (removeKey) RemoveKey(K);
    }

    public void registerAxis(JoystikEvent delegateFunc)
    {
        //unregister first
        keyEventHandler -= delegateFunc;
        keyEventHandler += delegateFunc;
    }

    public void unRegisterAxis(JoystikEvent delegateFunc)
    {
        //unregister first
        keyEventHandler -= delegateFunc;
    }

    public void RemoveKey(string K)
    {
        if (keyDownEvents.ContainsKey(K)) keyDownEvents.Remove(K);
        if (keyUpEvents.ContainsKey(K)) keyUpEvents.Remove(K);
        if (keys.Contains(K)) keys.Remove(K);
    }
    #endregion


    #region Key detection
    private void OnKeyDown(string K)
    {
        KeyEvent E = null;
        if (keyDownEvents.TryGetValue(K, out E))
            if (E != null)
                E(K);
    }
    private void OnKeyUp(string K)
    {
        KeyEvent E = null;
        if (keyUpEvents.TryGetValue(K, out E))
            if (E != null)
                E(K);
    }
    #endregion
}
