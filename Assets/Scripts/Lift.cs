using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift : MonoBehaviour
{
    public Transform _mainHook;
    public List<Transform> hooks = new List<Transform>();
    [SerializeField] private Transform cam;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform testSphere;
    [SerializeField] private float liftForce;
    public void OnLook()
    {
        if (hooks.Count <= 0) return;
        float max = float.MinValue;
        Transform closestHook = null;
        foreach (Transform hook in hooks)
        {
            Vector3 diraction = transform.position - hook.position;
            diraction = diraction.normalized - cam.forward;
            if (max > diraction.magnitude) continue;
            max = diraction.magnitude;
            closestHook = hook;
        }
        _mainHook = closestHook;
        testSphere.position = _mainHook.position;
    }
    public void OnLift()
    {
        Vector3 diraction =  _mainHook.position- transform.position;
        rb.velocity = Vector3.zero;
        rb.AddForce(diraction * liftForce, ForceMode.VelocityChange);
    }
}
