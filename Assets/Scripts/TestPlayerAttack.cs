using UnityEngine;
using System.Collections;

public class TestPlayerAttack : MonoBehaviour
{
    void OnInputMouseClick(Vector3 clickPoint)
    {
        //float _distance = 2;
        //Vector3 _hitboxSpawnPoint = transform.position + (GetDirectionFromClick(clickPoint) * _distance);
        //_hitboxSpawnPoint.y = transform.position.y;

        Vector3 _attackingDirection = transform.forward;
        _attackingDirection.y = 0;
        _attackingDirection *= 8;

        GameManager.Instance.CreateHitbox(GetComponent<Creature>(), 1, 1, _attackingDirection);
    }

   //public static Vector3 GetDirectionFromClick(Vector3 clickPoint)
   // {
   //     Vector3 _direction = (clickPoint - transform.position).normalized;
   //     _direction.y = transform.position.y;
   //     return _direction;
   // }


}
