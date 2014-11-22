using UnityEngine;
using System.Collections;

public class Life : MonoBehaviour 
{
    public PlayerLifeManager LifeManager;
    public Stats.Attribute life;

    void Awake() 
    {
        if (LifeManager == null) Debug.Log("Life of " + transform.root.gameObject.name + " must have a LifeManager");
    }

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
