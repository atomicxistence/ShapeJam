using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager SharedInstance { get; private set; }

    [SerializeField]
    private Sound[] sounds;

    private Sound currentPlayingSound;

    private void Awake()
    {
        #region Singleton
        if (SharedInstance == null)
        {
            SharedInstance = this;
        }
        else if (SharedInstance != this)
        {
            Destroy(gameObject);
        }
        #endregion

        DontDestroyOnLoad(gameObject);

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

        if (currentPlayingSound != null && currentPlayingSound.interruptable)
        {
            InterruptCurrentPlayingSoundCheck();
        }

        currentPlayingSound = soundToPlay;
        soundToPlay.source.Play();
    }

    private void InterruptCurrentPlayingSoundCheck()
    {
        if (currentPlayingSound.source.isPlaying)
        {
            currentPlayingSound.source.Stop();
        }
    }
}
