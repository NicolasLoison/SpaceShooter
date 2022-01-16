using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PckUpHp : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb;
    private Camera _camera;
    private Vector3 _botLeftCorner;
    private Vector3 _velocity = Vector3.zero;

    public float hpBonus;
    
    // Start is called before the first frame update
    void Start()
    {
        _camera = GameObject.FindGameObjectWithTag("MainCamera").transform.GetComponent<Camera>();
        _botLeftCorner = _camera.ViewportToWorldPoint(new Vector3(0, 0, 0));
    }

    private void FixedUpdate()
    {
        Vector3 targetVelocity = new Vector2(speed,0);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref _velocity, 0.05f);
    }

    private void Update()
    {
        if (transform.position.x < _botLeftCorner.x)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ship"))
        {
            if (ShipHealth.Instance.hpShip > 0)
            {
                ShipHealth.Instance.BonusHp(hpBonus);
                SoundManager.Instance.BonusHpPickUp();
            }
            Destroy(gameObject);
        }
    }
}
