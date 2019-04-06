using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetButton("Start1") || Input.GetButton("Start2") || Input.GetButton("Start3") || Input.GetButton("Start4"))
        {
            SceneManager.LoadScene("SampleScene");
        }

        if(Input.GetButtonDown("Cancel"))
        {
            Application.Quit();
        }
    }
}
