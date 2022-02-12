using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackTireOffGround : MonoBehaviour
{
    
    CircleCollider2D backWheel;

    public bool backWheelIsTouchingGround = true;
    public bool backTireColliderIsTouchingBouncePad = false;
    public bool backTireColliderIsTouchingZoomPad = false;

    void Start()
    {
        backWheel = GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (!backWheel.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            backWheelIsTouchingGround = false;
        }
        
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (!backWheel.IsTouchingLayers(LayerMask.GetMask("BouncePad")))    
        {
            backTireColliderIsTouchingBouncePad = false;
        }
        if (!backWheel.IsTouchingLayers(LayerMask.GetMask("ZoomPad")))    
        {
            backTireColliderIsTouchingZoomPad = false;
        }
    }

    void OnCollisionEnter2D(Collision2D other) 
    {
        if (backWheel.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            backWheelIsTouchingGround = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (backWheel.IsTouchingLayers(LayerMask.GetMask("BouncePad")))
        {
            backTireColliderIsTouchingBouncePad = true;
        }
        else if (backWheel.IsTouchingLayers(LayerMask.GetMask("ZoomPad")))
        {
            backTireColliderIsTouchingZoomPad = true;
        }
    }

}
