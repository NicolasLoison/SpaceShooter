using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveShip : MonoBehaviour
{
    public Vector2 speed;
    public Rigidbody2D rb;
    public PolygonCollider2D polygonCollider2D;
    public FloatingJoystick joystick;
    
    private Vector2 _movement;
    
    public static moveShip Instance;
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de moveShip dans la scène !");
            return;
        }

        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        float inputY = Input.GetAxis("Vertical");
        float inputX = Input.GetAxis("Horizontal");
        
        if (inputX == 0 && inputY == 0)
        {
            inputX = joystick.Horizontal;
            inputY = joystick.Vertical;
        }
        _movement = new Vector2(
            speed.x * inputX,
            speed.y * inputY
            );
        rb.velocity = _movement;
    }
}
