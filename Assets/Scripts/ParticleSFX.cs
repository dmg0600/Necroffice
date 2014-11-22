using UnityEngine;
using System.Collections;

public class ParticleSFX : MonoBehaviour
{
    public float TimeToDestroy = 0;

    [HideInInspector]
    public GameObject ObjectToDestroy = null;

    void Start()
    {
        if (TimeToDestroy > 0)
            DestroyAfterTime(TimeToDestroy, ObjectToDestroy);
    }

    public void DestroyAfterTime(float Time, GameObject optionalObjectToDestroy = null)
    {
        ObjectToDestroy = optionalObjectToDestroy;
        Invoke("EndEmission", Time);
    }

    public void EndEmission()
    {
        particleEmitter.emit = false;
        if (ObjectToDestroy != null)
        {
            Destroy(ObjectToDestroy);
        }
        Invoke("GetDestroyed", 1);
    }

    void GetDestroyed()
    {
        Destroy(gameObject);
    }
}
