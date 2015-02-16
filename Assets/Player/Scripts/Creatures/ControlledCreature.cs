using UnityEngine;
using System.Collections;

public class ControlledCreature : Creature {
    protected void initialize()
    {
        GeneratePlayerDwarf();
        base.initialize();
    }

    public override void OnDead()
    {
        //Muere enemigo
        GameManager.Instance.DestroyWithParticle("BloodSplat", gameObject);
        //AudioSource.PlayClipAtPoint(SkeletonDeadAudio, transform.position);
    }
}
