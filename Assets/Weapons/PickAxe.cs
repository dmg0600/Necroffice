using UnityEngine;
using System.Collections;

public class PickAxe : MonoBehaviour {

	// Use this for initialization
	void Start () {
        gameObject.GetComponent<PickeableWeapon>().weaponPicked += this.weaponPicked;
	}
	
	// Update is called once per frame
	void weaponPicked (GameObject who) {
        GameObject.Instantiate(AxeAttachable);
        who.GetComponent<AxeWeapon>().owner = who;

        GameObject.Destroy(this.gameObject);
	}

    [SerializeField]
    GameObject AxeAttachable;
}
