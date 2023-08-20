using System.Collections;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    [Header("Camera Components")]
    [SerializeField] private Camera playerCamera;
    [Header("Circle Settings")]
    [SerializeField] private Vector3 circleCentre;
    [SerializeField] private float circleRadius;

    private enum Rotating
    {
        Left,
        Right
    }

    private bool _isRotating = false;
    private Rotating direct;
    private float angle = -90f;
    private float lookAt = 0f;
    private Quaternion _startRotation;
    private Quaternion _targetRotation;
    private float _rotationDuration = 0.8f;
    private float _rotationTimer = 0f;

    private Vector3 _startPosition;
    private Vector3 _targetPosition;
    private float _movementDuration = 0.8f;
    private float _movementTimer = 0f;

    private void Start()
    {
        _startPosition = playerCamera.transform.position;
        SetAngleAndPosition();
    }

    public void Left()
    {
        direct = Rotating.Left;
        Rotate();
    }

    public void Right()
    {
        direct = Rotating.Right;
        Rotate();
    }

    public void Rotate()
    {
        if (!_isRotating)
        {
            StartCoroutine(RotateCameraSmoothly());
            StartCoroutine(MoveCameraSmoothly());
        }
    }

    private void SetAngleAndPosition()
    {
        _startRotation = playerCamera.transform.rotation;

        lookAt = direct == Rotating.Right ? -90f : 90f;
        angle += direct == Rotating.Right ? -90f : 90f;
        _targetRotation = _startRotation * Quaternion.Euler(Vector3.up * lookAt);
    }

    private IEnumerator RotateCameraSmoothly()
    {
        _isRotating = true;

        SetAngleAndPosition();
        _rotationTimer = 0f;

        while (_rotationTimer < _rotationDuration)
        {
            float t = _rotationTimer / _rotationDuration;
            playerCamera.transform.rotation = Quaternion.Slerp(_startRotation, _targetRotation, t);

            _rotationTimer += Time.deltaTime;
            yield return null;
        }

        playerCamera.transform.rotation = _targetRotation;
    }

    private IEnumerator MoveCameraSmoothly()
    {
        _startPosition = playerCamera.transform.position;
        float angleRad = Mathf.Deg2Rad * angle;

        float sin = Mathf.Round(Mathf.Sin(angleRad));
        float cos = Mathf.Round(Mathf.Cos(angleRad));

        _targetPosition = new Vector3(
            sin * circleRadius + circleCentre.x,
            playerCamera.transform.position.y,
            cos * circleRadius + circleCentre.z
        );

        _movementTimer = 0f;

        while (_movementTimer < _movementDuration)
        {
            float t = _movementTimer / _movementDuration;
            playerCamera.transform.position = Vector3.Lerp(_startPosition, _targetPosition, t);

            _movementTimer += Time.deltaTime;
            yield return null;
        }

        playerCamera.transform.position = _targetPosition;
        _isRotating = false;
    }
}
