using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Football : MonoBehaviour
{
    public float depthBoundary = 0f;

    public enum FootballStatus
    {
        Dribbling,
        Stopping
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player interact with football.");
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log(other.collider.name);
    }
}