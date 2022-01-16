using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class FadeOut : MonoBehaviour
{
    private GameObject _impact;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
        _impact = Instantiate(Resources.Load("Impact02"), transform.position, Quaternion.identity) as GameObject;
    }

    private void Update	()	{
        Color cl = GetComponent<SpriteRenderer>().color;	
        cl.a -=	0.05f;	
        GetComponent<SpriteRenderer>().color=cl;	
        if	(cl.a<0) {	
            Destroy(gameObject);	
        }
    }	
}
