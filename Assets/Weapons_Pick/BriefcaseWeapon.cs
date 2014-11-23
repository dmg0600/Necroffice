using UnityEngine;
using System.Collections;

public class BriefcaseWeapon : Weapon {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    override public IEnumerator attack()
    {
        //Quitar si no hace falta
        yield break;
    }

    override public bool canAttack()
    {
        return true;
    }

    override public void updateAI()
    {
    }

}
