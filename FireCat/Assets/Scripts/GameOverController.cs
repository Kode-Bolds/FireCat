using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverController : MonoBehaviour {

    public SceneManagerScript s;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
        if (Input.GetButtonDown("Select1") || Input.GetButtonDown("Select2") || Input.GetButtonDown("Select3") || Input.GetButtonDown("Select4"))
        {
            s.LoadScene("SampleScene");
        }
        else if (Input.GetButtonDown("Cancel1") || Input.GetButtonDown("Cancel2") || Input.GetButtonDown("Cancel3") || Input.GetButtonDown("Cancel4"))
        {
            s.LoadScene("MainMenu");
        }
    }
}
