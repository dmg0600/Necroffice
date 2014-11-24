using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public enum WeaponMode
{
    CONTROLLED,
    AI,
    not
}

public abstract class Weapon : MonoBehaviour
{
    [HideInInspector]
    public Transform DangerousPoint;

    public Hitbox Hitbox;

    [HideInInspector]
    public bool _attacking;

    public void Awake()
    {
        DangerousPoint = this.transform.FindChild("DangerousPoint");
    }

    #region unimplemented methods

    public IEnumerator atackHandler()
    {
        if (!_attacking)
        {
            _attacking = true;
            yield return StartCoroutine(attack());
            _attacking = false;
        }
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

    void Start()
    {
        if (_range == 0)
        {
            Debug.LogError("ERROR!!! ARMA CON RANGO 0, ESTO ESTA PROHIBIDO POR DISEÑO! CACA! FUERAAAAAA");
        }

        Hitbox.gameObject.SetActive(false);
    }

    public virtual void move()
    {

        Vector3 direction = (GameManager.Instance.Player.transform.position - owner.transform.position).normalized;

        int layermask = ~(1 << LayerMask.NameToLayer("Creature") | 1 << LayerMask.NameToLayer("Player") | 1 << LayerMask.NameToLayer("Weapon"));

        float distance = Vector3.Distance(owner.transform.position, GameManager.Instance.Player.transform.position);

        if (!Physics.Raycast(owner.transform.position, direction, (_visionRange > distance) ? distance : _visionRange, layermask))
            owner.BroadcastMessage("OnInputAxis", direction);

    }


    protected void idle()
    {
        if (objective != Vector3.zero && Vector3.Distance(owner.transform.position, objective) < 2.0f)
        {
            Vector3 direction = (objective - owner.transform.position).normalized;
            owner.BroadcastMessage("OnInputAxis", direction);
        }
        else
        {
            CancelInvoke();
            selectObjective();
        }
    }

    protected void selectObjective()
    {
        int layermask = ~(1 << LayerMask.NameToLayer("Creature") | 1 << LayerMask.NameToLayer("Player") | 1 << LayerMask.NameToLayer("Weapon"));

        float maxDistance = 0;
        Vector3 direction;
        Vector3 currentDir = Vector3.zero;

        RaycastHit hit;
        while (true)
        {
            direction = Vector3.forward;
            if (!Physics.Raycast(transform.position, direction, out hit))
            {
                objective = direction;
                break;
            }

            if (hit.distance > maxDistance)
            {
                maxDistance = hit.distance;
                currentDir = direction;
            }

            direction = Vector3.back;
            if (!Physics.Raycast(transform.position, direction, out hit))
            {
                objective = direction;
                break;
            }
            if (hit.distance > maxDistance)
            {
                maxDistance = hit.distance;
                currentDir = direction;
            }

            direction = Vector3.left;
            if (!Physics.Raycast(transform.position, direction, out hit))
            {
                objective = direction;
                break;
            }
            if (hit.distance > maxDistance)
            {
                maxDistance = hit.distance;
                currentDir = direction;
            }

            direction = Vector3.right;
            if (!Physics.Raycast(transform.position, direction, out hit))
            {
                objective = direction;
                break;
            }
            if (hit.distance > maxDistance)
            {
                maxDistance = hit.distance;
                currentDir = direction;
            }

            break;
        }

        owner.BroadcastMessage("OnInputAxis", currentDir);

        Invoke("selectObjective", 5.0f);
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
            _iManager = _owner.gameObject.GetComponent<InteractionManager>();
            _range += 1;
            Hitbox.Owner = value;
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

    public int DamageRanged = 0;
    public int VelocityRanged = 0;

    public int Range
    {
        get
        {
            return _range;
        }
    }
    #endregion

    public void SetOwner(Creature newOwner)
    {
        owner = newOwner;

    }

    public void SetMode(WeaponMode mode)
    {
        weaponMode = mode;
    }

    [SerializeField]
    private string _name;
    [SerializeField]
    public Texture _icon;
    [SerializeField]
    private int _powerBonus = 0;
    [SerializeField]
    private int _agilityBonus = 0;
    [SerializeField]
    private int _range = 0;
    [SerializeField]
    private float _visionRange = 15.0f;

    public GameObject _weaponPickeable;

    private GameObject _currentTarget = null;

    [HideInInspector]
    public Creature _owner;

    private int _currentCorner;
    private NavMeshPath _currentPath;

    private InteractionManager _iManager;
    private WeaponMode _mode = WeaponMode.CONTROLLED;

    private Vector3 objective = Vector3.zero;

    public InteractiveObject.Properties[] Property;
}
