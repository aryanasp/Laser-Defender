using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{

    //state
    int score = 0;
    int i = 0;
    public int Score { get => score; set => score = value; }

    // Start is called before the first frame update
    void Awake()
    {
        SetUpSingleton();
    }

    private void SetUpSingleton()
    {
        int numberGameSeesions = FindObjectsOfType(GetType()).Length;
        if(numberGameSeesions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Update is called once per frame
    public void AddToScore(int scoreValue)
    {
        Score += scoreValue;
    }

    public void ResetGame()
    {
        Destroy(gameObject);   
    }
}
