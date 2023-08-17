using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class TimeScaleTestScript : MonoBehaviour
{
    [SerializeField, Range(0, 1f)] private float scale = 1f;
    private float startUpdates;
    private void Start()
    {
        startUpdates = Time.fixedDeltaTime;
    }
    void Update()
    {
        Time.timeScale = scale;
        Time.fixedDeltaTime = startUpdates * scale;
    }
}
