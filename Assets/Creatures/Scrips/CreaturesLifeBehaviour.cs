using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[RequireComponent(typeof(Life))]
public class CreaturesLifeBehaviour : LifeBehaviour
{
    Life Life;

    //<HACK>
    //bool wait = false;
    //public void Update()
    //{
    //    if (!wait) StartCoroutine(FalseLife());
    //}
    //IEnumerator FalseLife()
    //{
    //    wait = true;
    //    yield return new WaitForSeconds(Random.Range(2, 5));
    //    Life.OnDamage(20);
    //    wait = false;
    //}
    //</HACK>

    public void Awake()
    {
        Life = GetComponent<Life>();

        OnRespawn();

    }

    public override void OnDead() 
    {
    }

    public override void OnRespawn()
    {
        transform.root.GetComponent<Stats>().RamdomStats();
        Life.life.Regenerate();

        GameObject[] weapons = Resources.LoadAll<GameObject>("Weapons");
        GameObject choosen = weapons[Random.Range(0, weapons.Count())];

        GameObject weaponFinal = GameObject.Instantiate(choosen,Vector3.zero, Quaternion.identity) as GameObject;
        
        Creature creature = GetComponent<Creature>();

        weaponFinal.transform.parent = transform.root;

        Debug.Log("arma creada " + weaponFinal);  

        if(creature != null)
        {
            weaponFinal.BroadcastMessage("SetOwner", creature);
            weaponFinal.BroadcastMessage("SetMode", WeaponMode.AI);
        }
    }
}
