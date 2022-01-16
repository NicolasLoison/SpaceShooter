using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public static bool gameIsPaused = false;
    public GameObject pauseMenuUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused) Resume();
            else Paused();
        }
    }

    public void Paused()
    {
        SetActiveWeapons(false);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0;
        gameIsPaused = true;
    }

    public void Resume()
    {
        SetActiveWeapons(true);
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
        gameIsPaused = false;
    }
    
    public void RetryButton()
    {
        Resume();
        GameManager.Instance.AddHighscore();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void LoadMainMenu()
    {
        Resume();
        GameManager.Instance.AddHighscore();
        SceneManager.LoadScene("MainMenu");
    }

    public static void SetActiveWeapons(bool isActive)
    {
        foreach (var weapon in GameObject.FindGameObjectsWithTag("Weapon"))
        {
            if (weapon.GetComponent<ShootAgain>() is { })
            {
                weapon.GetComponent<ShootAgain>().enabled = isActive;
            }
        }
    }
}
