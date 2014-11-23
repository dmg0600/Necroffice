using UnityEngine;
using System.Collections;

public class FireWeapon : Weapon 
{
    public void Start()
    {
        GameManager.Instance.ConvertMeeleWeapon(this, 5.0f, DamageRanged, name);
    }

    public override IEnumerator attack()
    {
        if (!canAttack())
            yield break;

        //Animación
        //todo

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
