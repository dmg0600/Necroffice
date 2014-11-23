using UnityEngine;
using System.Collections;

public class WinLevelTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Creature _creature = other.GetComponent<Creature>();
        if (_creature == null || GameManager.Instance.Player != _creature)
        {
            return;
        }

        GameManager.Instance.WinCurrentLevel();
    }
}
