using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int scorePlayer;
    private int _gameScenario;
    private int _nbLoop;
    public Text[] scoreTexts;
    public static GameManager Instance;
    public AudioMixer audioMixer;
    public GameObject topTurret;
    public GameObject botTurret;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de GameManager dans la scène !");
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        audioMixer.SetFloat("Volume", Mathf.Log10(PlayerPrefs.GetFloat("volume", 1)) * 20);
        MoveAsteriod.StopProduction = false;
        Time.timeScale = 1;
    }

    private void Update()
    {
        
    }

    public void AddScorePlayer(int score)
    {
        scorePlayer += score;
        _gameScenario += score;
        SetScoreText();
        GameScenario();
    }

    public void GameScenario()
    {
        switch (_gameScenario)
        {
            case 10:
                MoveAsteriod.StopProduction = true;
                EnemySpawner.Instance.StartWave(3);
                break;
            case 30:
                MoveAsteriod.StopProduction = true;
                EnemySpawner.Instance.StartWave(4);
                break;
            case 60:
                MoveAsteriod.StopProduction = true;
                EnemySpawner.Instance.StartWave(5);
                break;
            case 90:
                MoveAsteriod.StopProduction = true;
                EnemySpawner.Instance.StartWave(1);
                break;
            case 100:
                MoveAsteriod.StopProduction = true;
                EnemySpawner.Instance.StartBossWave();
                SoundManager.Instance.BossFight();
                BossHealth.Instance.maxHp *= 1.75f;
                switch (_nbLoop)
                {
                    case 0: 
                        botTurret.SetActive(true); 
                        break;
                    case 1: 
                        topTurret.SetActive(true); 
                        break;
                    default:
                        Time.timeScale *= 1.15f;
                        BossHealth.Instance.maxHp *= Time.timeScale;
                        break;
                }
                _nbLoop++;
                break;
        }
    }

    public void SetScoreText()
    {
        foreach (var scoreLabel in scoreTexts)
        {
            scoreLabel.text = scorePlayer.ToString();
        }
    }
    
    
    public void AddHighscore()
    {
        if (scorePlayer == 0) return;
        
        HighscoreEntry scoreEntry = new HighscoreEntry {score = scorePlayer};

        Highscores highscores;
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        if (jsonString == "")
        {
            highscores = new Highscores
            {
                HighscoreList = new List<HighscoreEntry>{ scoreEntry }
            };  
        }
        else highscores = JsonUtility.FromJson<Highscores>(jsonString);

        if (!highscores.HighscoreList.Contains(scoreEntry))
        {
            highscores.HighscoreList.Add(scoreEntry);
        }
        
        string json = JsonUtility.ToJson(highscores, true);
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();
    }

    public void ResetGameScenario()
    {
        _gameScenario = 0;
    }
}
