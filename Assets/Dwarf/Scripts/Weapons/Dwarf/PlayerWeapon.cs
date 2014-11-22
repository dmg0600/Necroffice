using UnityEngine;
using System.Collections;

public abstract class PlayerWeapon : MonoBehaviour, SmartWeapon
{
    public abstract bool canAttack();
    public abstract void attack();
}
