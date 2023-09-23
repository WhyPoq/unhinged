using UnityEngine.Audio;
using UnityEngine;
using System;
using UnityEngine.Rendering;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance = null;

    [SerializeField]
    private AudioMixerGroup musicGroup;
    [SerializeField]
    private AudioMixerGroup soundEffectGroup;

    [SerializeField]
    private Sound[] musicPieces;

    [SerializeField]
    private Sound[] soundEffects;

    Sound curPlayingMusic;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        //DontDestroyOnLoad(gameObject);

        foreach (Sound s in musicPieces)
        {
            s.source = gameObject.AddComponent<AudioSource>();

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.clip = s.clips[0];

            s.source.outputAudioMixerGroup = musicGroup;
        }

        foreach (Sound s in soundEffects)
        {
            s.source = gameObject.AddComponent<AudioSource>();

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.clip = s.clips[0];

            s.source.outputAudioMixerGroup = soundEffectGroup;
        }
    }

    public void SetSettings(string name, float volume, float pitch)
    {
        Sound s = Array.Find(soundEffects, sound => sound.name == name);
        s.source.volume = volume;
        s.source.pitch = pitch;
    }

    public void Play(string name)
    {
        Sound s = Array.Find(soundEffects, sound => sound.name == name);

        if (s != null)
        {
            s.source.clip = s.clips[UnityEngine.Random.Range(0, s.clips.Length)];
            s.source.Play();
        }
        else
        {
            Debug.LogWarning("");
        }
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(soundEffects, sound => sound.name == name);

        if (s != null)
        {
            s.source.Stop();
        }
        else
        {
            Debug.LogWarning("");
        }
    }

    public void TurnOffMusic()
    {
        if (curPlayingMusic != null)
        {
            curPlayingMusic.source.Stop();
        }
    }

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicPieces, sound => sound.name == name);

        if (s != null)
        {
            if (s != curPlayingMusic)
            {
                TurnOffMusic();
                s.source.Play();
                curPlayingMusic = s;
            }
        }
        else
        {
            Debug.LogWarning("");
        }
    }
}
