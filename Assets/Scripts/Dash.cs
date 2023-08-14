using System.Collections;
using UnityEngine;

public class Dash : MonoBehaviour
{
    [SerializeField] private Transform cam;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private int dashMaxCount;
    [SerializeField] private float dashRecoverTime, dashForce;
    private int _dashCount;
    private Coroutine _dashRecoverProcess;

    private void Start()
    {
        _dashCount = dashMaxCount;
    }
    public void OnDash()
    {
        print("Dash");
        if (_dashCount <= 0) return;
        Vector3 planeCameraDiraction = new Vector3(cam.forward.x, 0, cam.forward.z).normalized;
        print(planeCameraDiraction);
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
