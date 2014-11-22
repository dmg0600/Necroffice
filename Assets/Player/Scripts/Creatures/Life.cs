using UnityEngine;
using System.Collections;

public class Life : MonoBehaviour 
{
    public Stats.Attribute life;

    public void OnDamage(int damage) 
    {
        //life -= damage;
    }

    public void OnDeath() 
    {

    }

    public void OnSpawn() 
    {

    }
}
