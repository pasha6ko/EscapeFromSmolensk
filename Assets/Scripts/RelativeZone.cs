using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RelativeZone : MonoBehaviour
{
    [Header("Player Components")]
    protected PlayerHP playerHP;
    [Header("Act Settings")]
    [SerializeField, Range(0, 1)] protected float timeBetweenActes;

    protected bool _isPlayerCollide = false;


    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.transform.CompareTag("Player")) return;
        playerHP = collision.transform.GetComponent<PlayerHP>();
        _isPlayerCollide = true;
        StartCoroutine(Act());
    }

    private void OnCollisionExit(Collision collision)
    {
        playerHP = null;
        StopCoroutine(Act());
        _isPlayerCollide = false;
    }

    protected virtual IEnumerator Act()
    {
        yield return null;
    }
}
