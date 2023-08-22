using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnd : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private PlayerLook playerLook;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        CheckComponents(other);
        End();
    }

    private void End()
    {
        playerMovement.enabled = false;
        playerLook.enabled = false;
        Cursor.lockState = CursorLockMode.None;
    }

    private void CheckComponents(Collider other)
    {
        playerMovement = other.GetComponent<PlayerMovement>();
        playerLook = other.GetComponent<PlayerLook>();
    }
}
