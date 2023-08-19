using System;
using System.Collections;
using UnityEngine;

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
    [HideInInspector] public bool isDead = false;

    private IEnumerator Recovery()
    {
        ScoreCounter.Instance.Damage(2);
        deathVideo.SetActive(true);
        deathUI.SetActive(true);
        isDead = true;
        yield return new WaitForSeconds(10f);
        OnAnyKeyClick();

    }

    public void StartDeath()
    {
        StartCoroutine(Recovery());
        Destroy(playerMovement);
    }

    public void OnAnyKeyClick()
    {
        if (!isDead) return;
        OnDeath?.Invoke();
        SwitchOn?.Invoke();

        deathVideo.SetActive(false);
        deathUI.SetActive(false);

        StopCoroutine(Recovery());
        Destroy(gameObject);
    }
}
