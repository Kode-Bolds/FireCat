using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionScript : MonoBehaviour
{

    List<string> players;
    public List<Text> listOfTexts;

    List<GameObject> CATS;

    // Use this for initialization
    void Start()
    {
        players = new List<string>();
        CATS = new List<GameObject>();

        for (int i = 1; i < 5; i++)
        {
            GameObject gameObject = GameObject.Find("Cat1");
            CATS.Add(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Select1") || Input.GetKeyDown(KeyCode.A))
        {
            // player 1 join
            players.Add("Player 1");

            // change joining text on screen
            listOfTexts[0].text = "Player 1 has joined";

            // show character above text
            CATS[0].SetActive(true);

            // select colour?
        }
        if (Input.GetButton("Select2") || Input.GetKeyDown(KeyCode.S))
        {
            // player 2 join
            players.Add("Player 2");

            // change joining text on screen
            listOfTexts[1].text = "Player 2 has joined";
        }
        if (Input.GetButton("Select3") || Input.GetKeyDown(KeyCode.D))
        {
            // player 3 join
            players.Add("Player 3");

            // change joining text on screen
            listOfTexts[2].text = "Player 3 has joined";
        }
        if (Input.GetButton("Select3") || Input.GetKeyDown(KeyCode.F))
        {
            // player 4 join
            players.Add("Player 4");

            // change joining text on screen
            listOfTexts[3].text = "Player 4 has joined";
        }
    }
}
