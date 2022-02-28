using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flames : MonoBehaviour
{
    
    public SpriteRenderer flames;
    public MotorController MC;
    
    // Start is called before the first frame update
    void Start()
    {
        flames.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (MC.hasCompletedFlip == true)
        {
            flames.enabled = true;
            Debug.Log ("Flames");
        }
        else
        {
            flames.enabled = false;
        }
    }
}
