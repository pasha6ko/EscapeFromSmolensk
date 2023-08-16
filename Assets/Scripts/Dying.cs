using System;
using System.Collections;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.Video;

public class Dying : MonoBehaviour
{
    [Header("Player Components")]
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private CheckPoint playerCheckPoint;
    [SerializeField] private PlayerHP playerHP;
    [Header("Death Components")]
    [SerializeField] private GameObject deathVideo;
    [SerializeField] private GameObject deathUI;
    [SerializeField] private PlayerInput playerAnyKeyInput;

    private void Start()
    {
        playerAnyKeyInput.enabled = false;
    }

    public void Death()
    {
        StartCoroutine(Recovery());
        playerAnyKeyInput.enabled = true;
    }

    public void OnAnyKeyClick()
    {
        playerHP.OnDeath?.Invoke();
        playerHP.SwitchOn?.Invoke();

        playerInput.enabled = true;
        deathVideo.SetActive(false);
        deathUI.SetActive(false);

        StopCoroutine(Recovery());
        playerAnyKeyInput.enabled = false;
    }

    private IEnumerator Recovery()
    {
        playerInput.enabled = false;
        deathVideo.SetActive(true);
        deathUI.SetActive(true);

        yield return new WaitForSeconds(10f);

        if (!playerAnyKeyInput.enabled) yield break;

        playerAnyKeyInput.enabled = false;

        playerHP.OnDeath?.Invoke();
        playerHP.SwitchOn?.Invoke();

        playerInput.enabled = true;
        deathVideo.SetActive(false);
        deathUI.SetActive(false);
    }
}
