using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    private Resolution[] _resolutions;
    public Dropdown resolutionDropdown;
    public Slider slider;
    private void Start()
    {
        _resolutions = Screen.resolutions.Select(resolution => new Resolution {
            width = resolution.width, height = resolution.height
        }).Distinct().ToArray();
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        int currentResIndex = 0;
        for (var i = 0; i < _resolutions.Length; i++)
        {
            var res = _resolutions[i];
            string option = res.width + "x" + res.height;
            options.Add(option);

            if (res.width == Screen.width && res.height == Screen.height)
            {
                currentResIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResIndex;
        resolutionDropdown.RefreshShownValue();
        slider.value = PlayerPrefs.GetFloat("volume", 0);

        Screen.fullScreen = true;
    }

    public void SetVolume(float sliderValue)
    {
        audioMixer.SetFloat("Volume", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("volume", sliderValue);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = _resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    
}