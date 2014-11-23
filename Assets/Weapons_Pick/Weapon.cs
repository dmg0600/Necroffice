using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public enum WeaponMode
{
    CONTROLLED,
    AI
}

public abstract class Weapon : MonoBehaviour
{
    private bool _attacking;

    #region unimplemented methods

    public IEnumerator atackHandler() 
    {
        _attacking = true;
        yield return StartCoroutine(attack());
        _attacking = false;

        //Esto se pone aqui para no ponerse al final de cada attack
        //Aqui pasa igual que el OnAttack y OnAttackStart en contoller, 
        //pero interesa que varias cosas reciban este evento, beach
        _owner.BroadcastMessage("OnAttackEnd");
    }

    abstract public IEnumerator attack();
    abstract public bool canAttack();

    //IA METHODS
    abstract public void updateAI();

    #endregion

    public virtual void selectTarget()
    {
    }

    public virtual void move()
    {
        Vector3 direction = (GameManager.Instance.Player.transform.position - owner.transform.position).normalized;

        int layermask = ~(1 << LayerMask.NameToLayer("Creature"));
        if (Physics.Raycast(owner.transform.position, direction, _visionRange, layermask))
            owner.BroadcastMessage("OnInputAxis", direction); 
    }

    public InteractiveObject.Properties[] Property;

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
                Invoke("selectTarget", 2.0f);
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

    public int Agility
    {
        get
        {
            return _agilityBonus;
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
    [SerializeField]
    public Texture _icon;
    [SerializeField]
    private float _visionRange = 15.0f;



    private GameObject _currentTarget = null;
    public Creature _owner;

    private int _currentCorner;
    private NavMeshPath _currentPath;

    private InteractionManager _iManager;
    private WeaponMode _mode = WeaponMode.CONTROLLED;

    public void SetOwner(Creature newOwner)
    {
        _owner = newOwner;
        _iManager = _owner.gameObject.GetComponent<InteractionManager>();
    }

    public void SetMode(WeaponMode mode)
    {
        weaponMode = mode;
    }
}
