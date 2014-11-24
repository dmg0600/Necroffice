using UnityEngine;
using System.Collections;

public class Life : MonoBehaviour
{
    public Creature owner;
    public AudioClip[] DwarfDamage;
    public AudioClip[] SkeDamage;

    public Stats.Attribute life;

    bool destroyinh = false;
    bool onEvent = false;

    public void OnDamage(Hitbox hitbox)
    {
        InteractiveObject _iObject = GetComponent<InteractiveObject>();
        if (_iObject != null)
            _iObject.DamagedByHitbox(hitbox);

        OnDamage(hitbox.Damage);
    }

    public void OnDamage(int damage)
    {
        if (owner != null)
            if (owner._Weapon != null)
            {
                if (owner._Weapon.weaponMode == WeaponMode.CONTROLLED) AudioSource.PlayClipAtPoint(DwarfDamage[Random.Range(0, DwarfDamage.Length)], transform.position);
                else if (owner._Weapon.weaponMode == WeaponMode.AI) AudioSource.PlayClipAtPoint(SkeDamage[Random.Range(0, SkeDamage.Length)], transform.position);

                GetComponent<Controller>()._Animator.SetBool("Damaged", false);
            }
        
        life.sub(damage);

        if (life.isLower)
        {
            if (!destroyinh)
            {
                destroyinh = true;
                transform.BroadcastMessage("OnDead");
                destroyinh = false;

                if (owner != null)
                    if (owner._Weapon != null)
                    {
                        if (owner._Weapon.weaponMode == WeaponMode.CONTROLLED) AudioSource.PlayClipAtPoint(DwarfDamage[Random.Range(0, DwarfDamage.Length)], transform.position);
                        else if (owner._Weapon.weaponMode == WeaponMode.AI) AudioSource.PlayClipAtPoint(SkeDamage[Random.Range(0, SkeDamage.Length)], transform.position);

                        GetComponent<Controller>()._Animator.SetBool("Death", true);
                    }
            }
        }
        else
        {
            if (owner != null)
                if (owner._Weapon != null)
                {
                    if (owner._Weapon.weaponMode == WeaponMode.CONTROLLED) AudioSource.PlayClipAtPoint(DwarfDamage[Random.Range(0, DwarfDamage.Length)], transform.position);
                    else if (owner._Weapon.weaponMode == WeaponMode.AI) AudioSource.PlayClipAtPoint(SkeDamage[Random.Range(0, SkeDamage.Length)], transform.position);

                    GetComponent<Controller>()._Animator.SetBool("Damaged", true);
                }
        }
    }


    public void OnHeal(int cure)
    {
        life.add(cure);

        if (life.isMax)
            transform.root.BroadcastMessage("OnPlentyLife");

    }
}
