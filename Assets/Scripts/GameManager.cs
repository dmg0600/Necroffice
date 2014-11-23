using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GameManager : MonoBehaviour
{
    #region Singleton

    public static GameManager Instance = null;

    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Ya hay más de un GameManager en " + Application.loadedLevelName);
            Destroy(gameObject);
        }
        Instance = this;
    }

    #endregion

    public GameObject HitboxPrefab;
    public Weapon DefaultWeapon;
    public Creature Player;

    public Creature Corpse;

    public TweenAlpha FadeCurtain;
    public UILabel EnterLevelLabel;
    public TweenPosition EnterLevelLabelTween;

    public PauseMenu PauseMenu;

    public ParticleSFX[] Particles;
    public Level[] Levels;

    [HideInInspector]
    public int CurrentlyLoadedLevel = -1;


    public void CreateHitbox(Weapon ownerWeapon, float radius, int damage, Vector3 velocity, float duration, string nameOfHitbox = null)
    {
        GameObject _obj = Instantiate(HitboxPrefab, ownerWeapon.owner.transform.position + (ownerWeapon.owner.transform.forward * 0.5f), Quaternion.identity) as GameObject;
        Hitbox _hitbox = _obj.GetComponent<Hitbox>();

        _hitbox.Owner = ownerWeapon.owner;
        _hitbox.Radius = radius;
        _hitbox.Damage = damage;
        _hitbox.Duration = duration;
        _hitbox.name = nameOfHitbox ?? "Hitbox (" + ownerWeapon.owner.name + ")";
        _hitbox.Properties = ownerWeapon.Property.ToList();

        _hitbox.SetVelocity(velocity);

        _hitbox.Begin();
    }

    public void CreateHitbox(Creature owner, float radius, int damage, Vector3 velocity, float duration, string nameOfHitbox = null)
    {
        GameObject _obj = Instantiate(HitboxPrefab, owner.transform.position + (owner.transform.forward * 0.5f), Quaternion.identity) as GameObject;
        Hitbox _hitbox = _obj.GetComponent<Hitbox>();

        _hitbox.Owner = owner;
        _hitbox.Radius = radius;
        _hitbox.Damage = damage;
        _hitbox.Duration = duration;
        _hitbox.name = nameOfHitbox ?? "Hitbox (" + owner.name + ")";

        _hitbox.SetVelocity(velocity);

        _hitbox.Begin();
    }

    public void CreateHitbox(Transform origin, float radius, int damage, float duration, string nameOfHitbox = null)
    {
        GameObject _obj = Instantiate(HitboxPrefab, origin.position, Quaternion.identity) as GameObject;
        Hitbox _hitbox = _obj.GetComponent<Hitbox>();

        _hitbox.Owner = null;
        _hitbox.Radius = radius;
        _hitbox.Damage = damage;
        _hitbox.Duration = duration;
        _hitbox.name = nameOfHitbox ?? "Hitbox";


        _hitbox.Begin();
    }

    public void CreateParticle(string Name, Vector3 position)
    {
        ParticleSFX _particle = Particles.FirstOrDefault(x => x.name == Name);
        if (_particle == null)
            return;

        GameObject _obj = Instantiate(_particle.gameObject, position, Quaternion.identity) as GameObject;
    }

    public void DestroyWithParticle(string Name, GameObject ObjectToDestroy)
    {
        ParticleSFX _particle = Particles.FirstOrDefault(x => x.name == Name);
        if (_particle == null)
            return;

        GameObject _obj = Instantiate(_particle.gameObject, ObjectToDestroy.transform.position, ObjectToDestroy.transform.rotation) as GameObject;
        ParticleSFX _sfxP = _obj.GetComponent<ParticleSFX>();
        _sfxP.ObjectToDestroy = ObjectToDestroy;
    }

    void Start()
    {
        FirstLevel();
    }

    void FirstLevel()
    {
        LoadLevel(0);
    }

    public void LoadLevel(int level)
    {
        if (_alreadyLoadingLevel)
            return;

        if (level < 0 || level >= Levels.Length)
        {
            Debug.LogError("Has pedido cargar el nivel " + level + " cuando sólo hay " + Levels.Length);
            return;
        }

        StartCoroutine(LoadLevelCoroutine(level));
    }

    static bool _alreadyLoadingLevel = false;
    IEnumerator LoadLevelCoroutine(int level)
    {
        EnableInput(false);

        _alreadyLoadingLevel = true;

        //Baja cortina
        FadeCurtain.Toggle(); yield return new WaitForSeconds(0.5f);

        //Player.gameObject.SetActive(true);
        GameManager.Instance.EnablePlayerNoDesactivate(true);

        if (CurrentlyLoadedLevel >= 0)
        {
            //Desactivar nivel previo
            //todo
        }

        //Comprobar entrada
        if (Levels[level].Start == null)
        {
            Debug.LogError("No has asignado el punto de entrada del nivel " + level + ":" + Levels[level].name);
            yield break;
        }

        //Cargar el nivel pedido
        Levels[level].gameObject.SetActive(true);
        CurrentlyLoadedLevel = level;

        //Player al punto de salida
        Player.transform.position = Levels[level].Start.position;
        Player.transform.rotation = Levels[level].Start.rotation;

        //Cámara
        Camera.main.transform.position = Player.transform.position;
        Camera.main.transform.position -= Camera.main.transform.forward * 25;

        //yield return new WaitForSeconds(0.5f);

        //Otras cosas al principio del nivel
        //...

        //Arriba cortina
        FadeCurtain.Toggle(); yield return new WaitForSeconds(0.5f);
        EnableInput(true);

        //letrerico de nivel por el que vas
        EnterLevelLabel.text = "Entering Level " + (level + 1);
        EnterLevelLabelTween.Toggle();

        //yield return new WaitForSeconds(5f);

        _alreadyLoadingLevel = false;
    }


    public void WinCurrentLevel()
    {
        int _levelToLoad = CurrentlyLoadedLevel + 1;

        if (_levelToLoad >= Levels.Length)
        {
            Defines.GameWin = true;
            Application.LoadLevel("Credits");
        }
        else
            LoadLevel(CurrentlyLoadedLevel + 1);
    }

    public void EnableInput(bool isEnable)
    {
        Player.GetComponent<InputManager>().EnableInput(isEnable);
        Camera.main.transform.GetComponent<PlayerCamera>().EnableInput(isEnable);
    }

    void Update()
    {
        //Pause
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!PauseMenu.gameObject.activeInHierarchy)
                PauseMenu.Open();
            else
                PauseMenu.Close();
        }


        //<HACK> CHetos!
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            WinCurrentLevel();
        }
    }

    public void EnablePlayerNoDesactivate(bool enable) 
    {
        Player.transform.Find("Model").transform.gameObject.SetActive(enable);
        Player.transform.collider.enabled = enable;
        EnableInput(enable);
    }

    /// <summary>
    /// Encargada del desplazamiento a un punto seguro 
    /// para el respawn de tu cadaver
    /// cinemática de la muerte y recarga del nivel actual
    /// </summary>
    /// <returns></returns>
    public IEnumerator CorrutinaDeLaMuerte()
    {
        float nearDistObj = Mathf.Infinity;
        float distance = 0.0f;
        float SizeVelocity = 2.0f;

        EnablePlayerNoDesactivate(false);

        //instanciamos las partículas de muerte
        ParticleSFX _particleFire = GameManager.Instance.Particles.FirstOrDefault(x => x.name == Defines.ParticleDeath);

        GameObject goSpawn = null;     

        //Buscamos el punto seguro más cercano.
        List<GameObject> go = GameObject.FindGameObjectsWithTag(Defines.PuntoSeguro).ToList();

        foreach (GameObject goAux in go)
        {
            distance = Vector3.Distance(Player.transform.position, goAux.transform.position);

            if (distance < nearDistObj)
            {
                goSpawn = goAux;
                nearDistObj = distance;
            }
        }

        //Trasladamos al player al pto seguro
        Player.transform.position = goSpawn.transform.position;
        //instanciamos el cadaver
        GameObject creature = GameObject.Instantiate(Corpse.gameObject, Player.transform.position, Player.transform.rotation) as GameObject;
        creature.transform.localScale = new Vector3(Defines.Scala01, Defines.Scala01, Defines.Scala01);

        while (creature.transform.localScale != Vector3.one)
        {
            creature.transform.localScale = Vector3.Lerp(creature.transform.localScale, Vector3.one, Time.fixedDeltaTime * SizeVelocity);
            yield return new WaitForEndOfFrame();
        }

        LoadLevel(CurrentlyLoadedLevel);
    }
}

