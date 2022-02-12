using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shotgun : MonoBehaviour
{

    [SerializeField] Vector2 shotgunOffset = new Vector2(0f,0f);
    public Camera mainCamera;
    public EdgeCollider2D carHood;

    Vector2 mouseInput;
    Vector2 mousePosition;
    Vector2 carPosition;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

    }

    void FixedUpdate() 
    {
        GunRotation();
        FollowCar();
    }

    void GunRotation()
    {
        mousePosition = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());              //Retrieves current mouse position in relation to the camera
        Vector2 shotgunDirection = mousePosition - rb.position;                                         //Subtracts the location of the shotgun by the location of the mouse to retrieve the distance between the two objects
        float shotgunAngle = Mathf.Atan2(shotgunDirection.y, shotgunDirection.x) * Mathf.Rad2Deg;       //Finds the angle between the two objects
        rb.rotation = shotgunAngle;                                                                     //Applies angle to rigidbody
    }

    void FollowCar()
    {
        carPosition = carHood.transform.position;
        transform.position = Vector2.MoveTowards(transform.position, carPosition + shotgunOffset, 0.3f);
    }

}
