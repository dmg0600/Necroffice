using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class InteractiveObject : MonoBehaviour
{
    public enum Properties { Invisible = 0, Kinematic, Levitate, Fire, CanBurn, Explodes, Immortal };

    public List<Properties> myProperties = new List<Properties>();


    public void Start()
    {
        if (myProperties.Contains(Properties.Invisible))
        {
            foreach (var _component in GetComponentsInChildren<Renderer>())
            {
                _component.enabled = false;
            }
        }

        if (myProperties.Contains(Properties.Kinematic))
        {
            rigidbody.isKinematic = true;
        }

        if (myProperties.Contains(Properties.Levitate))
        {
            rigidbody.useGravity = false;
        }

    }

    public void OnDead()
    {
        //Se destruye
        Debug.Log("Se destruye objeto " + gameObject.name);

        //Partícula
        GameManager.Instance.DestroyWithParticle("DustExplosion", gameObject);
    }
}
