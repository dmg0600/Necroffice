using UnityEngine;
using System.Collections;

public class ControlledCreature : Creature {
    protected override void initialize()
    {
        GeneratePlayerDwarf();
        base.initialize();
        InputManager.Instance.RegisterKeyDown("Fire1", OnAction);
    }

    public override void OnDead()
    {
        //Muere enemigo
        GameManager.Instance.DestroyWithParticle("BloodSplat", gameObject);
        //AudioSource.PlayClipAtPoint(SkeletonDeadAudio, transform.position);
    }


    //The purpose of this method is to handle more actions in the future
    public void OnAction(string action)
    {
        StartCoroutine(_Weapon.attack());
    }
    public override bool IsPlayer()
    {
        return true;
    }
}
