﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum WeaponMode
{
    CONTROLLED,
    AI
}

public abstract class Weapon : MonoBehaviour
{
    #region unimplemented methods
    abstract public void attack();
    abstract public bool canAttack();

    //IA METHODS
    abstract public void updateAI();

    #endregion

    public virtual void selectTarget()
    {

        
        NavMeshPath path = new NavMeshPath();
        NavMesh.CalculatePath(transform.position, GameManager.Instance.gameObject.transform.position, -1, path);
        /*
        if (!_iManager.nearInteractiveObjs.Contains(target))
        {
            foreach (GameObject pTarget in _iManager.nearInteractiveObjs)
            {
                NavMesh.CalculatePath(transform.position, pTarget.transform.position, -1, path);

                if (path.corners.Length < _currentPath.corners.Length && path.status != NavMeshPathStatus.PathInvalid)
                {
                    _currentPath = path;
                    _currentCorner = 0;
                    target = pTarget;
                }
            }
        }
        */
        Invoke("selectTarget", 0.25f);
        
    }

    public virtual void move()
    {
        Vector3 direction = _currentPath.corners[_currentCorner] - transform.position;

        direction.y = 0;

        if (direction.magnitude < 1.0)
            direction = _currentPath.corners[++_currentCorner] - transform.position;

        //owner.GetComponent<Controller>().OnInputAxis(direction);
    }

    #region GET/SET
    ///////////////////////////////////////////////////////////////
    ////////////////////// GETTERS Y SETTERS //////////////////////
    ///////////////////////////////////////////////////////////////
    public WeaponMode weaponMode
    {
        set
        {
            this._mode = value;

            if (this._mode == WeaponMode.AI)
                selectTarget();
        }
        get
        {
            return this._mode;
        }
    }

    public GameObject target
    {
        set
        {
            _currentTarget = value;
        }
        get
        {
            return _currentTarget;
        }
    }

    public Creature owner
    {
        set
        {
            _owner = value;
        }
        get
        {
            return _owner;
        }
    }

    public int Power
    {
        get
        {
            return _powerBonus;
        }
    }

    public int Range
    {
        get
        {
            return _range;
        }
    }
    #endregion

    [SerializeField]
    private string _name;
    [SerializeField]
    private int _powerBonus = 0;
    [SerializeField]
    private int _agilityBonus = 0;
    [SerializeField]
    private int _range = 0;

    private GameObject _currentTarget = null;
    private Creature _owner;

    private int _currentCorner;
    private NavMeshPath _currentPath;

    private InteractionManager _iManager;
    private WeaponMode _mode = WeaponMode.CONTROLLED;

    public void SetOwner(Creature newOwner)
    {
        Debug.Log("SetOwner " + newOwner);
        _owner = newOwner;
        _iManager = _owner.gameObject.GetComponent<InteractionManager>();
    }

    public void SetMode(WeaponMode mode)
    {
        Debug.Log("ejecutando SetMode " + mode);
        _mode = mode;
    }
}
