using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BossRobot : MonoBehaviour
{
    [SerializeField] private UnityEvent events;
    public void Death()
    {
        events?.Invoke();
        Destroy(gameObject);
    }
}
