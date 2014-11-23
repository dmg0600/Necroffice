using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[RequireComponent(typeof(Life))]
public class PlayerLifeBehaviour : LifeBehaviour
{
    public GameObject RespawnPoint;
    Life Life;

    public void Awake()
    {
        Life = GetComponent<Life>();
        GameObject[] Respawn = GameObject.FindGameObjectsWithTag("Respawn");

        if(Respawn.Length != 1) Debug.Log("Must be just and only 1 Player Respawn point in scene, bitch");

        RespawnPoint = Respawn.FirstOrDefault();

    }

    void Start()
    {
        if (RespawnPoint != null) OnRespawn();
    }

    public override IEnumerator OnDead() 
    {
        if (imDead) yield break;
        imDead = true;

        //Corrutina de la muerte
        yield return StartCoroutine(GameManager.Instance.CorrutinaDeLaMuerte());

        //Respawn Player
        StartCoroutine(OnRespawn());
    }

    public override IEnumerator OnRespawn()
    {
        if (!imDead) yield break;
        imDead = false;

        if (RespawnPoint != null)
            transform.root.position = RespawnPoint.transform.position;

        Life.life.Regenerate();
        transform.root.GetComponent<Stats>().RamdomStats();

        GetComponent<Creature>().EquipWeapon(GameManager.Instance.DefaultWeapon);

        //TODO: Quitar la Weapon y poner periodico

        yield break;
    }
}
