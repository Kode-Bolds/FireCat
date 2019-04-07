using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreGetter : MonoBehaviour
{
    [Header("Display")]
    public Text DisplayText;
    // Use this for initialization
    void Start()
    {
        int playerNum = Score.listOfPlayerScores.Count;
        if (playerNum > 0)
        {
            int highNum = 0;
            int highScore = 0;
            for(int i = 0; i < playerNum; i++)
            {
                if(Score.listOfPlayerScores[i]>highScore)
                {
                    highNum = i + 1;
                    highScore = Score.listOfPlayerScores[i];
                }
            }
            if (highScore > 0)
            {
                DisplayText.text = "Player " + highNum + " Won with " + highScore + " of Points!";
            }
            else
            {
                DisplayText.text = "No one scored any points! Pathetic!";
            }
        }
        else
        {
            DisplayText.text = "there were no players! WHAT DO YOU WANT FROM ME?";
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
