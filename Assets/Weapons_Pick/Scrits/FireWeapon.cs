﻿using UnityEngine;
using System.Collections;

public class FireWeapon : Weapon 
{
    public AudioClip[] audio;
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
        owner.GetComponent<Controller>()._Animator.SetInteger("Attack", UnityEngine.Random.Range(3, 6));
        AudioSource.PlayClipAtPoint(audio[Random.Range(0, audio.Length)], transform.position);
    }

    public override bool canAttack()
    {
        return true;
    }

    void FixedUpdate()
    {
        if (_attacking && !owner.GetComponent<Controller>()._Animator.GetCurrentAnimatorStateInfo(0).IsName("MeleeAtk_1") &&
            !owner.GetComponent<Controller>()._Animator.GetCurrentAnimatorStateInfo(0).IsName("MeleeAtk_2") &&
            !owner.GetComponent<Controller>()._Animator.GetCurrentAnimatorStateInfo(0).IsName("MeleeAtk_3"))
        {
            _attacking = false;
            _owner.BroadcastMessage("OnAttackEnd");
        }

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
