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
