using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    
    Rigidbody2D rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
    
    }

    void OnCollisionEnter2D(Collision2D other) 
    {
        Destroy(gameObject);
    }

}
