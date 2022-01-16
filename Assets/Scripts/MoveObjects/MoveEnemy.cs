using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemy : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb;
    private Camera _camera;
    private GameObject _enemyStop;

    private Vector3 _botLeftCorner;
    private Vector3 _botRightCorner;
    private Vector3 _topLeftCorner;

    private Vector3 _size;
    private Vector3 _velocity = Vector3.zero;
    
    // Start is called before the first frame update
    void Start()
    {
        _size.x = gameObject.GetComponent<SpriteRenderer>().bounds.size.x;
        _size.y = gameObject.GetComponent<SpriteRenderer>().bounds.size.y;
        _camera = GameObject.FindGameObjectWithTag("MainCamera").transform.GetComponent<Camera>();
        _topLeftCorner = _camera.ViewportToWorldPoint(new Vector3(0, 1, 0));
        _botLeftCorner = _camera.ViewportToWorldPoint(new Vector3(0, 0, 0));
        _botRightCorner = _camera.ViewportToWorldPoint(new Vector3(1, 0, 0));
        transform.position = new Vector3(Random.Range(_botRightCorner.x, _botRightCorner.x*1.5f), 
            Random.Range(_botLeftCorner.y + _size.y, _topLeftCorner.y - _size.y), 0);
        _enemyStop = GameObject.Find("EnemyStop");
    }

    private void FixedUpdate()
    {
        Vector3 targetVelocity = new Vector2(speed,0);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref _velocity, 0.05f);
    }

    private void Update()
    {
        Vector3 pos = transform.position;

        if (pos.x + _size.x < _botLeftCorner.x)
        {
            Destroy(gameObject);
            GameManager.Instance.AddScorePlayer(-5);
        }
        else if (pos.x < _enemyStop.transform.position.x)
        {
            speed = -0.75f;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.transform.CompareTag("Ship") && !collision.transform.CompareTag("Weapon")) return;
        ShipHealth.Instance.TakeDamage(50);
        SoundManager.Instance.RocketExplosionSound();
        Destroy(gameObject);
    }
}
