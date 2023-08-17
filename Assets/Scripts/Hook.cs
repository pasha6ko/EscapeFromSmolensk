using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Hook : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        Lift lift = other.transform.GetComponent<Lift>();
        if (lift == null) return;
        lift.hooks.Add(transform);
    }
    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        Lift lift = other.transform.GetComponent<Lift>();
        if (lift == null) return;
        lift.hooks.Remove(transform);
        if(lift.mainHook == transform) lift.mainHook = null;
    }
}
