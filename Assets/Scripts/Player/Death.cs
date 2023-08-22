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
    [SerializeField] private GameObject deathUI;
    [HideInInspector] public bool isDead = false;

    private IEnumerator Recovery()
    {
        ScoreCounter.Instance.Damage(2);
        deathUI.SetActive(true);
        isDead = true;
        yield return new WaitForSeconds(1f);
        OnDeath?.Invoke();
        SwitchOn?.Invoke();
        deathUI.SetActive(false);
        Destroy(gameObject);
    }

    public void StartDeath()
    {
        if (isDead) return;
        StartCoroutine(Recovery());
        Destroy(playerMovement);
    }
}
