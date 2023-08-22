using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEvents : MonoBehaviour
{
    [Header("Events Components")]
    [SerializeField] private UnityEvent events;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        events?.Invoke();
        Destroy(gameObject);
    }
}
