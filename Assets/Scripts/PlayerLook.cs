using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] Transform camera;
    [SerializeField] private float rotationSensivity;
    private Vector2 _inputVector;

    private void Update()
    {
        float eulerX = ( -_inputVector.y * rotationSensivity) % 360;
        print(eulerX);
        float eulerY = (_inputVector.x * rotationSensivity) % 360;
        camera.transform.rotation = Quaternion.Euler(Mathf.Clamp(camera.transform.rotation.eulerAngles.x + eulerX, -70, 75), 0, 0);
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y + eulerY, 0);
        _inputVector = Vector2.zero;
    }
    public void OnLook(InputValue input)
    {
        //print(input.Get<Vector2>());
        _inputVector = input.Get<Vector2>();
    }

}
