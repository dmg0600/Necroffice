using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AxeWeapon : Weapon
{
    public override IEnumerator attack()
    {
        if (!canAttack())
            yield break;

        //Propiedades de hitbox
        MeleeHitbox.Duration = 0;
        MeleeHitbox.Damage = Mathf.Clamp(_owner._Stats.Power.value + Power, 1, 5);

        //Habilitar hitbox
        MeleeHitbox.gameObject.SetActive(true);
        //todo: desactivar con animacion

        // Play Animacion
        //todo

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
        //Debug.Log(Vector3.Distance(owner.transform.position, GameManager.Instance.Player.transform.position));
        if (Vector3.Distance(owner.transform.position, GameManager.Instance.Player.transform.position) > Range)
        {
            move();
        }
        else 
        {
            if (!_attacking)
            {
                StartCoroutine(atackHandler());
            }
            
        }
    }
}
