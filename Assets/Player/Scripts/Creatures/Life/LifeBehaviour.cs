using UnityEngine;
using System.Collections;

public abstract class PlayerLifeManager : MonoBehaviour 
{
    public abstract void OnDead();
    public abstract void OnRespawn();
}
