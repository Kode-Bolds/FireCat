using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public List<Text> listOfPlayerScoreTexts;
    List<int> listOfPlayerScores;
    int numberOfPlayers;

    // Use this for initialization
    void Start()
    {
        numberOfPlayers = listOfPlayerScoreTexts.Count;
        listOfPlayerScores = new List<int>();

        // Initialise the text objects on the screen
        for (int i = 0; i < numberOfPlayers; i++)
        {
            listOfPlayerScores.Add(0);
           // listOfPlayerScoreTexts[i].text = listOfPlayerScores[i].ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Updates the text objects on the screen with integer score values
        for (int i = 0; i < numberOfPlayers; i++)
        {
            listOfPlayerScoreTexts[i].text = listOfPlayerScores[i].ToString();
        }
    }
}
