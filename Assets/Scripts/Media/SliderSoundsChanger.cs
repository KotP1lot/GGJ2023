using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SliderSoundsChanger : MonoBehaviour
{
    public Slider sliderSounds, sliderMusic;
    public AudioMixerGroup mixerSounds, mixerMusic;
    public float GetMusicLevel()
    {
        float value;
        bool result = mixerMusic.audioMixer.GetFloat("MusicVolume",out value); // -80, 0 (0,1)
        if(result)
        {
            return (value + 80) / 80;
        } else
        {
            return 0f;
        }
    }
    public float GetSoundsLevel()
    {
        float value;
        bool result = mixerSounds.audioMixer.GetFloat("SoundsVolume", out value);
        if (result)
        {
            return (value + 80) / 80;
        }
        else
        {
            return 0f;
        }
    }
    private void Start()
    {
        sliderSounds.value = GetSoundsLevel();
        sliderMusic.value = GetMusicLevel();
    }

}
