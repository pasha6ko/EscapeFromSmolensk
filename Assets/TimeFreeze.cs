using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeFreeze : MonoBehaviour
{
    public enum TimeTypes
    {
        Slow,
        Normal,
        Fast
    }
    [SerializeField] private float smoothChangeTime;
    [SerializeField, Range(0,1f)] private float minScale,maxScale;
    private float startUpdates;
    private Coroutine timeChangeProcess;
    private void Start()
    {
        startUpdates = Time.fixedDeltaTime;
    }

    public void SetTimeScale(float scale)
    {
        Time.timeScale = scale;
        Time.fixedDeltaTime = startUpdates * scale;
    }

    public void ChangeTimeScale(TimeTypes type)
    {
        if (timeChangeProcess != null)
        {
            StopCoroutine(timeChangeProcess);
            timeChangeProcess = null;
        }
        float targetTimeScale = 1f;
        if(type == TimeTypes.Slow) targetTimeScale = minScale;
        else if(type == TimeTypes.Normal) targetTimeScale = maxScale;
        timeChangeProcess = StartCoroutine(ChangeTimeToSmooth(targetTimeScale));
    }

    private IEnumerator ChangeTimeToSmooth(float to)
    {
        float time = 0f;
        float from = Time.timeScale;
        while (time <= 1f)
        {
            time += Time.deltaTime/smoothChangeTime;
            SetTimeScale(Mathf.Lerp(from,to,time));
            yield return null;
        }
    }
}
