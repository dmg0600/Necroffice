using UnityEngine;
using System.Collections;

public abstract class LifeBehaviour : MonoBehaviour 
{
    public bool imDead = false;

    public abstract IEnumerator OnDead();
    public abstract IEnumerator OnRespawn();
}
