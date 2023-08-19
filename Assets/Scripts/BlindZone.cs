using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class BlindZone : MonoBehaviour
{
    [Header("Blind Settings")]
    [SerializeField, Range(10f, 150f)] private float targetFov;
    [Header("Blind Components")]
    [SerializeField] private Volume postProcessing;

    private Vignette _vignette;

    private void Start()
    {
        postProcessing.profile.TryGet(out _vignette);
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
