using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : MonoBehaviour
{

    public GameObject rocketPrefab;
    public GameObject[] enemies;
    public static int Load = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LaunchRocket();
        }
    }

    public void LaunchRocket()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject boss = GameObject.FindGameObjectWithTag("Boss");
        if (enemies.Length > 0 || boss != null)
        {
            if (Load >= 1)
            {
                Instantiate(rocketPrefab, transform.position, Quaternion.identity);
                SoundManager.Instance.RocketLaunchSound();
                Load--;
            }
        }
    }
}
