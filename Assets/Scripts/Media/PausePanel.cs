using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PausePanel : MonoBehaviour
{
    public AudioMixerGroup mixerMusic, mixerSound;
    [SerializeField] private PauseController pause;
    public void ChangeMusicVolume(float volume)
    {
        if(pause.IsActive)
        {
            mixerMusic.audioMixer.SetFloat("MusicVolume", Mathf.Lerp(-80, 0, volume));
        }
    } 
    public void ChangeSoundVolume(float volume)
    {
        if (pause.IsActive)
        {
            mixerSound.audioMixer.SetFloat("SoundsVolume", Mathf.Lerp(-80, 0, volume));
        } 
    }
}
