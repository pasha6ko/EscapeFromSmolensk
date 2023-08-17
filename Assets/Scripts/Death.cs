using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Death : MonoBehaviour
{
    public Action OnDeath;
    public Action SwitchOn;

    [Header("Player Components")]
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private CheckPoint playerCheckPoint;
    [Header("Death Components")]
    [SerializeField] private GameObject deathVideo;
    [SerializeField] private GameObject deathUI;

    public void StartDeath()
    {
        StartCoroutine(Recovery());
        playerMovement.enabled = false;
    }

    public void OnAnyKeyClick()
    {
        if (playerMovement.enabled) return;
        OnDeath?.Invoke();
        SwitchOn?.Invoke();

        deathVideo.SetActive(false);
        deathUI.SetActive(false);

        StopCoroutine(Recovery());
        playerMovement.enabled = true;
    }

    private IEnumerator Recovery()
    {
        deathVideo.SetActive(true);
        deathUI.SetActive(true);

        yield return new WaitForSeconds(10f);

        if (playerMovement.enabled) yield break;
        playerMovement.enabled = true;

        OnDeath?.Invoke();
        SwitchOn?.Invoke();

        deathVideo.SetActive(false);
        deathUI.SetActive(false);
    }
}
