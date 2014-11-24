using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PistolWeapon : Weapon
{
    public AudioClip audio;

    //public GameObject HitboxShoot;

    public override IEnumerator attack()
    {
        if (!canAttack())
            yield break;

        owner.GetComponent<Controller>()._Animator.SetInteger("Attack", 2);
        yield return new WaitForSeconds(0.25f);

        Hitbox clone = Instantiate(Hitbox, transform.position, owner.transform.rotation) as Hitbox;

        clone.Owner = this.owner;
        clone.Damage = DamageRanged;
        clone.Duration = Range;
        clone.name = "Hitbox (" + this.name + " - " + this.owner.name + ")";
        clone.Properties = this.Property.ToList();

        clone.rigidbody.velocity = owner.transform.forward * VelocityRanged;
        clone.gameObject.SetActive(true);

        StartCoroutine(clone.Begin());

        AudioSource.PlayClipAtPoint(audio, transform.position);

        yield return new WaitForSeconds(0.5f);
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
                StartCoroutine(attackHandler());
            }
        }
        /*else
        {
            idle();
        }*/
    }
}
