using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointManager : MonoBehaviour
{

    public Vector2 lastCheckPointPos;
    public GameObject checkpoint1;
    public GameObject checkpoint2;
    //private static CheckPointManager instance;
    //FinishLine fl;
    //GameObject car;

    void Awake()
    {
        //if (instance == null)
        //{
        //    instance = this;
        //    DontDestroyOnLoad(instance);
        //}
        //else
        //{
        //    Destroy(gameObject);
        //}

        //Transform checkpointsTransfrom = transform.Find("Checkpoints");

        //foreach (Transform checkpointSingleTransform in checkpointsTransfrom)
        //{
        //    Debug.Log(checkpointSingleTransform);
        //}

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
