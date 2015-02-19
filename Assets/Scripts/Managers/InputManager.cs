using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class InputManager : SingletonComponent<InputManager>
{
    public delegate void KeyEvent(string K);
    public delegate void JoystikEvent(string axe, float value);

    private List<string> keys;
    private List<string> axis;
    private Dictionary<string, KeyEvent> keyDownEvents, keyUpEvents;
    private Dictionary<string, JoystikEvent> axisEvents;
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
        axisEvents = new Dictionary<string, JoystikEvent>();
        keys = new List<string>();
        axis = new List<string>();
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

        foreach (string axe in axis)
        {
            OnAxis(axe, Input.GetAxis(axe));
        }
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

    public void registerAxis(string axe, JoystikEvent kEvent)
    {
       
        if (axisEvents.ContainsKey(axe))
            axisEvents[axe] += kEvent;
        else
        {
            if (!axis.Contains(axe)) axis.Add(axe);
            axisEvents.Add(axe, kEvent);
        }
    }

    public void unRegisterAxis(string axe, JoystikEvent kEvent, bool removeKey)
    {
        if (axisEvents.ContainsKey(axe))
        {
            axisEvents[axe] -= kEvent;
            if (axisEvents[axe] == null)
                axisEvents.Remove(axe);
        }
        if (removeKey) RemoveAxe(axe);
    }

    public void RemoveKey(string K)
    {
        if (keyDownEvents.ContainsKey(K)) keyDownEvents.Remove(K);
        if (keyUpEvents.ContainsKey(K)) keyUpEvents.Remove(K);
        if (keys.Contains(K)) keys.Remove(K);
    }

    public void RemoveAxe(string K)
    {
        if (axisEvents.ContainsKey(K)) keyDownEvents.Remove(K);
        if (axis.Contains(K)) keys.Remove(K);
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

    private void OnAxis(string axe, float value)
    {
        JoystikEvent E = null;
        if (axisEvents.TryGetValue(axe, out E))
            if (E != null)
                E(axe, value);
    }

    #endregion
}
