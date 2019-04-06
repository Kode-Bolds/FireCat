using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    //SceneManagerScript sceneManager;
    public Text timerText;
    public float timerValue;

    // Use this for initialization
    void Start()
    {
        //sceneManager = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SceneManagerScript>();
        timerText.text = timerValue.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        timerValue -= Time.deltaTime;
        timerText.text = (Mathf.Round(timerValue)).ToString();

        if (timerValue < 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }
}
