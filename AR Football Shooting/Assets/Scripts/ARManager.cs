using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class ARManager : MonoBehaviour
{
    public bool tracked;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (tracked)
        {
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0;
        }
    }

    public void Tracked()
    {
        tracked = true;
    }

    public void Lost()
    {
        tracked = false;
    }
}
