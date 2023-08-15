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
    [SerializeField] private GameObject deathRawImage;
    [SerializeField] private PlayerInput skipDeath;

    private void Start()
    {
        skipDeath.enabled = false;
    }

    public void Death()
    {
        StartCoroutine(Recovery());
        skipDeath.enabled = true;
    }

    public void OnAnyKeyClick()
    {
        playerHP.OnDeath?.Invoke();
        playerHP.SwitchOn?.Invoke();

        playerInput.enabled = true;
        deathVideo.SetActive(false);
        deathRawImage.SetActive(false);

        StopCoroutine(Recovery());
        skipDeath.enabled = false;
    }

    private IEnumerator Recovery()
    {
        playerInput.enabled = false;
        deathVideo.SetActive(true);
        deathRawImage.SetActive(true);

        yield return new WaitForSeconds(10f);

        if (!skipDeath.enabled) yield break;

        skipDeath.enabled = false;

        playerHP.OnDeath?.Invoke();
        playerHP.SwitchOn?.Invoke();

        playerInput.enabled = true;
        deathVideo.SetActive(false);
        deathRawImage.SetActive(false);
    }
}
