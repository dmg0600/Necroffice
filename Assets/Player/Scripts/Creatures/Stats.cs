using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Stats : MonoBehaviour
{
    [System.Serializable]
    public class Attribute
    {
        public int max = 0;
        public int current = 0;
        public Attribute() { }
        public Attribute(int maxValue)
        {
            max = maxValue;
        }
        public void Regenerate()
        {
            current = max;
        }
    }

    public Attribute Agility = new Attribute(2);

    public Attribute Power = new Attribute(2);
}
