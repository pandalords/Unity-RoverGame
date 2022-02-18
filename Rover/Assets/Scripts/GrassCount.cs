using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class GrassCount : MonoBehaviour
{
    public Text counterText;

  void Update () {
    GameObject[] PercentMowed = GameObject.FindGameObjectsWithTag ("Grass");
    counterText.text = PercentMowed.Length.ToString();

}

}
