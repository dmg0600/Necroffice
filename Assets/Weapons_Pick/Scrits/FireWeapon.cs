using UnityEngine;
using System.Collections;

public class FireWeapon : Weapon 
{
    public override IEnumerator attack()
    {
        if (!canAttack())
            yield break;

        //Animación

        //Ataque

    }

    public override bool canAttack()
    {
        return true;
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
