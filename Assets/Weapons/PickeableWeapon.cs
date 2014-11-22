using UnityEngine;
using System.Collections;

public class PickeableWeapon : MonoBehaviour {


    public delegate void WeaponPickedHandler(GameObject who);
    public event WeaponPickedHandler weaponPicked;

	void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Controller>() == null) return;

        //avisamos el player
        if (weaponPicked != null)
        {
            weaponPicked(collision.gameObject);
        }
    }
}
