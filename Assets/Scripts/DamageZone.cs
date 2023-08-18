using JetBrains.Annotations;
using System;
using System.Collections;
using UnityEngine;

public class DamageZone : RelativeZone
{
    [Header("Damage Settings")]
    [SerializeField] public DamagesTypes damageType;

    private OneValueOperations _action;

    public enum DamagesTypes
    {
        NoDamage,
        Small,
        Medium,
        Large
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
                value = 50;

                break;

            case DamagesTypes.Large:
                value = 100;

                break;
        }

        while (_isPlayerCollide && playerHP != null && _action != null)
        {
            _action?.Invoke(value);

            yield return new WaitForSeconds(timeBetweenActes);
        }
    }
}
