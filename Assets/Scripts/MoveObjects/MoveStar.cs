using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveStar : MonoBehaviour
{
    public Rigidbody2D rb;

    private Camera _camera;
    private Vector3 _velocity = Vector3.zero;
    private Vector3 _topLeftCorner;
    private Vector3 _botLeftCorner;
    private Vector3 _botRightCorner;
    private Vector3 _size;
    private float _speed;

    // Start is called before the first frame update
    void Start()
    {
        _camera = GameObject.FindGameObjectWithTag("MainCamera").transform.GetComponent<Camera>();
        _topLeftCorner = _camera.ViewportToWorldPoint(new Vector3(0, 1, 0));
        _botLeftCorner = _camera.ViewportToWorldPoint(new Vector3(0, 0, 0));
        _botRightCorner = _camera.ViewportToWorldPoint(new Vector3(1, 0, 0));
        transform.position = new Vector3(Random.Range(_botRightCorner.x, _botRightCorner.x*2f), Random.Range(_botLeftCorner.y, _topLeftCorner.y), 0);
        _speed = Random.Range(-15, -2);
    }

    private void FixedUpdate()
    {
        Vector3 targetVelocity = new Vector2(_speed,0);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref _velocity, 0.05f);
    }

    private void Update()
    {
        _size.x = gameObject.GetComponent<SpriteRenderer>().bounds.size.x;
        _size.y = gameObject.GetComponent<SpriteRenderer>().bounds.size.y;
        Vector3 pos = transform.position;

        if (!(pos.x < _botLeftCorner.x)) return;
        Instantiate(Resources.Load("Star"), Vector3.zero, Quaternion.identity);
        Destroy(gameObject);
    }
}
