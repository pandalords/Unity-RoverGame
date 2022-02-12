using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{

    public BoxCollider2D body;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (body.IsTouchingLayers(LayerMask.GetMask("Bullet")))
        {
            Destroy(gameObject);
        }
    }

}
