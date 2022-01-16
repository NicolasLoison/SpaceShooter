using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarGenerator : MonoBehaviour
{

    public GameObject starPrefab;

    public int maxStars;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < maxStars; i++)
        {
            Instantiate(starPrefab, transform, true);
        }
    }
}
