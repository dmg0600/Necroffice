using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PistolWeapon : Weapon
{
    public AudioClip audio;
    public override IEnumerator attack()
    {
        if (!canAttack())
            yield break;

        Hitbox clone = Instantiate(Hitbox, transform.position, transform.rotation) as Hitbox;

        clone.Owner = this.owner;
        clone.Damage = DamageRanged;
        clone.Duration = VelocityRanged * Range;
        clone.name = "Hitbox (" + this.name + " - " + this.owner.name + ")";
        clone.Properties = this.Property.ToList();

        clone.SetVelocity(owner.transform.forward * VelocityRanged);

        clone.Begin();
        AudioSource.PlayClipAtPoint(audio, transform.position);

        owner.GetComponent<Controller>()._Animator.SetInteger("Attack", UnityEngine.Random.Range(0, 3));
    }

    public override bool canAttack()
    {
        return true;
    }


    void FixedUpdate()
    {
        if (_attacking && !owner.GetComponent<Controller>()._Animator.GetCurrentAnimatorStateInfo(0).IsName("RangedAtk_1") &&
            !owner.GetComponent<Controller>()._Animator.GetCurrentAnimatorStateInfo(0).IsName("RangedAtk_2"))
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
