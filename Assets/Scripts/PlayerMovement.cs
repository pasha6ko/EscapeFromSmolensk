using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public enum MovementStates
    {
        Stay,
        Run,
        WallRun,
        InAir
    }

    [Header("State")]
    public MovementStates movementState;

    [Header("Player Components")]
    [SerializeField] private Rigidbody playerRb;
    [SerializeField] private WallRun wallRun;
    [SerializeField] private Collider playerCollider;

    [Header("Player Movenet Settings")]
    [SerializeField, Range(0f, 10f)] private float speed;
    [SerializeField, Range(0f, 20f)] private float jumpForce;
    [SerializeField] private bool invertedInput;

    private float _dashForce = 1f;
    private Vector2 _inputVector;

    private void FixedUpdate()
    {
        float magnitude = _inputVector.magnitude;
        bool inAir = movementState == MovementStates.InAir;
        if (movementState == MovementStates.WallRun) return;
        if (!inAir)
        {
            Run();
        }
        if ((magnitude == 0) && IsGrounded())
        {
            movementState = MovementStates.Stay;
        }
        else if (magnitude > 0 && IsGrounded())
        {
            movementState = MovementStates.Run;
        }
    }

    public void SetDashForce(float value) => _dashForce = value + 1;
    public void OnMove(InputValue input)
    {
        _inputVector = input.Get<Vector2>() * (invertedInput ? -1 : 1);
    }

    public void OnWallRun()
    {
        movementState = MovementStates.WallRun;
    }

    public void OnJump()
    {
        if (!IsGrounded() && movementState != MovementStates.WallRun) return;
        playerRb.velocity = new Vector3(playerRb.velocity.x, jumpForce, playerRb.velocity.z);
    }

    private void Run()
    {
        Vector3 direction = (_inputVector.x * playerRb.transform.right + _inputVector.y * playerRb.transform.forward) * speed * _dashForce;
        playerRb.velocity = new Vector3(direction.x, playerRb.velocity.y, direction.z);
    }

    public bool IsGrounded()
    {
        float _distanceToTheGround = playerCollider.bounds.extents.y;
        return Physics.Raycast(playerRb.position, Vector3.down, _distanceToTheGround + 0.01f);
    }

    public void SetInveseInput(bool value) => invertedInput = value;
}
