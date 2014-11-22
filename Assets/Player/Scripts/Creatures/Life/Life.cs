using UnityEngine;
using System.Collections;

public class Life : MonoBehaviour 
{
    public Stats.Attribute life;

    public void OnDamage(int damage) 
    {
        life.sub(damage);

        if (life.isLower)
           transform.root.BroadcastMessage("OnDead");
    }

    public void OnHeal(int cure)
    {
        life.add(cure);

        if (life.isMax)
            transform.root.BroadcastMessage("OnPlentyLife");
    }
}
