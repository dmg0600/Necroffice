using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class MouseLogic : MonoBehaviour
{
    GameObject Sujet;
    Camera camera;

    int floorLayer;

    void Awake() 
    {
        Sujet = this.transform.root.gameObject;
        camera = this.gameObject.GetComponent<Camera>();
        floorLayer = 1 << LayerMask.NameToLayer("Floor");
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, floorLayer))
                hit.transform.gameObject.BroadcastMessage("OnObjective", hit.point);
        }
    }
}
