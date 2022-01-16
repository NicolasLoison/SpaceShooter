using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider slider;

    void Start()
    {
        slider.value = PlayerPrefs.GetFloat("volume", 1);
    }
    public void SetLevel (float sliderValue)
    {
        audioMixer.SetFloat("Volume", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("volume", sliderValue);
        PlayerPrefs.Save();
    }
}