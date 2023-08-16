using System.Collections;
using UnityEngine;

public class HealZone : RelativeZone
{
    protected override IEnumerator Act()
    {
        while (_isPlayerCollide)
        {
            playerHP.OnHeal?.Invoke();

            yield return new WaitForSeconds(timeBetweenActes);
        }
    }
}
