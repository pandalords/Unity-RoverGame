using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointManager : MonoBehaviour
{

    public Vector2 lastCheckPointPos;
    private static CheckPointManager instance;
    //FinishLine fl;
    //GameObject car;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        //fl = GameObject.FindGameObjectWithTag("FL").GetComponent<FinishLine>();
        //car = FindObjectOfType.GameObject<Car>();
    }

    void Update()
    {
        //if (fl.loadNextLevel == true)
        //{
            //lastCheckPointPos = 
        //}
    }
}
