using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Dash : MonoBehaviour
{
    [Header("Player Components")]
    [SerializeField] private Transform cam;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private TimeFreeze timeFreeze;

    [Header("Dash Settings")]
    [SerializeField] private int dashMaxCount;
    [SerializeField, Range(0, 20)] private float dashTime, dashRecoverTime, dashForce;
    [SerializeField] private AnimationCurve dashCurve;

    [SerializeField] private int _dashCount;
    private Coroutine _dashRecoverProcess;
    private Vector2 _inputVector;


    private void Start()
    {
        _dashCount = dashMaxCount;
    }

    private void FixedUpdate()
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
        StartCoroutine(DashProcess());
        if (_inputVector == Vector2.zero) _inputVector = Vector2.up;
        if (playerMovement != null) playerMovement.movementState = PlayerMovement.MovementStates.InAirRun;
        StartCoroutine(DashProcess());
        _dashCount--;
        DeshRecover();
    }

    public void DeshRecover()
    {
        if (_dashRecoverProcess != null) return;
        if (playerMovement == null) return;
        if (playerMovement.movementState != PlayerMovement.MovementStates.Stay &&
            playerMovement.movementState != PlayerMovement.MovementStates.Run &&
            playerMovement.movementState != PlayerMovement.MovementStates.WallRun) return;
        if (_dashCount >= dashMaxCount) return;
        _dashRecoverProcess = StartCoroutine(DeshRecoverAs());
    }

    private IEnumerator DashProcess()
    {
        float timer = 0;
        while (timer <= 1)
        {
            timer += Time.deltaTime / dashTime;
            float dashForceValue = dashCurve.Evaluate(timer) * dashForce;
            playerMovement.SetDashForce(dashForceValue);
            yield return null;
        }
        if (timeFreeze != null)
            timeFreeze.ChangeTimeScale(TimeFreeze.TimeTypes.Normal);
    }

    public void OnTimeFreeze(InputValue value)
    {
        if (timeFreeze == null) return;
        float state = value.Get<float>();
        if (state > 0.6f) timeFreeze.ChangeTimeScale(TimeFreeze.TimeTypes.Slow);
        else timeFreeze.ChangeTimeScale(TimeFreeze.TimeTypes.Normal);
    }

    private IEnumerator DeshRecoverAs()
    {
        while (_dashCount < dashMaxCount)
        {
            yield return new WaitForSeconds(dashRecoverTime);
            _dashCount++;
        }
        _dashRecoverProcess = null;
    }
}
