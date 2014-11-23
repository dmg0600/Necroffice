using UnityEngine;
using System.Collections;

public class PowerUp_Items : MonoBehaviour
{


    public int Cure;

    public bool Damage = false;

    void OnTriggerEnter(Collider col)
    {
        if (col.name == Defines.Player)
        {

            if (!Damage)
            {
                GameManager.Instance.Player.GetComponent<Life>().OnHeal(Cure);
            }
            else
                GameManager.Instance.Player.GetComponent<Life>().OnDamage(Cure);

            Destroy(this.gameObject);
        }
    }

}
