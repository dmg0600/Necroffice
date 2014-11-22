using UnityEngine;
using System.Collections;

public abstract class IAWeapon : MonoBehaviour, SmartWeapon
{
    public abstract bool canAttack();
    public abstract void attack();
}
