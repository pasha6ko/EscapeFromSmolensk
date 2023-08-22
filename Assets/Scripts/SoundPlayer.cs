using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    [SerializeField] private List<AudioClip> audioClipList;
    [SerializeField] private AudioSource audioSource;

    private Dictionary<string, AudioClip> audioClipDictionary = new Dictionary<string, AudioClip>();

    private void Start()
    {
        foreach (AudioClip clip in audioClipList)
        {
            audioClipDictionary.Add(clip.name, clip);
        }
    }

    public void PlayAudioClip(string clipName)
    {
        if (audioClipList.Count == 0)
        {
            Debug.LogWarning("No audio clips in the list.");
            return;
        }

        if (!audioClipDictionary.ContainsKey(clipName)) return;

        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }

        
        AudioClip clipToPlay = audioClipDictionary[clipName];
        audioSource.clip = clipToPlay;
        audioSource.Play();
    }

    public void PlayRandomClip()
    {
        int length = audioClipList.Count;
        audioSource.clip = audioClipList[Random.RandomRange(0, length)];
        audioSource.Play();
    }

    public void PlayAndDestory(string clipName)
    {
        PlayAudioClip(clipName);
        if (!audioClipDictionary.ContainsKey(clipName));
        audioClipDictionary.Remove(clipName);
    }
}
