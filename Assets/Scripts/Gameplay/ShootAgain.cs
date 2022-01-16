using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootAgain : MonoBehaviour
{
    private Vector3 _size;
    private float _elapsedTime;
    public float attackInterval;
    private void Start()
    {
        _size.x	= gameObject.GetComponent<SpriteRenderer>().bounds.size.x;
        _size.y = gameObject.GetComponent<SpriteRenderer>().bounds.size.y;
    }

    private void Update	()	{
        Shoot();
    }

    public void Shoot()
    {
        if (Time.time >= _elapsedTime)
        {
            var launcherPos = transform.position;
            var pos = new Vector2(launcherPos.x+_size.x, launcherPos.y);
            _elapsedTime = Time.time + attackInterval;
            Instantiate(Resources.Load("SpaceShipShoot"), pos, Quaternion.identity);
            SoundManager.Instance.PlayerShot();
        }
    }
}
