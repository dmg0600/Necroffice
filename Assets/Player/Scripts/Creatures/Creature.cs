using UnityEngine;
using System.Collections;

public class Creature : MonoBehaviour
{
    public enum Alignments { PLAYER = 0, NEUTRAL, ENEMY };

    public Alignments Alignment = Alignments.ENEMY;

    public Transform WeaponHoldPoint;

    public Weapon _Weapon;

    [HideInInspector]
    public Controller _Control;

    [HideInInspector]
    public Stats _Stats;

    bool isPlayer = false;


    void Awake()
    {
        _Control = GetComponent<Controller>();
        _Stats = GetComponent<Stats>();
        if (GetComponent<InputManager>() != null)
            isPlayer = true;
    }

    void Start()
    {
        EquipWeapon(GameManager.Instance.DefaultWeapon);
    }

    void EquipWeapon(Weapon newWeapon)
    {
        if (_Weapon != null && _Weapon != GameManager.Instance.DefaultWeapon)
        {
            DropWeapon();
        }

        //Poner nueva
        GameObject _obj = Instantiate(newWeapon.gameObject, WeaponHoldPoint.position, WeaponHoldPoint.rotation) as GameObject;
        _obj.transform.parent = WeaponHoldPoint.transform;
        _obj.transform.localPosition = Vector3.zero;
        _obj.transform.localScale = Vector3.one;      //Todas las armas tienen que tener escala (1,1,1)!

        _Weapon = _obj.GetComponent<Weapon>();

        _Weapon.owner = this;

        if (IsPlayer())
        {
            _Weapon.weaponMode = WeaponMode.CONTROLLED;
        }
        else
        {
            _Weapon.weaponMode = WeaponMode.AI;
        }
    }

    void DropWeapon()
    {
        //todo: soltar como objeto en vez de destruir

        Destroy(_Weapon.gameObject);

        _Weapon = null;
    }

    void OnInputMouseClick(Vector3 clickPoint)
    {
        if (_Weapon != null)
            _Weapon.attack();


        //    Vector3 _attackingDirection = transform.forward;
        //    _attackingDirection.y = 0;
        //    _attackingDirection *= 8;

        //    GameManager.Instance.CreateHitbox(GetComponent<Creature>(), 1, 1, _attackingDirection);
    }

    public bool IsPlayer()
    {
        return isPlayer;
    }

    public void OnDead()
    {
        Debug.Log("Se muere " + name);

        if (IsPlayer())
        {
            //Muere player 
            GameManager.Instance.DestroyWithParticle("BloodSplat", gameObject);
        }
        else
        {
            //Muere enemigo
            GameManager.Instance.DestroyWithParticle("BloodSplat", gameObject);
        }
    }
}
