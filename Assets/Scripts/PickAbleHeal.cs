using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickAbleHeal : MonoBehaviour
{
    private PlayerHP _playerHP;
    private void OnTriggerEnter(Collider other)
    {
        if (!other.transform.CompareTag("Player")) return;

        _playerHP = other.GetComponent<PlayerHP>();    

        _playerHP.OnHeal?.Invoke(2);
        Destroy(gameObject);
    }
}
