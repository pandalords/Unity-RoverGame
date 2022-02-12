using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    
    public MotorController trickChecker;
    public TMP_Text scoreText;
    float score = 0f;

    void Update()
    {
        scoreText.text = score.ToString();
        PointsForAirTime();
    }

    void PointsForAirTime()
    {
        if (trickChecker.isInAir && trickChecker.isInSlowMo)
        {
            score += Mathf.Round((1f * Time.time) * trickChecker.TimeSpeedReduction);
        }
        else if (trickChecker.isInAir)
        {
            score += Mathf.Round(1f * Time.time);
        }
    }

}
