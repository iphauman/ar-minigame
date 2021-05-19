using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootballGameManager : MonoBehaviour
{
    public GameObject Footballs;    // parent of instantiated footballs
    
    public GameObject Football;
    public Vector3 FootballResponwnPostion;
    public List<GameObject> FootballHolder;

    private Action InstantiateFootball;

    void Start()
    {
        FootballHolder = new List<GameObject>();

        // assign the default game mode
        InstantiateFootball = DefaultInstantiate;
    }

    // Update is called once per frame
    void Update()
    {
        CheckOutboundFootball();

        InstantiateFootball();
        
    }

    private void DefaultInstantiate()
    {
        if (FootballHolder.Count <= 0)
        {
            GameObject ball = Instantiate(Football, FootballResponwnPostion, Quaternion.identity);
            ball.transform.SetParent(Footballs.transform);
            FootballHolder.Add(ball);
        }
    }

    public void CheckOutboundFootball()
    {
        List<GameObject> Temp = new List<GameObject>();
        // check if the ball is out of bound
        foreach (GameObject ball in FootballHolder)
        {
            if (ball.transform.position.y < 0)
            {
                Temp.Add(ball);
                Destroy(ball);
            }
        }

        // remove the destroy football from the list
        foreach (var item in Temp)
        {
            FootballHolder.Remove(item);
        }
    }

    public void ResetGamePlay()
    {
        FootballHolder.Clear();
        InstantiateFootball = DefaultInstantiate;
    }
}
