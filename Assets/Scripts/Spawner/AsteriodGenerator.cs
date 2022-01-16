using UnityEngine;

public class AsteriodGenerator : MonoBehaviour
{

    public GameObject asteriodPrefab;

    public int maxAsteriod;

    public static AsteriodGenerator Instance;
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de EnemySpawner dans la scène !");
            return;
        }
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < maxAsteriod; i++)
        {
            Instantiate(asteriodPrefab, transform, true);
        }
    }

    public void StartProduction(int nbAsteriod)
    {
        for (int i = 0; i < nbAsteriod; i++)
        {
            Instantiate(asteriodPrefab, transform, true);
        }
    }
}
