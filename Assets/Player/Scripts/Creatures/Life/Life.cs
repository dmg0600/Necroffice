using UnityEngine;
using System.Collections;

public class Life : MonoBehaviour
{
    public Stats.Attribute life;

    bool destroyinh = false;

    public void OnDamage(Hitbox hitbox)
    {
        life.sub(hitbox.Damage);

        InteractiveObject _iObject = GetComponent<InteractiveObject>();
        if (_iObject != null)
            _iObject.DamagedByHitbox(hitbox);

        if (life.isLower)
        {
            if (!destroyinh)
            {
                destroyinh = true;
                transform.BroadcastMessage("OnDead");
            }
        }
    }

    public void OnHeal(int cure)
    {
        life.add(cure);

        if (life.isMax)
            transform.root.BroadcastMessage("OnPlentyLife");
    }
}
