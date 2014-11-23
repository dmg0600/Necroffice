using UnityEngine;
using System.Collections;

public class BriefcaseWeapon : Weapon 
{
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
