using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{
    [System.Serializable]
    public class AIParameters
    {
        public int cosas = 0;
    }

    public string Name;
    public int PowerBonus = 0;
    public int AgilityBonus = 0;

    public AIParameters AI = new AIParameters();
}
