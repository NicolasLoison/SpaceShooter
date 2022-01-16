using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// ReSharper disable All

public class HighscoreTable : MonoBehaviour
{
    public Transform entryContainer;
    public Transform entryTemplate;
    public GameObject emptyHighscores;
    private List<Transform> highscoreEntryTransformList;
    private List<HighscoreEntry> highscoreEntryList;
    private void Awake()
    {
        entryTemplate.gameObject.SetActive(false);
        
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);
        if (highscores == null)
        {
            emptyHighscores.SetActive(true);
            return;
        }

        // Tri décroissant des scores
        for (int i = 0; i < highscores.HighscoreList.Count; i++)
        {
            for (int j = i + 1; j < highscores.HighscoreList.Count; j++)
            {
                if (highscores.HighscoreList[j].score > highscores.HighscoreList[i].score)
                {
                    // Swap
                    HighscoreEntry tmp = highscores.HighscoreList[i];
                    highscores.HighscoreList[i] = highscores.HighscoreList[j];
                    highscores.HighscoreList[j] = tmp;
                }
            }
        }

        highscoreEntryTransformList = new List<Transform>();
        for (int i = 0; i < 4; i++)
        {
            if (i < highscores.HighscoreList.Count) // On vérifie qu'il y a au moins i score dans la liste de score
                CreateHighscoreEntryTransform(highscores.HighscoreList[i], entryContainer, highscoreEntryTransformList);
        }
    }

    private void CreateHighscoreEntryTransform(HighscoreEntry highscoreEntry, Transform container, List<Transform> transformList)
    {
        float templateHeight = entryTemplate.GetComponent<RectTransform>().rect.height;
        Transform entryTransform = Instantiate(entryTemplate, container);
        entryTransform.position = Vector3.zero;
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count + 1;
        string rankString;
        switch (rank)
        {
            default:
                rankString = rank + "TH"; 
                break;
            case 1:rankString = "1ST"; break;
            case 2: rankString = "2ND"; break;
            case 3: rankString = "3RD"; break;
        }
        entryTransform.Find("posText").GetComponent<Text>().text = rankString;
        
        int score = highscoreEntry.score;
        entryTransform.Find("scoreText").GetComponent<Text>().text = score.ToString();
        
        transformList.Add(entryTransform);
    }
}