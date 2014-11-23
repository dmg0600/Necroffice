using UnityEngine;
using System.Collections;

public class BriefcaseWeapon : Weapon 
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

    // Use this for initialization
    void Start()
    {
        selectTarget();
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
