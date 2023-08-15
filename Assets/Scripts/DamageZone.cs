using System.Collections;
using UnityEngine;

public class DamageZone : RelativeZone
{
    [SerializeField] private float attackTime;

    protected override IEnumerator Act()
    {
        while (_isPlayerCollide)
        {
            playerHP.OnSmallDamage?.Invoke();

            yield return new WaitForSeconds(attackTime);
        }
    }
}
