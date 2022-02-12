using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shooting : MonoBehaviour
{

    [SerializeField] float bulletSpeed = 10f;
    
    public Transform shotgunBarrel;
    public GameObject bulletPrefab;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    
    void OnFire(InputValue value)
    {
        GameObject bullet = Instantiate(bulletPrefab, shotgunBarrel.position, shotgunBarrel.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(shotgunBarrel.right * bulletSpeed, ForceMode2D.Impulse);
    }

    void OnSecondaryFire(InputValue value)
    {
        GameObject bullet = Instantiate(bulletPrefab, shotgunBarrel.position, shotgunBarrel.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(shotgunBarrel.right * bulletSpeed, ForceMode2D.Impulse);
    }


}
