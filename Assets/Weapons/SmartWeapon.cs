using UnityEngine;
using System.Collections;

public abstract class SmartWeapon : MonoBehaviour 
{
    public GameObject owner;
    public GameObject target;

    public abstract bool canAttack();
    public abstract bool attack();
    public abstract bool nextTarget();
}
