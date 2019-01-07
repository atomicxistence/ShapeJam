using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private Sound[] sounds;

    private void Awake()
    {
        foreach (var sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();

            sound.source.clip = sound.audioClip;
            sound.source.volume = sound.volume;
            sound.source.loop = sound.loop;
        }
    }

    private void Start()
    {
        Play("Ambient");
    }

    public void Play(string name)
    {
        var soundToPlay = Array.Find(sounds, sound => sound.name == name);
        if (soundToPlay == null)
        {
            Debug.LogError($"Did not find an audio source called {soundToPlay}.");
            return;
        }
        soundToPlay.source.Play();

    }
}
