using System.Collections;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    [Header("Camera Components")]
    [SerializeField] private Camera playerCamera;
    [Header("Arrows Settongs")]
    [SerializeField] private Rotating direct;

    private enum Rotating
    {
        Left,
        Right
    }

    private bool _isRotating = false;
    private Quaternion _startRotation;
    private Quaternion _targetRotation;
    private float _rotationDuration = 0.8f;
    private float _rotationTimer = 0f;

    private void Start()
    {
        SetAngle();
    }

    public void Rotate()
    {
        if (!_isRotating)
        {
            StartCoroutine(RotateCameraSmoothly());
        }
    }

    private void SetAngle()
    {
        _startRotation = playerCamera.transform.rotation;

        if (direct == Rotating.Right)
        {
            _targetRotation = _startRotation * Quaternion.Euler(Vector3.up * -90f);
        }
        else
        {
            _targetRotation = _startRotation * Quaternion.Euler(Vector3.up * 90f);
        }
    }

    private IEnumerator RotateCameraSmoothly()
    {
        _isRotating = true;
        SetAngle();
        _rotationTimer = 0f;

        while (_rotationTimer < _rotationDuration)
        {
            playerCamera.transform.rotation = Quaternion.Slerp(_startRotation, _targetRotation, _rotationTimer / _rotationDuration);
            _rotationTimer += Time.deltaTime;
            yield return null;
        }

        playerCamera.transform.rotation = _targetRotation;
        _isRotating = false;
    }
}
