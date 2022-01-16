using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class poseShip : MonoBehaviour
{

    public new Camera camera;

    private Vector3 _topLeftCorner;
    private Vector3 _topRightCorner;
    private Vector3 _botLeftCorner;
    private Vector3 _botRightCorner;

    private Vector3 _size;
    // Start is called before the first frame update
    void Start()
    {
        _topLeftCorner = camera.ViewportToWorldPoint(new Vector3(0, 1, 0));
        _topRightCorner = camera.ViewportToWorldPoint(new Vector3(1, 1, 0));
        _botLeftCorner = camera.ViewportToWorldPoint(new Vector3(0, 0, 0));
        _botRightCorner = camera.ViewportToWorldPoint(new Vector3(1, 0, 0));
    }

    // Update is called once per frame
    private void Update()
    {
        _size.x = gameObject.GetComponent<SpriteRenderer>().bounds.size.x;
        _size.y = gameObject.GetComponent<SpriteRenderer>().bounds.size.y;
        Vector3 pos = transform.position;
        
        if (pos.x - (_size.x / 2) < _botLeftCorner.x)
        {
            pos = new Vector3(_botLeftCorner.x+_size.x/2, pos.y, pos.z);
        }
        if (pos.x + (_size.x / 2) > _botRightCorner.x)
        {
            pos = new Vector3(_botRightCorner.x-_size.x/2, pos.y, pos.z);
        }

        if (pos.y + _size.y/2 > _topLeftCorner.y)
        {
            pos = new Vector3(pos.x, _topLeftCorner.y-_size.y/2, pos.z);
        }
        if (pos.y - _size.y/2 < _botLeftCorner.y)
        {
            pos = new Vector3(pos.x, _botLeftCorner.y+_size.y/2, pos.z);
        }
        transform.position = pos;
    }
}
