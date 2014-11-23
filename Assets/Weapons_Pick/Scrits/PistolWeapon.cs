using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PistolWeapon : Weapon
{
    public override IEnumerator attack()
    {
        if (!canAttack())
            yield break;

       //GameManager.Instance.CreateHitbox

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
        if (Vector3.Distance(owner.transform.position, GameManager.Instance.Player.transform.position) > Range)
        {
            move();
        }
        else
        {
            attack();
        }
    }
}
