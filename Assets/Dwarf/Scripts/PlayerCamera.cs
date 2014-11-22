using UnityEngine;
using System.Collections;

public class PlayerCamera : MonoBehaviour
{
    public SpringPosition SpringPositionComponent;

    public Transform Pivot;

    public float Distance = 5.43f;


    void Update()
    {
        Vector3 _target = Pivot.transform.position - transform.forward * Distance;

        SpringPositionComponent.target = _target;
        SpringPositionComponent.enabled = true;

        //transform.LookAt(Pivot.transform.position);
    }
}
