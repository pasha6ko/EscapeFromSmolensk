using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{
    [Header("Player Components")]
    [SerializeField] private Transform cam;
    [SerializeField] private Transform player;
    [Header("Camera Settings")]
    [SerializeField] private float rotationSensivity;
    private Vector2 _inputVector;

    private void Update()
    {
        float eulerX = (-_inputVector.y * rotationSensivity) % 360;
        float eulerY = (_inputVector.x * rotationSensivity) % 360;

        Vector3 targetYRotation = new Vector3(0, transform.rotation.eulerAngles.y + eulerY, 0);
        Vector3 targetXRotation = new Vector3(cam.rotation.eulerAngles.x + eulerX, 0, 0);
        player.rotation = Quaternion.Euler(targetYRotation);
        cam.localRotation = Quaternion.Euler(targetXRotation);
        _inputVector = Vector2.zero;
    }

    public void OnLook(InputValue input)
    {
        _inputVector = input.Get<Vector2>();
    }
}
