using UnityEngine;
using UnityEngine.Audio;

public class PausePanel : MonoBehaviour
{
    public AudioMixerGroup mixerMusic, mixerSound;
    private bool isMainMenu = false;
    public void ChangeMusicVolume(float volume)
    {
            mixerMusic.audioMixer.SetFloat("MusicVolume", Mathf.Lerp(-80, 0, volume));
    } 
    public void ChangeSoundVolume(float volume)
    {
            mixerSound.audioMixer.SetFloat("SoundsVolume", Mathf.Lerp(-80, 0, volume));
    }
}
