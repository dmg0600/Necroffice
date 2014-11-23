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
    [HideInInspector]
    public Transform DangerousPoint;
    [HideInInspector]
    public bool _attacking;

    public int Damage = 0;

    public void Awake()
    {
        DangerousPoint = this.transform.FindChild("DangerousPoint");
    }

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

    void Start()
    {
        if(_range == 0)
        {
            Debug.LogError("ERROR!!! ARMA CON RANGO 0, ESTO ESTA PROHIBIDO POR DISEÑO! CACA! FUERAAAAAA");
        }
    }

    public virtual void move()
    {
        
        Vector3 direction = (GameManager.Instance.Player.transform.position - owner.transform.position).normalized;

        int layermask = ~(1 << LayerMask.NameToLayer("Creature") | 1 << LayerMask.NameToLayer("Player") | 1 << LayerMask.NameToLayer("Weapon"));

        

        float distance = Vector3.Distance(owner.transform.position, GameManager.Instance.Player.transform.position);
/*

        RaycastHit[] hits;
        hits = Physics.RaycastAll(owner.transform.position, direction, (_visionRange > distance) ? distance : _visionRange, layermask);
        int i = 0;
        while (i < hits.Length)
        {
            RaycastHit hit = hits[i];
            Debug.Log(hit.collider.gameObject.name);
            i++;
        }
        Debug.Log("----------------------------------------- ");
*/

        if (!Physics.Raycast(owner.transform.position, direction, (_visionRange > distance) ? distance: _visionRange , layermask))
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
            _iManager = _owner.gameObject.GetComponent<InteractionManager>();
            _range += 1;
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
        owner = newOwner;
        
    }

    public void SetMode(WeaponMode mode)
    {
        weaponMode = mode;
    }
}
