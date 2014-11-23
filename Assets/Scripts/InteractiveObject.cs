using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class InteractiveObject : MonoBehaviour
{
    public enum Properties { Invisible = 0, Kinematic, Levitate, Fire, CanBurn, Immortal };

    public List<Properties> _Properties = new List<Properties>();



    public bool onFire = false;

    public void Start()
    {
        if (_Properties.Contains(Properties.Invisible))
        {
            foreach (var _component in GetComponentsInChildren<Renderer>())
            {
                _component.enabled = false;
            }
        }

        if (_Properties.Contains(Properties.Kinematic))
        {
            rigidbody.isKinematic = true;
        }

        if (_Properties.Contains(Properties.Levitate))
        {
            rigidbody.useGravity = false;
        }

        if (_Properties.Contains(Properties.CanBurn))
            InvokeRepeating("DamageByFire", 0f, 2f);

    }

    public void OnDead()
    {
        //Se destruye

        //Explosión
        ExplodeBehaviour _explosion = GetComponent<ExplodeBehaviour>();
        if (_explosion != null)
        {
            _explosion.Detonate();
            return;
        }

        //Destruir genérico
        GameManager.Instance.DestroyWithParticle(Defines.ParticleDustExplosion, gameObject);

    }


    public void DamagedByHitbox(Hitbox hitbox)
    {
        //Set on fire
        if (_Properties.Contains(Properties.CanBurn) && hitbox.Properties.Contains(Properties.Fire) && !onFire)
        {
            onFire = true;

            ParticleSFX _particleFire = GameManager.Instance.Particles.FirstOrDefault(x => x.name == Defines.ParticleFire);
            GameObject go = Instantiate(_particleFire.gameObject, transform.position, Quaternion.identity) as GameObject;

            go.transform.parent = transform;

            ParticleController pc = go.GetComponent<ParticleController>();
            pc.vScaleMin = transform.localScale;
            pc.vScaleMax = transform.localScale*1.1f;
            pc.fTimeLife = Defines.burningTime;
            Invoke("QuitBurning", Defines.burningTime);
        }
    }

    void QuitBurning()
    {
        onFire = false;
    }

    void DamageByFire()
    {
        if (!onFire)
            return;

        GetComponent<Life>().OnDamage(1);
    }
}
