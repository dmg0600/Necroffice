using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AxeWeapon : Weapon
{
    public override void attack()
    {
        if (!canAttack())
            return;

        _attacking = true;

        //Animación
        //todo


        //Ataque
        Vector3 _attackingDirection = owner.transform.forward;
        _attackingDirection.y = 0;
        _attackingDirection *= 8;

        Creature _creatureOwner = owner.GetComponent<Creature>();

        GameManager.Instance.CreateHitbox(_creatureOwner, 1, 1, _attackingDirection, 1f);
    }

    public override bool canAttack()
    {
        return true;
    }


    void FixedUpdate()
    {

        
        if (weaponMode == WeaponMode.AI)
        {
            updateAI();
        }
    }

    public override void updateAI()
    {
        if (Vector3.Distance(owner.transform.position, GameManager.Instance.playerTransform.position) > Range)
        {
            move();
        }
        else
        {
            attack();
        }

    }

    ///**
    // * Controla la colision del arma cuando esta atacando
    // * 
    //*/
    //void OnCollisionEnter(Collision collision)
    //{
    //    if(_attacking && !_damagedEntities.Contains(collision.gameObject))
    //    {
    //        //collision.gameObject.GetComponent<Life>().damage(1 + Power);
    //    }
    //}

    //private List<GameObject> _damagedEntities;

    private bool _attacking;

}
