using UnityEngine;
using System.Collections;

public class Creature : MonoBehaviour
{
    public enum Alignments { PLAYER = 0, NEUTRAL, ENEMY };

    public Alignments Alignment = Alignments.ENEMY;

    public Weapon _Weapon;

    [HideInInspector]
    public Controller _Control;

    [HideInInspector]
    public Stats _Stats;

    void Awake()
    {
        _Control = GetComponent<Controller>();
        _Stats = GetComponent<Stats>();
    }

}
