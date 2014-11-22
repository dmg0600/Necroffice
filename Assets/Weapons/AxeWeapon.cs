using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AxeWeapon : Weapon
{

    public override void attack()
    {
        _attacking = true;
    }

    public override bool canAttack()
    {
        return true;
    }



	// Use this for initialization
	void Start () {
        selectTarget();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	    if (weaponMode == WeaponMode.AI)
        {
            updateAI();
        }
	}

    public override void updateAI()
    {
        

        if(Vector3.Distance(owner.transform.position, target.transform.position) > Range)
        {
            move();
        }
        else
        {
            attack();
        }
        
    }

    /**
     * Controla la colision del arma cuando esta atacando
     * 
    */
    void OnCollisionEnter(Collision collision)
    {
        if(_attacking && !_damagedEntities.Contains(collision.gameObject))
        {
            //collision.gameObject.GetComponent<Life>().damage(1 + Power);
        }
    }

    private bool _attacking;
    private List<GameObject> _damagedEntities;
}
