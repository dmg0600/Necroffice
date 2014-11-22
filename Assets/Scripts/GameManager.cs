using UnityEngine;
using System.Collections;

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

    public void CreateHitbox(Creature owner, float radius, int damage, Vector3 velocity)
    {
        GameObject _obj = Instantiate(HitboxPrefab, owner.transform.position + (owner.transform.forward * 0.5f), Quaternion.identity) as GameObject;
        Hitbox _hitbox = _obj.GetComponent<Hitbox>();

        _hitbox.Owner = owner;
        _hitbox.Radius = radius;
        _hitbox.Damage = damage;
        _hitbox.Duration = 1;

        _hitbox.SetVelocity(velocity);

        _hitbox.Begin();
    }

}
