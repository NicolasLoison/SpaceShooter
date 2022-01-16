using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MuteAudio : MonoBehaviour
{
    [FormerlySerializedAs("AudioMixer")] public AudioMixer audioMixer;

    public Toggle muteToggle;
    // Start is called before the first frame update
    void Start()
    {
        // float volume = PlayerPrefs.GetFloat("lastVolume", 0);
        // if (volume.Equals(-80f))
        // {
        //     muteToggle.isOn = false;
        // }
        // else
        // {
        //     muteToggle.isOn = true;
        // }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MuteToggle(bool isMute)
    {
        if (isMute)
        {
            PlayerPrefs.SetFloat("lastVolume", PlayerPrefs.GetFloat("volume", 1));
            audioMixer.SetFloat("Volume", -80);
            PlayerPrefs.SetFloat("volume", 0.0001f);
        }
        else
        {
            var volume = PlayerPrefs.GetFloat("lastVolume", 0);
            audioMixer.SetFloat("Volume", Mathf.Log10(volume) * 20);
            PlayerPrefs.SetFloat("volume", volume);
        }
        PlayerPrefs.Save();
    }
}
