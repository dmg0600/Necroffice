using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class MouseLogic : MonoBehaviour
{
    public GameObject Floor;
    GameObject Sujet;

    void Awake() 
    {
        Sujet = this.transform.root.gameObject;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Floor.collider.Raycast(ray, out hit, Mathf.Infinity)) 
                Sujet.BroadcastMessage("OnObjetive", hit.point);
        }
    }
}
