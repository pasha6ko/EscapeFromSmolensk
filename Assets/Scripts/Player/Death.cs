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
        yield return new WaitForSeconds(0.5f);
        isDead = true;
        yield return new WaitForSeconds(9.5f);
        OnEKeyClick();

    }

    public void StartDeath()
    {
        if (isDead) return;
        StartCoroutine(Recovery());
        Destroy(playerMovement);
    }

    public void OnEKeyClick()
    {
        if (!isDead) return;
        deathVideo.SetActive(false);
        deathUI.SetActive(false);

        isDead = false;
        OnDeath?.Invoke();
        SwitchOn?.Invoke();

        StopCoroutine(Recovery());
        Destroy(gameObject);
    }
}
