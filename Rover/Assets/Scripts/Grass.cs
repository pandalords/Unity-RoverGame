using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : MonoBehaviour
{
    
    public BoxCollider2D grass;

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (grass.IsTouchingLayers(LayerMask.GetMask("CarBody")))
        {
            Destroy(gameObject);
        }
    }

}
