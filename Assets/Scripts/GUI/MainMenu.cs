using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string levelToLoad;
    public GameObject settingsWindow;
    public GameObject spaceGameTitle;
    public GameObject leaderboard;
    public AudioMixer audioMixer;

    private void Start()
    {
        audioMixer.SetFloat("Volume", Mathf.Log10(PlayerPrefs.GetFloat("volume", 1)) * 20);
    }
    public void StartGame()
    {
        SceneManager.LoadScene(levelToLoad);
    }
    
    public void HighScoreButton()
    {
        leaderboard.SetActive(true);
        spaceGameTitle.SetActive(false);
    }

    public void SettingsButton()
    {
        settingsWindow.SetActive(true);
        spaceGameTitle.SetActive(false);
    }
    public void CloseSettingsWindow()
    {
        spaceGameTitle.SetActive(true);
        settingsWindow.SetActive(false);
    }   
    
    public void CloseLeaderBoard()
    {
        spaceGameTitle.SetActive(true);
        leaderboard.SetActive(false);
    }  

    public void QuitGame()
    {
        Application.Quit();
    }
}