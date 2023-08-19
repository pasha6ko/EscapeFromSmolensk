using UnityEngine;

public class HeadShaking : MonoBehaviour
{
    [Header("Player Components")]
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Transform jointCamera;
    [Header("Shaking Settings")]
    [SerializeField, Range(0, 3)] private float timerForce = 2f;
    [SerializeField, Range(0, 20)] private float shakingSpeed = 15f;
    [SerializeField, Range(0, 20)] private float shakingForce = 0.2f;

    private float _timer = 0;
    private Vector3 _jointOriginalPos;
    private Vector3 _bobAmount = Vector3.up;

    private void Start()
    {
        _jointOriginalPos = new Vector3(jointCamera.localPosition.x, 0.83f, jointCamera.localPosition.z);
        _bobAmount *= shakingForce;
    }

    private void FixedUpdate()
    {
        HeadShake();
    }

    private void HeadShake()
    {
        if (playerMovement.movementState != PlayerMovement.MovementStates.Run)
        {
            jointCamera.localPosition = Vector3.Lerp(jointCamera.localPosition, _jointOriginalPos, Time.deltaTime * shakingSpeed);
            _timer = 0;
            return;
        }
        _timer += Time.deltaTime * (shakingSpeed + timerForce);
        jointCamera.localPosition = Vector3.up * (_jointOriginalPos.y + Mathf.Sin(_timer) * _bobAmount.y);
    }
}
