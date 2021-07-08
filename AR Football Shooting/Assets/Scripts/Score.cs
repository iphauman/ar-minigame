using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private int score;
    
    // Start is called before the first frame update
    private void Start()
    {
        score = 0;
    }

    public void AddScore(int value)
    {
        score += value;
    }

}
