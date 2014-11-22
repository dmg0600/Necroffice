using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Stats : MonoBehaviour
{
    public Attribute Agility = new Attribute(0, 5);
    public Attribute Power = new Attribute(0, 5);
    #region InteractionManagerGizmos
    /// <summary>
    /// Color para pintar las esferas de feedback cercano
    /// </summary>
    private Color m_NearFeedbackColor = new Color(1.0f, 0.0f, 0, 0.5f);

    /// <summary>
    /// El player puede usar éste objeto
    /// </summary>
    public bool m_Usable = false;

    /// <summary>
    /// Radio de interacción
    /// </summary>
    public float m_InteractionRadius = 0.0f;

    /// <summary>
    /// Ángulo de interacción para el objeto
    /// </summary>
    public float m_InteractionAngle = 180.0f;

    /// <summary>
    /// Función virtual que se llamará cuando el player use un objeto
    /// </summary>    
    public virtual void OnUse()
    {

    }

    /// <summary>
    /// Dibuja información de debug por pantalla
    /// </summary>
    void OnDrawGizmos()
    {
        Quaternion rotateQuat = Quaternion.AngleAxis(-m_InteractionAngle * 0.5f, Vector3.up);
        Vector3 vectorToPaint = rotateQuat * transform.forward;
        Vector3 lPoint = transform.position + (vectorToPaint.normalized * 6.0f);
        Gizmos.DrawLine(transform.position, lPoint);

        rotateQuat = Quaternion.AngleAxis(m_InteractionAngle * 0.5f, Vector3.up);
        vectorToPaint = rotateQuat * transform.forward;
        Vector3 rPoint = transform.position + (vectorToPaint.normalized * 6.0f);
        Gizmos.DrawLine(transform.position, rPoint);

        Gizmos.color = m_NearFeedbackColor;
        Gizmos.DrawSphere(transform.position, m_InteractionRadius);
    }
    #endregion

    #region Attribute
    [System.Serializable]
    public class Attribute
    {
        int upper = 5;
        int lower = 1;

        public int value = 1;
        public int max = 1;

        public Attribute() { }

        public Attribute(int minValue, int maxValue)
        {
            upper = maxValue;
            lower = minValue;
            Regenerate();
        }

        public Attribute(int maxValue, int minValue, int newval)
        {
            upper = maxValue;
            lower = minValue;

            if (validateValue(newval))
                max = newval;
            else
                Reset();
        }

        public void Regenerate()
        {
            value = max;
        }

        public void Reset()
        {
            value = lower;
        }

        public void ramdomValue()
        {
            value = Random.Range(upper, lower);
        }

        public bool validateValue(int num, bool inclusive = true)
        {
            return inclusive
                ? lower <= num && num <= upper
                : lower < num && num < upper;
        }

        public float ChangeScale(int NewMin, int NewMax)
        {
            return (((value - lower) * (NewMax - NewMin)) / (NewMax - lower)) + NewMin;
        }

        public float Get01Value()
        {
            return (value * 1f / upper);
        }

        public void sub(int i)
        {
            this.value = Mathf.Clamp(this.value - i, this.lower, this.max);
        }

        public void add(int i)
        {
            this.value = Mathf.Clamp(this.value + i, this.lower, this.max);
        }

        public bool isUpper { get { return value >= upper; } }
        public bool isLower { get { return value <= lower; } }
        public bool isMax { get { return value >= max; } }
    }
    #endregion
}
