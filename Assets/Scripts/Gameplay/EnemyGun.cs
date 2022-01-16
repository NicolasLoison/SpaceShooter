using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    public GameObject enemyShoot;

    public float attackInterval;

    private float _elapsedTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= _elapsedTime)
        {
            _elapsedTime = Time.time + attackInterval;
            ShootEnemyLaser();
        }
    }

    private void ShootEnemyLaser()
    {
        GameObject playerShip = GameObject.FindGameObjectWithTag("Ship");
        if (playerShip != null)
        {
            GameObject laserBeam = Instantiate(enemyShoot);
            SoundManager.Instance.EnemyShot();
            laserBeam.transform.position = transform.position;
            // Légère prédiction en fonction du déplacement actuel (rb.velocity) du vaisseau
            var playerPos = (Vector2)playerShip.transform.position 
                            + playerShip.GetComponent<Rigidbody2D>().velocity * 0.5f; 
            Vector2 direction = playerPos - (Vector2)laserBeam.transform.position;
            laserBeam.transform.LookAt(playerPos);
            laserBeam.GetComponent<EnemyShoot>().SetDirection(direction);
        }
    }
}
