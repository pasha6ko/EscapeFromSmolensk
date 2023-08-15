using System.Collections;
using UnityEngine;

public class HealZone : RelativeZone
{
    [Header("Heal Settings")]
    [SerializeField, Range(0, 1)] private float healTime;

    protected override IEnumerator Act()
    {
        while (_isPlayerCollide)
        {
            playerHP.OnHeal?.Invoke();

            yield return new WaitForSeconds(healTime);
        }
    }
}
