using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontTireOffGround : MonoBehaviour
{
    
    CircleCollider2D frontTireCollider;

    public bool frontTireColliderIsTouchingGround;
    public bool frontTireColliderIsTouchingBouncePad = false;
    public bool frontTireColliderIsTouchingZoomPad = false;

    void Start()
    {
        frontTireCollider = GetComponent<CircleCollider2D>();
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (!frontTireCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            frontTireColliderIsTouchingGround = false;
        }
    }

    void OnTriggerExit2D(Collider2D other) 
    {
        if (!frontTireCollider.IsTouchingLayers(LayerMask.GetMask("BouncePad")))
        {
            frontTireColliderIsTouchingBouncePad = false;
        }
        if (!frontTireCollider.IsTouchingLayers(LayerMask.GetMask("ZoomPad")))
        {
            frontTireColliderIsTouchingZoomPad = false;
        }
    }

    void OnCollisionEnter2D(Collision2D other) 
    {
        if (frontTireCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            frontTireColliderIsTouchingGround = true;
        }

    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (frontTireCollider.IsTouchingLayers(LayerMask.GetMask("BouncePad")))
        {
            frontTireColliderIsTouchingBouncePad = true;
        }  
        else if (frontTireCollider.IsTouchingLayers(LayerMask.GetMask("ZoomPad")))
        {
            frontTireColliderIsTouchingZoomPad = true;
        }  
    }

}
