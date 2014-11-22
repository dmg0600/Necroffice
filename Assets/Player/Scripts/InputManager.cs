using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class InputManager : MonoBehaviour
{
    GameObject Sujet;

    [SerializeField]
    GameObject RotateRef;

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
                Sujet.BroadcastMessage("OnInputMouseClick", hit.point);
        }

        Vector3 axis = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;

        if (axis != Vector3.zero)
        {
            Quaternion rotate = Quaternion.LookRotation(new Vector3(RotateRef.transform.forward.x, 0, RotateRef.transform.forward.z));
            axis = rotate * axis;
            Sujet.BroadcastMessage("OnInputAxis", axis);
        }

    }
}
