using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyShoot : MonoBehaviour
{

    public Rigidbody2D rb;
    private Vector3 _botLeftCorner;

    public int speed;
    private Vector2 _direction;
    private Vector3 _size;
    private bool _isReady;

    private Vector3 _velocity = Vector3.zero;

    // Start is called before the first frame update
    private void Start()
    {
        if (Camera.main is { }) _botLeftCorner = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
    }

    // Update is called once per frame
    private void Update()
    {
        if (_isReady)
        {
            Vector2 pos = transform.position;
            pos += _direction * speed * Time.deltaTime;
            transform.position = pos; 
            if (pos.x < _botLeftCorner.x)
            {
                Destroy(gameObject);
            }
        }
    }
    // private void FixedUpdate()
    // {
    //     if (_isReady)
    //     {
    //         Vector2 targetVelocity = transform.position;
    //         targetVelocity += _direction * speed * Time.deltaTime;
    //         rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref _velocity, 0.05f);
    //     }
    // }

    public void SetDirection(Vector2 direction)
    {
        _direction = direction.normalized;
        transform.eulerAngles = new Vector3(0, 0, GetAngleFromVectorFloat(direction));
        _isReady = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.transform.CompareTag("Ship")) return;
        Destroy(gameObject);
        ShipHealth.Instance.TakeDamage(25);
        SoundManager.Instance.AsteriodExplosion();
    }
    
    public static float GetAngleFromVectorFloat(Vector3 dir) {
        // Calcule la rotation nécessaire pour que le laser pointe vers le vaisseau en fonction de sa direction
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;
        return n;
    }
}
