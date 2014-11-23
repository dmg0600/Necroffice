using UnityEngine;
using System.Collections;
using System.Linq;

public class GameManager : MonoBehaviour
{
    #region Singleton

    public static GameManager Instance = null;

    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Ya hay más de un GameManager en " + Application.loadedLevelName);
            Destroy(gameObject);
        }
        Instance = this;
    }

    #endregion

    public GameObject HitboxPrefab;
    public Weapon DefaultWeapon;
    public ParticleSFX[] Particles;

    public Transform playerTransform;

    public void CreateHitbox(Creature owner, float radius, int damage, Vector3 velocity, float duration)
    {
        GameObject _obj = Instantiate(HitboxPrefab, owner.transform.position + (owner.transform.forward * 0.5f), Quaternion.identity) as GameObject;
        Hitbox _hitbox = _obj.GetComponent<Hitbox>();

        _hitbox.Owner = owner;
        _hitbox.Radius = radius;
        _hitbox.Damage = damage;
        _hitbox.Duration = duration;

        _hitbox.SetVelocity(velocity);

        _hitbox.Begin();
    }

    public void CreateHitbox(Transform origin, float radius, int damage, float duration)
    {
        GameObject _obj = Instantiate(HitboxPrefab, origin.position, Quaternion.identity) as GameObject;
        Hitbox _hitbox = _obj.GetComponent<Hitbox>();

        _hitbox.Owner = null;
        _hitbox.Radius = radius;
        _hitbox.Damage = damage;
        _hitbox.Duration = duration;

        _hitbox.Begin();
    }

    public void CreateParticle(string Name, Vector3 position)
    {
        ParticleSFX _particle = Particles.FirstOrDefault(x => x.name == Name);
        if (_particle == null)
            return;

        GameObject _obj = Instantiate(_particle.gameObject, position, Quaternion.identity) as GameObject;
    }

    public void DestroyWithParticle(string Name, GameObject ObjectToDestroy)
    {
        ParticleSFX _particle = Particles.FirstOrDefault(x => x.name == Name);
        if (_particle == null)
            return;

        GameObject _obj = Instantiate(_particle.gameObject, ObjectToDestroy.transform.position, ObjectToDestroy.transform.rotation) as GameObject;
        ParticleSFX _sfxP = _obj.GetComponent<ParticleSFX>();
        _sfxP.ObjectToDestroy = ObjectToDestroy;
    }
}
