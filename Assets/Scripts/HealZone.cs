using System.Collections;
using UnityEngine;

public class HealZone : RelativeZone
{
    [SerializeField] private float healTime;

    protected override IEnumerator Act()
    {
        while (_isPlayerCollide)
        {
            playerHP.OnHeal?.Invoke();

            yield return new WaitForSeconds(healTime);
        }
    }
}
