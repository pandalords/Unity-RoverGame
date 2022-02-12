using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    
    public CircleCollider2D headCollider;
    public MotorController motorController;
    CheckPointManager cPM;

    void Start()
    {
        cPM = GameObject.FindGameObjectWithTag("CPM").GetComponent<CheckPointManager>();
    }
    
    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            motorController.Die();
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

}
