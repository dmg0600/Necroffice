using UnityEngine;
using System.Collections;

public class Life : MonoBehaviour
{
    public Stats.Attribute life;

    bool destroyinh = false;

    public void OnDamage(Hitbox hitbox)
    {
        life.sub(hitbox.Damage);

        if (life.isLower && !destroyinh)
        {
            destroyinh = true;
            Debug.Log("se llama al OnDead");
            transform.root.BroadcastMessage("OnDead");
        }
    }

    public void OnHeal(int cure)
    {
        life.add(cure);

        if (life.isMax)
            transform.root.BroadcastMessage("OnPlentyLife");
    }
}
