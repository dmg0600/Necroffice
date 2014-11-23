using UnityEngine;
using System.Collections;

public class Life : MonoBehaviour
{
    public Stats.Attribute life;

    bool destroyinh = false;
    bool onEvent = false;

    public void OnDamage(Hitbox hitbox)
    {
        InteractiveObject _iObject = GetComponent<InteractiveObject>();
        if (_iObject != null)
            _iObject.DamagedByHitbox(hitbox);

        OnDamage(hitbox.Damage);
    }

    public void OnDamage(int damage)
    {
        life.sub(damage);
        GetComponent<Controller>()._Animator.SetBool("Damaged", false);

        if (life.isLower)
        {
            if (!destroyinh)
            {
                destroyinh = true;
                transform.BroadcastMessage("OnDead");
                GetComponent<Controller>()._Animator.SetBool("Death", true);
                destroyinh = false;
            }
        }
        else
        {
            GetComponent<Controller>()._Animator.SetBool("Damaged", true);
        }
    }


    public void OnHeal(int cure)
    {
        life.add(cure);

        if (life.isMax)
            transform.root.BroadcastMessage("OnPlentyLife");

    }
}
