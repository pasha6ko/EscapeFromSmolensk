using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    [SerializeField] private List<AudioClip> audioClipList;
    private AudioSource audioSource;

    private Dictionary<string, AudioClip> audioClipDictionary = new Dictionary<string, AudioClip>();

    private void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();

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

        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }

        AudioClip clipToPlay = audioClipDictionary[clipName];
        audioSource.clip = clipToPlay;
        audioSource.Play();
    }
}
