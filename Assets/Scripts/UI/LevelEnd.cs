using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnd : MonoBehaviour
{
    private PlayerMovement _playerMovement;
    private PlayerLook _playerLook;
    private HeadShaking _headShaking;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        CheckComponents(other);
        End();
    }

    private void End()
    {
        _playerMovement.enabled = false;
        _playerLook.enabled = false;
        _headShaking.enabled = false;
        Cursor.lockState = CursorLockMode.None;
    }

    private void CheckComponents(Collider other)
    {
        _playerMovement = other.GetComponent<PlayerMovement>();
        _playerLook = other.GetComponent<PlayerLook>();
        _headShaking = other.GetComponent<HeadShaking>();
    }
}
