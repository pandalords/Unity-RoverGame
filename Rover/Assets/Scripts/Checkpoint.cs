using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    public CheckPointManager checkPointManager;
    public BoxCollider2D fuelCanister;
    public MotorController MC;

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (fuelCanister.IsTouchingLayers(LayerMask.GetMask("CarBody")))
        {
            MC.hasActivatedACheckpoint = true;
            checkPointManager.lastCheckPointPos = transform.position;
            Destroy(gameObject);
        }
    }
}
