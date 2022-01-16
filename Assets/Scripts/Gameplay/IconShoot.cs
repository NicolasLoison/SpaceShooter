using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconShoot : MonoBehaviour
{
    public GameObject rocketLauncher;
    public Button rocketButton;

    private void Update()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject boss = GameObject.FindGameObjectWithTag("Boss");
        if (enemies.Length > 0 || boss != null)
        {
            if (RocketLauncher.Load >= 1) rocketButton.interactable = true;
        }
        else
        {
            rocketButton.interactable = false;
        }
    }

    public void RocketButton()
    {
        rocketLauncher.GetComponent<RocketLauncher>().LaunchRocket();
    }
}
