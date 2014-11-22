using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NewspaperWeapon : Weapon
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

    public override void selectTarget()
    {
        NavMeshPath path = new NavMeshPath();
        if (!_targetList.Contains(target))
        {
            foreach (GameObject pTarget in _targetList)
            {
                NavMesh.CalculatePath(transform.position, pTarget.transform.position, -1, path);

                if (path.corners.Length < _currentPath.corners.Length && path.status != NavMeshPathStatus.PathInvalid)
                {
                    _currentPath = path;
                    _currentCorner = 0;
                }
            }
        }

        Invoke("selectTarget", 1.0f);
    }

    public override void move()
    {
        Vector3 direction = _currentPath.corners[_currentCorner] - transform.position;

        direction.y = 0;

        if (direction.magnitude < 1.0)
            direction = _currentPath.corners[++_currentCorner] - transform.position;

        //owner.GetComponent<Controller>().OnInputAxis(direction);
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
    private int _currentCorner;
    private NavMeshPath _currentPath;
    private List<GameObject> _damagedEntities;
}
