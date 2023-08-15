using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRaycast : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject _target;
    [SerializeField] LayerMask _ignoreLayer;
    GameObject parent;
    [SerializeField] GameObject SpawnObj;

    GameObject clone;
    void Start()
    {
        parent = GameObject.Find("Sum");
        clone =  Instantiate(SpawnObj);
    }

    // Update is called once per frame
    void Update()
    {
        
        Ray ray = new Ray(transform.position,_target.transform.position-transform.position);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit ,200f, _ignoreLayer))
        {
            clone.active = true;
            clone.transform.position = hit.point;
            Debug.DrawRay(transform.position, _target.transform.position - transform.position, Color.green);
        }
        else
        {
            clone.active =false;
            Debug.DrawRay(transform.position, _target.transform.position - transform.position, Color.red);
        }
    }
}
