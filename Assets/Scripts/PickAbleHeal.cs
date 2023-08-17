using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickAbleHeal : MonoBehaviour
{
    [Header("Player Components")]
    [SerializeField] private PlayerHP playerHP;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.transform.CompareTag("Player")) return;

        playerHP.OnHeal?.Invoke();
        Destroy(gameObject);
    }
}
