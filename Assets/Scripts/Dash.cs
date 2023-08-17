using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Dash : MonoBehaviour
{
    [Header("Player Components")]
    [SerializeField] private Transform cam;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Rigidbody rb;

    [Header("Dash Settings")]
    [SerializeField] private int dashMaxCount;
    [SerializeField, Range(0, 20)] private float dashRecoverTime, dashForce;

    [SerializeField] private int _dashCount;
    private Coroutine _dashRecoverProcess;
    private Vector2 _inputVector;


    private void Start()
    {
        _dashCount = dashMaxCount;
    }
    public void FixedUpdate()
    {
        DeshRecover();
    }
    public void OnMove(InputValue input)
    {
        _inputVector = input.Get<Vector2>();
    }
    public void OnDash()
    {
        if (_dashCount <= 0) return;
        if (_inputVector == Vector2.zero) _inputVector = Vector2.up; 
        Vector3 forceDirection = transform.forward * _inputVector.y + transform.right * _inputVector.x;
        rb.AddForce(forceDirection * dashForce, ForceMode.VelocityChange);
        _dashCount--;
        DeshRecover();

    }
    public void DeshRecover()
    {
        if (_dashRecoverProcess != null) return;
        if (!playerMovement.IsGrounded()) return;
        if (_dashCount >= dashMaxCount) return;
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
