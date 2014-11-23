using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NewspaperWeapon : Weapon
{
    public override IEnumerator attack()
    {
        if (!canAttack())
            yield break;

        //Propiedades de hitbox
        Hitbox.Duration = 0;
        Hitbox.Damage = Mathf.Clamp(_owner._Stats.Power.value + Power, 1, 5);

        //Habilitar hitbox
        Hitbox.gameObject.SetActive(true);
        //todo: desactivar con animacion

        //<HACK>
        //yield return new WaitForSeconds(1); MeleeHitbox.gameObject.SetActive(false);
        //</HACK>

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
        float distance = Vector3.Distance(owner.transform.position, GameManager.Instance.Player.transform.position);
        bool dead = GameManager.Instance.Player.GetComponent<Life>().life.value == 0;
        //Debug.Log(Vector3.Distance(owner.transform.position, GameManager.Instance.Player.transform.position));
        if (distance > Range && !dead)
        {
            move();
        }
        else if (!dead)
        {
            if (!_attacking)
            {
                StartCoroutine(atackHandler());
            }
        }
        /*else
        {
            idle();
        }*/
    }
}
