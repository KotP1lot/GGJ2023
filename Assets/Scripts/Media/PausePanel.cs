using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class PausePanel : MonoBehaviour
{
    public AudioMixerGroup mixerMusic, mixerSound;
    [SerializeField] private PauseController pause;
    private bool isMainMenu = false;
    private void Start()
    {
        isMainMenu = !GameObject.Find("SettingsWindow").IsUnityNull();
    }
    public void ChangeMusicVolume(float volume)
    {
        if((pause != null && pause.IsActive) || isMainMenu)
        {
            mixerMusic.audioMixer.SetFloat("MusicVolume", Mathf.Lerp(-80, 0, volume));
        }
    } 
    public void ChangeSoundVolume(float volume)
    {
        if ((pause != null && pause.IsActive) || isMainMenu)
        {
            mixerSound.audioMixer.SetFloat("SoundsVolume", Mathf.Lerp(-80, 0, volume));
        } 
    }
}
