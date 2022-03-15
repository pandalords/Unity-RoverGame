using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GumballMachine : MonoBehaviour
{
    
    [SerializeField] float timeObjectStaysInGameAfterTouch = 3f;
    public CircleCollider2D topArea;
    public BoxCollider2D bottomArea;
    public Rigidbody2D rb;
    public MotorController MC;

    void DestroyObject()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (bottomArea.IsTouchingLayers(LayerMask.GetMask("CarBody")))
        {
            rb.AddForce(new Vector2(10f,20f), ForceMode2D.Impulse);
            rb.AddTorque(-90f);
            Invoke("DestroyObject", timeObjectStaysInGameAfterTouch);
        }
        else if (topArea.IsTouchingLayers(LayerMask.GetMask("CarBody")))
        {
            MC.Die();
        }
    }



}
