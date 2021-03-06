﻿using UnityEngine;
using System.Collections;

public abstract class Creature : MonoBehaviour
{

    public AudioClip DwarfDeadAudio;
    public AudioClip SkeletonDeadAudio;

    public enum Alignments { PLAYER = 0, NEUTRAL, ENEMY };

    public Alignments Alignment = Alignments.ENEMY;

    public GameObject WeaponHoldPoint;

    public Weapon _Weapon;

    public string Name = "Player";

    [HideInInspector]
    public Controller _Control;

    [HideInInspector]
    public Stats _Stats;

    [HideInInspector]
    public Life _Life;
    bool _attacking = false;

    void Awake()
    {
        _Control = GetComponent<Controller>();

        _Stats = GetComponent<Stats>();

        _Life = GetComponent<Life>();
    }

    void Start()
    {
        initialize();
    }
    protected virtual void initialize()
    {
        EquipWeapon(GameManager.Instance.DefaultWeapon);
    }

    public void EquipWeapon(Weapon newWeapon)
    {
        if (_Weapon != null)
        {
            DropWeapon();
        }
        //Poner nueva
        GameObject _obj = Instantiate(newWeapon.gameObject, WeaponHoldPoint.transform.position, WeaponHoldPoint.transform.rotation) as GameObject;
        _obj.transform.parent = WeaponHoldPoint.transform;
        //_obj.transform.localPosition = Vector3.zero;
        //_obj.transform.localScale = Vector3.one;      //Todas las armas tienen que tener escala (1,1,1)!

        _Weapon = _obj.GetComponent<Weapon>();

        _Weapon.owner = this;
    }

    void DropWeapon()
    {
        //todo: soltar como objeto en vez de destruir

        Destroy(_Weapon.gameObject);

        //GameObject.Instantiate(_Weapon._weaponPickeable, transform.position - Vector3.back, transform.rotation);

        _Weapon = null;
    }

    public void OnAttack(Vector3 clickPoint)
    {
        if (_Weapon != null)
            StartCoroutine(_Weapon.attackHandler());
    }

    public abstract bool IsPlayer();

    public abstract void OnDead();

    public string GenerateDwarvenName()
    {
        //Nombres
        string[] _names = new string[] { "Ulan", "Fikden", "Iolkhan", "Gegdo", "Glorirgoid ", "Groornuki ", "Snavromi ", "Brufirlum ", "Dumroir ", "Mogis", "Galtharm", "Huriuryl", "Kramohm", "Bandrus", "Sogthorm", "Karahrgrum", "Muirihrgrun", "Grunni", "Amkahm", "Beldur", "Toriamand", "Urmohan", "Orimiggs", "Bramoumiir", "Harthran", "Grenahgrom" };

        //Apellidos
        string[] _surnames = new string[] { "Trollrock", "Flaskmaker", "Brickarmour", "Chainflayer", "Axerock", "Chaosbreaker", "Pebblehorn", "Boneforged", "Koboldbelly", "Honorcloak", "Cliffbeard", "Marblebranch", "Marbledust", "Coldbreaker", "Stormbranch", "Palegem", "Palebelly", "Bronzesteel", "Highbraid", "Thunderroar", "Barleybreaker", "Blackbreaker", "Deepaxe", "Marblebeard", "Mountainbelly", "Ironriver" };


        string _fName = _names[UnityEngine.Random.Range(0, _names.Length)];
        string _fSurname = _surnames[UnityEngine.Random.Range(0, _surnames.Length)];
        return _fName + " " + _fSurname;
    }

    public void GeneratePlayerDwarf()
    {
        Name = GenerateDwarvenName();
        //Debug.Log("Generado nombre");

        int _maxLife = Random.Range(40, 45);

        _maxLife -= (_Stats.Agility.value + _Stats.Power.value);

        _Life.life = new Stats.Attribute(0, _maxLife);

    }

    public bool imAttacking()
    {
        return _attacking;
    }

    public void attacking(bool attacking)
    {
        _attacking = attacking;
    }
}
