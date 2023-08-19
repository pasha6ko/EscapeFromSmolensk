using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class BlindZone : MonoBehaviour
{
    [SerializeField, Range(10f, 150f)] private float targetFov;
    [SerializeField] private Volume post;
    private Vignette _vignette;

    private void Start()
    {
        post.profile.TryGet(out _vignette);
        _vignette.active = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.transform.CompareTag("Player")) return;
        _vignette.active = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.transform.CompareTag("Player")) return;
        _vignette.active = false;
    }
}
