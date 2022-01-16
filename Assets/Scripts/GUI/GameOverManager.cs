using System;
using UnityEngine;
using UnityEngine.SceneManagement;
// ReSharper disable All

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverUI;
    public static GameOverManager Instance;
    private void Awake()
    {
        if (Instance  != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de GameOverManager dans la  scène");
            return;
        }
        Instance = this;
    }

    public void GameOver()
    {
        SoundManager.Instance.Defeat();
        gameOverUI.SetActive(true);
        PauseMenu.SetActiveWeapons(false);
        foreach (var enemy in GameObject.FindGameObjectsWithTag("EnemyWeapon"))
        {
            enemy.GetComponent<EnemyGun>().enabled = false;
        }
    }

    public void RetryButton()
    {
        GameManager.Instance.AddHighscore();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        gameOverUI.SetActive(false);
    }
    public void MainMenuButton()
    {
        GameManager.Instance.AddHighscore();
        SceneManager.LoadScene("MainMenu");
    }

    public void VoirHighsores()
    {
        GameManager.Instance.AddHighscore();
        SceneManager.LoadScene("MainMenu");
    }
}
