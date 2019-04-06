using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectionScript : MonoBehaviour
{
    public Text continueText;

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
            GameObject catContainer = GameObject.Find("CatContainer" + i);
            CATS.Add(catContainer);
            CATS[i-1].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (players.Count > 0)
        {
            continueText.gameObject.SetActive(true);
        }

        if (players.Count > 0 && (Input.GetButton("Start1") || Input.GetButton("Start2") || Input.GetButton("Start3") || Input.GetButton("Start4") || Input.GetKeyDown(KeyCode.KeypadEnter)))
        {
            SceneManager.LoadScene("SampleScene");
        }

        if (Input.GetButton("Select1") || Input.GetKeyDown(KeyCode.A))
        {
            // player 1 join
            players.Add("Player 1");

            // change joining text on screen
            listOfTexts[0].text = "Player 1 Joined!";

            // show character above text
            CATS[0].SetActive(true);
        }
        if (Input.GetButton("Select2") || Input.GetKeyDown(KeyCode.S))
        {
            // player 2 join
            players.Add("Player 2");

            // change joining text on screen
            listOfTexts[1].text = "Player 2 Joined!";

            CATS[1].SetActive(true);
        }
        if (Input.GetButton("Select3") || Input.GetKeyDown(KeyCode.D))
        {
            // player 3 join
            players.Add("Player 3");

            // change joining text on screen
            listOfTexts[2].text = "Player 3 Joined!";

            CATS[2].SetActive(true);
        }
        if (Input.GetButton("Select4") || Input.GetKeyDown(KeyCode.F))
        {
            // player 4 join
            players.Add("Player 4");

            // change joining text on screen
            listOfTexts[3].text = "Player 4 Joined!";

            CATS[3].SetActive(true);
        }
    }
}
