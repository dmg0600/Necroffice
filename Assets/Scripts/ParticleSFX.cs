using UnityEngine;
using System.Collections;

public class ParticleSFX : MonoBehaviour
{
    public void DestroyAfterTime(float Time)
    {
        Invoke("GetDestroyed", Time);
    }

    public void GetDestroyed()
    {
        Destroy(gameObject);
    }
}
