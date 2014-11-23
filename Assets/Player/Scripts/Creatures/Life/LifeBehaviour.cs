using UnityEngine;
using System.Collections;

public abstract class LifeBehaviour : MonoBehaviour 
{
    public abstract void OnDead();
    public abstract void OnRespawn();
}
