using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GumballMachine : MonoBehaviour
{
    
    public CircleCollider2D topArea;
    public BoxCollider2D bottomArea;
    public Rigidbody2D rb;
    public MotorController MC;

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (bottomArea.IsTouchingLayers(LayerMask.GetMask("CarBody")))
        {
            rb.AddForce(new Vector2(5f,5f), ForceMode2D.Impulse);
            rb.AddTorque(-45f);
        }
        else if (topArea.IsTouchingLayers(LayerMask.GetMask("CarBody")))
        {
            MC.Die();
        }
    }



}
