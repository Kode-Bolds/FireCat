using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerInput : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Mathf.Abs(Input.GetAxis("Horizontal1")) > 0.1f)
        {
            Debug.Log("Player1 moved horizontal");
        }
        if (Mathf.Abs(Input.GetAxis("Horizontal2")) > 0.1f)
        {
            Debug.Log("Player2 moved horizontal");
        }
        if (Mathf.Abs(Input.GetAxis("Vertical1")) > 0.1f)
        {
            Debug.Log("Player1 moved vertical");
        }
        if (Mathf.Abs(Input.GetAxis("Vertical2")) > 0.1f)
        {
            Debug.Log("Player2 moved vertical");
        }
        if (Mathf.Abs(Input.GetAxis("HorizontalAim1")) > 0.1f)
        {
            Debug.Log("Player1 aimed horizontal");
        }
        if (Mathf.Abs(Input.GetAxis("HorizontalAim2")) > 0.1f)
        {
            Debug.Log("Player2 aimed horizontal");
        }
        if (Mathf.Abs(Input.GetAxis("VerticalAim1")) > 0.1f)
        {
            Debug.Log("Player1 aimed vertical");
        }
        if (Mathf.Abs(Input.GetAxis("VerticalAim2")) > 0.1f)
        {
            Debug.Log("Player2 aimed vertical");
        }
    }
}
