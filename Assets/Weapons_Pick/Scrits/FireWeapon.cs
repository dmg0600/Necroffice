using UnityEngine;
using System.Collections;

public class FireWeapon : Weapon 
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
        int[] i = new int[] { 3, 5 };
        owner.GetComponent<Controller>()._Animator.SetInteger("Attack", i[Random.Range(0, i.Length)]);

        //AudioSource.PlayClipAtPoint(audio[Random.Range(0, audio.Length)], transform.position);

        yield return new WaitForSeconds(0.7f);

        //_attacking = false;
        Hitbox.gameObject.SetActive(false);
    }

    public override bool canAttack()
    {
        return true;
    }

}
