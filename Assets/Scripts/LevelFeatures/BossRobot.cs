using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BossRobot : MonoBehaviour
{
    [Header("Events Components")]
    [SerializeField] private UnityEvent events;
    public void Death()
    {
        events?.Invoke();
        Destroy(gameObject);
    }
}
