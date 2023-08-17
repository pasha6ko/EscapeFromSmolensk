using System;
using System.Collections;
using UnityEngine;

public class DamageZone : RelativeZone
{
    [Header("Damage Settings")]
    [SerializeField] private DamagesTypes damageType;

    private Action _action;

    private enum DamagesTypes
    {
        Small,
        Medium,
        Large
    }

    protected override IEnumerator Act()
    {
        switch (damageType)
        {
            case DamagesTypes.Small:
                _action = playerHP.OnSmallDamage;
                break;

            case DamagesTypes.Medium:
                _action = playerHP.OnDamage;
                break;

            case DamagesTypes.Large:
                _action = playerHP.OnCriticalDamage;
                break;
        }

        while (_isPlayerCollide && playerHP != null)
        {
            _action?.Invoke();

            yield return new WaitForSeconds(timeBetweenActes);
        }
    }
}
