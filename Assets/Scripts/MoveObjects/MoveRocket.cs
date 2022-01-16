using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRocket : MonoBehaviour
{
    public Rigidbody2D rb;
    private Vector3 _botLeftCorner;
    public GameObject[] enemies;
    private GameObject _boss;

    public int speed;
    private Vector2 _direction;
    private Vector3 _size;
    private bool _isReady;
    public float tauxDropHp;

    private Vector3 _velocity = Vector3.zero;

    // Start is called before the first frame update
    private void Start()
    {
        if (Camera.main is { }) _botLeftCorner = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
    }

    // Update is called once per frame
    private void Update()
    {
        _boss = GameObject.FindGameObjectWithTag("Boss");
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject enemy = FindTarget();
        Vector2 pos = transform.position;
        //Le missile cible l'enemi en fonction de son déplacement actuel (rb.velocity) et de sa position
        var enemyPos = (Vector2)enemy.transform.position; 
        var direction = enemyPos - pos;
        transform.LookAt(enemyPos);
        SetDirection(direction);
        pos += _direction * speed * Time.deltaTime;
        transform.position = pos;
    }

    private GameObject FindTarget()
    {
        GameObject enemy;
        if (_boss is { })
        {
            enemy = GameObject.FindGameObjectWithTag("Boss");
        }
        else if (enemies.Length != 0) {
            enemy = enemies[0];
        }
        else {
            // Si un laser détruit l'ennemi pendant que le missile avance, l'ennemi est remplacé par un astériod.
            enemy = GameObject.FindGameObjectWithTag("Asteriod");
        }

        return enemy;
    }

    public void SetDirection(Vector2 direction)
    {
        _direction = direction.normalized;
        transform.eulerAngles = new Vector3(0, 0, GetAngleFromVectorFloat(direction)-90);
        // Le sprite du missile doit de base recevoir une rotation en z de -90 donc on l'applique ici en plus
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
            GameManager.Instance.AddScorePlayer(5);
            Instantiate(Resources.Load("Impact03"), transform.position, Quaternion.identity);
            SoundManager.Instance.RocketExplosionSound();
            RocketLauncher.Load++;
            BonusSpawner.Instance.SpawnHealthBonus(tauxDropHp, gameObject);
        }

        if (collision.transform.CompareTag("Asteriod"))
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
            GameManager.Instance.AddScorePlayer(1);
            Instantiate(Resources.Load("Impact01"), transform.position, Quaternion.identity);
            SoundManager.Instance.RocketExplosionSound();
            RocketLauncher.Load++;
        }
        
        if (collision.transform.CompareTag("Boss"))
        {
            Destroy(gameObject);
            BossHealth.Instance.TakeDamage(25);
            Instantiate(Resources.Load("Impact03"), transform.position, Quaternion.identity);
            SoundManager.Instance.RocketExplosionSound();
            RocketLauncher.Load++;
        }
    }
    
    public static float GetAngleFromVectorFloat(Vector3 dir) {
        // Calcule la rotation nécessaire pour que le laser pointe vers le vaisseau en fonction de sa direction
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;
        return n;
    }
}
