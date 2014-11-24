using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NewspaperWeapon : Weapon
{
    public AudioClip[] audio;
    int i = 0;
    public override IEnumerator attack()
    {
        if (!canAttack())
            yield break;

        //Habilitar hitbox
        Hitbox.gameObject.SetActive(true);

        //Propiedades de hitbox
        Hitbox.Duration = 0;

        Hitbox.Damage = Mathf.Clamp(_owner._Stats.Power.value + Power, 1, 5);


        //todo: desactivar con animacion

        //<HACK>
        //yield return new WaitForSeconds(1); MeleeHitbox.gameObject.SetActive(false);
        //</HACK>

        // Play Animacion
        i = i % 3 + 1;
        owner.GetComponent<Controller>()._Animator.SetInteger("Attack", i + 2);

        //AudioSource.PlayClipAtPoint(audio[Random.Range(0, audio.Length)], transform.position);

        yield return new WaitForSeconds(0.7f);

        //_attacking = false;
        Hitbox.gameObject.SetActive(false);
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
