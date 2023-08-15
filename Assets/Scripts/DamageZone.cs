using System.Collections;
using UnityEngine;

public class DamageZone : RelativeZone
{
    [Header("Damage Settings")]
    [SerializeField, Range(0, 1)] private float attackTime;

    protected override IEnumerator Act()
    {
        while (_isPlayerCollide)
        {
            playerHP.OnSmallDamage?.Invoke();

            yield return new WaitForSeconds(attackTime);
        }
    }
}
