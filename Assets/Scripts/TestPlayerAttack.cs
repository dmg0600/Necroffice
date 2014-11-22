using UnityEngine;
using System.Collections;

public class TestPlayerAttack : MonoBehaviour
{
    public GameObject HitboxPrefab;

    void OnInputMouseClick(Vector3 clickPoint)
    {
        float _distance = 2;
        Vector3 _hitboxSpawnPoint = transform.position + (GetDirectionFromClick(clickPoint) * _distance);
        _hitboxSpawnPoint.y = transform.position.y;

        CreateHitbox(_hitboxSpawnPoint, 1, 1);
    }

    Vector3 GetDirectionFromClick(Vector3 clickPoint)
    {
        Vector3 _direction = (clickPoint - transform.position).normalized;
        _direction.y = transform.position.y;
        return _direction;
    }

    public void CreateHitbox(Vector3 position, float radius = 1, int damage = 1)
    {
        GameObject _obj = Instantiate(HitboxPrefab, position, Quaternion.identity) as GameObject;
        Hitbox _hitbox = _obj.GetComponent<Hitbox>();

        _hitbox.Owner = GetComponent<Creature>();
        _hitbox.Radius = radius;
        _hitbox.Damage = damage;
        _hitbox.Duration = 1;

        _hitbox.SetVelocity((position - transform.position).normalized * 10);

        _hitbox.Begin();
    }
}
