using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RelativeZone : MonoBehaviour
{
    [Header("Player Components")]
    [SerializeField] protected PlayerHP playerHP;
    [Header("Act Settings")]
    [SerializeField, Range(0, 1)] protected float timeBetweenActes;

    protected bool _isPlayerCollide = false;


    private void OnCollisionEnter(Collision collision)
    {
        _isPlayerCollide = true;
        StartCoroutine(Act());
    }

    private void OnCollisionExit(Collision collision)
    {
        _isPlayerCollide = false;
    }

    protected virtual IEnumerator Act()
    {
        yield return null;
    }
}
