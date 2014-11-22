using UnityEngine;
using System.Collections;

public class AttackController : MonoBehaviour 
{
    SmartWeapon weapon = null;
    float halfH = 0f;

    void Awake() 
    {
        var c = gameObject.collider as CapsuleCollider;
        if(c != null) halfH = c.height * 0.5f;
    }

    public void OnInputMouseClick(object o)
    {
        Vector3 objetive = (Vector3)o;

        if (objetive != null && weapon != null)
        {
            Debug.DrawLine(transform.position, objetive + new Vector3(0, halfH, 0), Color.red, Mathf.Infinity);
            //TODO: Attack Logic
        }
    }
}
