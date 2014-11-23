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
    #region unimplemented methods
    abstract public void attack();
    abstract public bool canAttack();

    //IA METHODS
    abstract public void updateAI();

    #endregion

    public virtual void selectTarget()
    {


        _currentPath = new NavMeshPath();
        NavMesh.CalculatePath(transform.position, GameManager.Instance.Player.transform.position, -1, _currentPath);

        foreach(Vector3 point in _currentPath.corners)
        {

            Debug.Log(point);
        }

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
        //Invoke("selectTarget", 1.0f);
        
    }

    public virtual void move()
    {
        Vector3 direction = _currentPath.corners[_currentCorner] - owner.transform.position;
        direction.y = 0;
        //Debug.Log(_currentPath.corners[_currentCorner]);

        //Debug.DrawRay(owner.transform.position,direction);

       /* if (Vector3.Distance(_currentPath.corners[_currentCorner], transform.position) < 1.0)
            direction = _currentPath.corners[++_currentCorner] - transform.position;*/

        owner.GetComponent<Controller>().OnInputAxis(direction);

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
