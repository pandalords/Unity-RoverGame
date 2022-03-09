using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class FinishLine : MonoBehaviour
{

    public BoxCollider2D flag;
    
    //public bool loadNextLevel = false;
    
    public GameObject levelCompleteBox;
    public Animator animator;
    public GrassCount GC;
    //public TMP_Text levelCompleteText;
    public MotorController MC;
    public TextMeshProUGUI timeText;
    
    public void LevelCompletePopUp()
    {
        //levelCompleteBox.SetActive(true);
        //levelCompleteText.text = text;
        //animator.SetTrigger("popUpBox");
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ReloadCurrentLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

   void OnTriggerEnter2D(Collider2D other)
    {
       if (flag.IsTouchingLayers(LayerMask.GetMask("CarBody")))
       { 
        levelCompleteBox.SetActive(true);
        MC.levelFinished = true;
        timeText.text = "Time: " + GC.timerText.text;
       }  
    }


}
