using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{

    public BoxCollider2D flag;
    
    public bool loadNextLevel = false;
    
    
    void Start()
    {
        
    }


    void Update()
    {
        
    }

   void OnTriggerEnter2D(Collider2D other)
    {
       if (flag.IsTouchingLayers(LayerMask.GetMask("CarBody")))
       { 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        loadNextLevel = true;
       }  
    }


}
