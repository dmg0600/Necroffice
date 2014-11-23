using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AxeWeapon : Weapon
{
    public override IEnumerator attack()
    {
        if (!canAttack())
            yield break;

        //Animación
        //todo

        //Ataque
        Vector3 _attackingDirection = owner.transform.forward;
        _attackingDirection.y = 0;
        _attackingDirection *= 8;

        Creature _creatureOwner = owner.GetComponent<Creature>();

        GameManager.Instance.CreateHitbox(this, 1, 1, _attackingDirection, 1f);
    }

    public override bool canAttack()
    {
        return true;
    }


    void FixedUpdate()
    {
        if (weaponMode == WeaponMode.AI)
            updateAI();
    }

    public override void updateAI()
    {
        float distance = Vector3.Distance(owner.transform.position, GameManager.Instance.Player.transform.position);
        bool dead = GameManager.Instance.Player.GetComponent<Life>().life.value == 0;
        //Debug.Log(Vector3.Distance(owner.transform.position, GameManager.Instance.Player.transform.position));
        if (distance > Range && !dead)
        {
            move();
        }
        else  if (!dead)
        {
            if (!_attacking)
            {
                StartCoroutine(atackHandler());
            }
        }
        else
        {
            idle();
        }
    }

}
