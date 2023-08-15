using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelativeZone : MonoBehaviour
{
    [SerializeField] protected PlayerHP playerHP;

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
