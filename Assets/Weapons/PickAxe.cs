using UnityEngine;
using System.Collections;

public class PickAxe : MonoBehaviour
{
    void Start()
    {
        gameObject.GetComponent<PickeableWeapon>().weaponPicked += this.weaponPicked;
    }

    void weaponPicked(GameObject who)
    {
        GameObject.Instantiate(AxeAttachable);
        who.GetComponent<AxeWeapon>().owner = who.GetComponent<Creature>();

        GameObject.Destroy(this.gameObject);
    }

    [SerializeField]
    GameObject AxeAttachable;
}
