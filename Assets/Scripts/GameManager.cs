using UnityEngine;
using System.Collections;
using System.Collections.Generic;
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


    public void CreateHitbox(Weapon ownerWeapon, float radius, int damage, Vector3 velocity, float duration, string nameOfHitbox = null)
    {
        GameObject _obj = Instantiate(HitboxPrefab, ownerWeapon.owner.transform.position + (ownerWeapon.owner.transform.forward * 0.5f), Quaternion.identity) as GameObject;
        Hitbox _hitbox = _obj.GetComponent<Hitbox>();

        _hitbox.Owner = ownerWeapon.owner;
        _hitbox.Radius = radius;
        _hitbox.Damage = damage;
        _hitbox.Duration = duration;
        _hitbox.name = nameOfHitbox ?? "Hitbox (" + ownerWeapon.owner.name + ")";
        _hitbox.Properties = ownerWeapon.Property.ToList();

        _hitbox.SetVelocity(velocity);

        _hitbox.Begin();
    }

    public void CreateHitbox(Creature owner, float radius, int damage, Vector3 velocity, float duration, string nameOfHitbox = null)
    {
        GameObject _obj = Instantiate(HitboxPrefab, owner.transform.position + (owner.transform.forward * 0.5f), Quaternion.identity) as GameObject;
        Hitbox _hitbox = _obj.GetComponent<Hitbox>();

        _hitbox.Owner = owner;
        _hitbox.Radius = radius;
        _hitbox.Damage = damage;
        _hitbox.Duration = duration;
        _hitbox.name = nameOfHitbox ?? "Hitbox (" + owner.name + ")";

        _hitbox.SetVelocity(velocity);

        _hitbox.Begin();
    }

    public void CreateHitbox(Transform origin, float radius, int damage, float duration, string nameOfHitbox = null)
    {
        GameObject _obj = Instantiate(HitboxPrefab, origin.position, Quaternion.identity) as GameObject;
        Hitbox _hitbox = _obj.GetComponent<Hitbox>();

        _hitbox.Owner = null;
        _hitbox.Radius = radius;
        _hitbox.Damage = damage;
        _hitbox.Duration = duration;
        _hitbox.name = nameOfHitbox ?? "Hitbox";


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
