using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class GrassCount : MonoBehaviour
{
  [SerializeField] int numberOfDecimalsInCountDown = 1;
  public Text counterText;
  public TMP_Text timerText;
  public MotorController MC;
  float totalTime = 0;
  public float levelCompleteTime;
  public double roundedTotalTime;

  void Update () 
  {
    GameObject[] PercentMowed = GameObject.FindGameObjectsWithTag ("Grass");
    counterText.text = PercentMowed.Length.ToString();

    if (MC.hasDied && !MC.hasActivatedACheckpoint)
    {
      totalTime = 0;
    }
    else if (MC.levelFinished)
    {
      roundedTotalTime = levelCompleteTime;
      //Debug.Log(levelCompleteTime);
    }
    else
    {
    totalTime += Time.deltaTime;
    roundedTotalTime = System.Math.Round(totalTime, numberOfDecimalsInCountDown);
    //float roundedTotalTimeFloat = (float)roundedTotalTime;
    //float timeInSeconds = Mathf.FloorToInt(roundedTotalTimeFloat % 60);
    timerText.text = roundedTotalTime.ToString();
    }

    //seconds = Mathf.FloorToInt(timevar%60);
  }

}
