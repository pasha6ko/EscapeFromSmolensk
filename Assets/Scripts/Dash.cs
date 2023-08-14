using System.Collections;
using UnityEngine;

public class Dash : MonoBehaviour
{
    [Header("Player Components")]
    [SerializeField] private Transform cam;
    [SerializeField] private Rigidbody rb;

    [Header("Dash Settings")]
    [SerializeField] private int dashMaxCount;
    [SerializeField, Range(0, 20)] private float dashRecoverTime, dashForce;

    private int _dashCount;
    private Coroutine _dashRecoverProcess;

    private void Start()
    {
        _dashCount = dashMaxCount;
    }
    public void OnDash()
    {
        if (_dashCount <= 0) return;
        Vector3 planeCameraDiraction = new Vector3(cam.forward.x, 0, cam.forward.z).normalized;
        rb.AddForce(planeCameraDiraction * dashForce, ForceMode.VelocityChange);
        _dashCount--;

    }
    public void DeshRecover()
    {
        if (_dashRecoverProcess == null)
            _dashRecoverProcess = StartCoroutine(DeshRecoverAs());
    }
    IEnumerator DeshRecoverAs()
    {
        while (_dashCount < dashMaxCount)
        {
            yield return new WaitForSeconds(dashRecoverTime);
            _dashCount++;
        }
        _dashRecoverProcess = null;
    }



}
