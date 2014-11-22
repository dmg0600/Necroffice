using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum WeaponMode
{
    CONTROLLED,
    AI
}

public abstract class Weapon : MonoBehaviour
{
    abstract public void attack();
    abstract public bool canAttack();

    //IA METHODS
    abstract public void updateAI();
    abstract public void selectTarget();
    abstract public void move();

    public void updateTargets();

    public WeaponMode weaponMode
    {
        set
        {
            this._mode = value;
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

    public GameObject owner
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

    [SerializeField]
    private string _name;
    [SerializeField]
    private int _powerBonus = 0;
    [SerializeField]
    private int _agilityBonus = 0;
    [SerializeField]
    private int _range = 0;

    private GameObject _currentTarget = null;
    private GameObject _owner;
    protected List<GameObject> _targetList;

    private WeaponMode _mode;

}
