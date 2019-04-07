using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public List<Text> listOfPlayerScoreTexts;
    public static List<int> listOfPlayerScores = new List<int>();
    static int numberOfPlayers;

    // Use this for initialization
    void Start()
    {
        numberOfPlayers = listOfPlayerScoreTexts.Count;

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

    public void AddToScore(int player, int amount = 1)
    {
        listOfPlayerScores[player] += amount;
    }

    public static void ResetScores()
    {
        listOfPlayerScores.Clear();
        for (int i = 0; i < numberOfPlayers; i++)
        {
            listOfPlayerScores.Add(0);
        }
    }
}
