using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusSpawner : MonoBehaviour
{
    public GameObject bonusHpPrefab;
    public static BonusSpawner Instance;
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de BonusSpawner dans la scène !");
            return;
        }
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void SpawnHealthBonus(float pourcentage, GameObject parent)
    {
        if (Random.value >= 1-pourcentage)
        {
            Instantiate(bonusHpPrefab, parent.transform.position, Quaternion.identity);
        }
    }
}
