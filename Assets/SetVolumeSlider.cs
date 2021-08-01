using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SetVolumeSlider : MonoBehaviour
{
    [SerializeField]
    private AudioMixer audioMixer;
    
    public void SetExAudio(float volume) {
        audioMixer.SetFloat("MyExposedParam", volume);
    }
}
