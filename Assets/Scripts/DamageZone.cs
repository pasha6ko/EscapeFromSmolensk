using JetBrains.Annotations;
using System;
using System.Collections;
using UnityEngine;

public class DamageZone : RelativeZone
{
    [Header("Damage Settings")]
    [SerializeField] public DamagesTypes damageType;

    public enum DamagesTypes
    {
        Small,
        Medium,
        Large,
        Critical
    }

    protected override IEnumerator Act()
    {
        int value = 0;
        switch (damageType)
        {
            case DamagesTypes.Small:
                value = 10;
                break;

            case DamagesTypes.Medium:
                value = 4;
                break;

            case DamagesTypes.Large:
                value = 2;
                break;
            case DamagesTypes.Critical:
                ScoreCounter.Instance.RestartLevel();
                value = 1;
                break;
        }

        while (_isPlayerCollide && playerHP != null)
        {
            playerHP.OnDamage?.Invoke(value);

            yield return new WaitForSeconds(timeBetweenActes);
        }
    }
}
