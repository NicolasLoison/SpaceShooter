using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject bossPrefab;
    public GameObject bossHpUI;
    public static EnemySpawner Instance;
    public GameObject[] enemies;
    private GameObject _boss;
    private bool _waveEnCours;
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de EnemySpawner dans la scène !");
            return;
        }
        Instance = this;
    }

    private void Update()
    {
        if (_waveEnCours)
        {
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
            _boss = GameObject.FindGameObjectWithTag("Boss");
            if (enemies.Length == 0 && _boss == null)
            {
                _waveEnCours = false;
                MoveAsteriod.StopProduction = false;
                AsteriodGenerator.Instance.StartProduction(5);
            }
        }
    }

    public void StartWave(int nbEnemies)
    {
        _waveEnCours = true;
        for (int i = 0; i < nbEnemies; i++)
        {
            Instantiate(enemyPrefab, transform, true);
        }
    }

    public void StartBossWave()
    {
        // bossHpUI.SetActive(true);
        _waveEnCours = true;
        Instantiate(bossPrefab, transform, true);
    }
}
