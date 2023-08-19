using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift : MonoBehaviour
{
    [HideInInspector] public Transform mainHook;
    [HideInInspector] public List<Transform> hooks = new List<Transform>();

    [Header("Player Components")]
    [SerializeField] private Transform cam;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Transform marker;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private Transform ropeStartPoint;
    [SerializeField] private float FOV;

    [Header("Lift Settings")]
    [SerializeField, Range(0f, 10f)] private float liftForce;
    [SerializeField, Range(0f, 10f)] private float recoverTime;
    [SerializeField] private float inAirLockTime;
    private bool _canLift = true;

    private void Update()
    {
        if (marker == null) return;
        if (mainHook == null || !InFieldOfView(mainHook))
        {
            marker.gameObject.SetActive(false);
            return;
        }
        if (!InFieldOfView(mainHook)) return;
        marker.gameObject.SetActive(true);
        marker.position = Camera.main.WorldToScreenPoint(mainHook.position);

    }

    private void FixedUpdate()
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
        mainHook = closestHook;
    }

    public void OnLift()
    {
        if (!_canLift) return;
        if (mainHook == null) return;
        if (!InFieldOfView(mainHook)) return;
        Vector3 diraction = mainHook.position - transform.position;
        rb.useGravity = true;
        rb.velocity = Vector3.zero;
        playerMovement.movementState = PlayerMovement.MovementStates.InAir;
        rb.AddForce(diraction * liftForce, ForceMode.VelocityChange);
        _canLift = false;
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, ropeStartPoint.position);
        lineRenderer.SetPosition(1, mainHook.position);
        StartCoroutine(LiftRecovery());

    }

    private IEnumerator LiftRecovery()
    {
        float time = 0;
        while (time < recoverTime)
        {
            time += Time.deltaTime;
            if (time < inAirLockTime) playerMovement.movementState = PlayerMovement.MovementStates.InAir;
            lineRenderer.SetPosition(0, ropeStartPoint.position);
            yield return null;
        }
        _canLift = true;
        lineRenderer.enabled = false;
    }

    private bool InFieldOfView(Transform point)
    {
        Vector3 direction = point.transform.position - cam.transform.position;

        Vector3 angle = Quaternion.FromToRotation(cam.forward, direction).eulerAngles;
        if ((angle.x > 360 - 20 || angle.x < 0 + 20) && (angle.y > 360 - FOV / 2 || angle.y < FOV / 2))
        {
            Debug.DrawRay(transform.position + Vector3.up, point.transform.position - transform.position, Color.green);
            return true;
        }
        Debug.DrawRay(transform.position + Vector3.up, point.transform.position - transform.position, Color.red);
        return false;
    }
}
