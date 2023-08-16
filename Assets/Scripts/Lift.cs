using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift : MonoBehaviour
{
    [HideInInspector] public Transform _mainHook;
    [HideInInspector] public List<Transform> hooks = new List<Transform>();

    [Header("Player Components")]
    [SerializeField] private Transform cam;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Transform marker;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private Transform ropeStartPoint;

    [Header("Lift Settings")]
    [SerializeField, Range(0f, 10f)] private float liftForce;
    [SerializeField, Range(0f, 10f)] private float recoverTime;
    private bool canLift = true;

    public void Update()
    {
        if (marker == null) return;
        if (_mainHook == null) marker.position = new Vector2(-10000, -10000);
        else if(marker != null)marker.position = Camera.main.WorldToScreenPoint(_mainHook.position);
    }
    public void FixedUpdate()
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
    }

    public void OnLift()
    {
        if (!canLift) return;
        if (_mainHook == null) return;
        playerMovement.OnJump();
        Vector3 diraction =  _mainHook.position- transform.position;
        rb.velocity = Vector3.zero;
        rb.AddForce(diraction * liftForce, ForceMode.VelocityChange);
        canLift = false;
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, ropeStartPoint.position);
        lineRenderer.SetPosition(1, _mainHook.position);
        StartCoroutine(LiftRecovery());
    
    }
    IEnumerator LiftRecovery()
    {
        float time = 0;
        while (time < recoverTime)
        {
            time += Time.deltaTime;
            lineRenderer.SetPosition(0, ropeStartPoint.position);
            yield return null;  
        }
        canLift = true;
        lineRenderer.enabled = false;
    }
}
