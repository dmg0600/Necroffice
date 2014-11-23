using UnityEngine;
using System.Collections;

public abstract class LifeBehaviour : MonoBehaviour 
{
    public abstract IEnumerator OnDead();
    public abstract IEnumerator OnRespawn();
}
