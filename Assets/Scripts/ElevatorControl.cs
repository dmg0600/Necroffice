using UnityEngine;
using System.Collections;

public class ElevatorControl : MonoBehaviour
{
    public GameObject SkullBlink;

    public SpringPosition PuertaR, PuertaL;


    bool busy = false;
    bool opened = false;

    void Blink()
    {
        SkullBlink.SetActive(!SkullBlink.activeSelf);
    }

    public void OpenDoors()
    {
        if (busy) return;

        busy = true;

        PuertaR.target = new Vector3(0, 0, -1);

        PuertaL.target = new Vector3(0, 0, 1);

        PuertaL.strength = 1;
        PuertaR.strength = 1;

        PuertaR.enabled = true;

        PuertaL.onFinished = EndAnimation;

        PuertaL.enabled = true;

        InvokeRepeating("Blink", 0, 0.33f);
    }

    void EndAnimation()
    {
        busy = false;
        opened = !opened;
    }

    public void CloseDoors()
    {

        PuertaR.target = Vector3.zero;

        PuertaL.target = Vector3.zero;

        PuertaL.strength = 5;
        PuertaR.strength = 5;

        PuertaL.onFinished = EndAnimation;

        PuertaR.enabled = true;

        PuertaL.enabled = true;

        CancelInvoke("Blink");

        busy = true;
    }

    public void Toggle()
    {
        if (opened)
            CloseDoors();
        else
            OpenDoors();
    }

    void OnTriggerEnter(Collider other)
    {
        Creature _creature = other.GetComponent<Creature>();

        if (_creature == null)
            return;

        if (_creature != GameManager.Instance.Player)
            return;

        OpenDoors();
    }

    void OnTriggerExit(Collider other)
    {
        Creature _creature = other.GetComponent<Creature>();

        if (_creature == null)
            return;

        if (_creature != GameManager.Instance.Player)
            return;

        CloseDoors();
    }

}
