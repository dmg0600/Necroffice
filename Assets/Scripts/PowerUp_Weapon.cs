using UnityEngine;
using System.Collections;

public class PowerUp_Weapon : MonoBehaviour
{

    public Weapon pickWeapon;

    void OnTriggerEnter(Collider col)
    {
        if (col.name == Defines.Player)
        {
            col.gameObject.GetComponent<Creature>().EquipWeapon(pickWeapon);
            Destroy(this.gameObject);
        }
    }
}
