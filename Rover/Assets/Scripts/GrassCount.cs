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
  float totalTime = 0;
  public double roundedTotalTime;

  void Update () 
  {
    GameObject[] PercentMowed = GameObject.FindGameObjectsWithTag ("Grass");
    counterText.text = PercentMowed.Length.ToString();


    totalTime += Time.deltaTime;
    roundedTotalTime = System.Math.Round(totalTime, numberOfDecimalsInCountDown);
    //float roundedTotalTimeFloat = (float)roundedTotalTime;
    //float timeInSeconds = Mathf.FloorToInt(roundedTotalTimeFloat % 60);
    timerText.text = roundedTotalTime.ToString();

    //seconds = Mathf.FloorToInt(timevar%60);
  }

}
