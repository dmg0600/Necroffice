using UnityEngine;
using System.Collections;

public class Life : MonoBehaviour 
{
    public Stats.Attribute life;

    public void OnDamage(int damage) 
    {
        life.sub(damage);

        if (life.isLower)
            OnDeath();
    }

    public virtual void OnDeath()  
    {

    }

    public virtual void OnSpawn() 
    {

    }
}
