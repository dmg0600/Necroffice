using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class MouseLogic : MonoBehaviour
{
    GameObject Sujet;

    int floorLayer;

    void Awake() 
    {
        Sujet = this.transform.root.gameObject;

        floorLayer = 1 << LayerMask.NameToLayer("Floor");
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, floorLayer))
                Sujet.BroadcastMessage("OnMouseClick", hit.point);
        }
    }
}
