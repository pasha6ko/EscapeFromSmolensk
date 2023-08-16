using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRaycast : MonoBehaviour
{
    [SerializeField] GameObject _target;
    void Update()
    {
        Ray ray = new Ray(transform.position,_target.transform.position-transform.position);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 200f))
        {
            Debug.DrawLine(transform.position, _target.transform.position);
            Debug.DrawRay(hit.point, hit.normal);
        }
    }
}
