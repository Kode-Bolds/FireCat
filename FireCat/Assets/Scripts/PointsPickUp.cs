using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsPickUp : MonoBehaviour
{
    [Header("Scoring")]
    public int PointValue = 1;
    public string NameOfObjectWithScoreManager;
    public string TagOfPlayers;

    private Score _score;
    // Use this for initialization
    void Start()
    {
        GetScoreManager();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        print("I've been entered!");
        if(other.gameObject.tag == TagOfPlayers)
        {
            print("I've been entered By a dirty pussy!");
            PlayerController p = other.gameObject.GetComponent<PlayerController>();
            int playerNum = 0;
            if(p != null)
            {
                playerNum = p.playerNumber;
            }
            if(_score == null)
            {
                GetScoreManager();
            }
            _score.AddToScore(playerNum-1, PointValue);
            var parentScript = gameObject.transform.parent.GetComponent<ParentDestroyHack>();
            if (parentScript != null)
            {
                parentScript.KILLYOURSELF(); 
            }
        }
    }

    private void GetScoreManager()
    {
        GameObject gameObject = GameObject.Find(NameOfObjectWithScoreManager);
        if(gameObject != null)
        {
            _score = gameObject.GetComponent<Score>();
        }
    }
}
