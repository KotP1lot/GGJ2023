using UnityEngine.Audio;
using System;
using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public Sound[] sfx;

    public static AudioManager instance;
    public AudioMixer mixer;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            return;
        }

        foreach (Sound sound in sfx)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;

            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
            sound.source.outputAudioMixerGroup = sound.mixerGroup;
        }
        
    }
    public void Start()
    {
        Play("Theme");
    }
    public void Play(string name)
    {
        Sound s = Array.Find(sfx, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("Sound" + name + "not found");
            return;
        }
        s.source.Play();
    }
}