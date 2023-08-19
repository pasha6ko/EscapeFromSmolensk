using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InversZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.transform.CompareTag("Player")) return;
        PlayerMovement player = other.transform.GetComponent<PlayerMovement>();
        player.SetInveseInput(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.transform.CompareTag("Player")) return;
        PlayerMovement player = other.transform.GetComponent<PlayerMovement>();
        player.SetInveseInput(false);
    }
}
