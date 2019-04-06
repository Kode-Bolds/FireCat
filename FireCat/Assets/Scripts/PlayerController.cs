using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float playerSpeed;

    private CharacterController character;

    private float xAxis;
    private float yAxis;

	// Use this for initialization
	void Start ()
    {
        character = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        xAxis = Input.GetAxisRaw("Horizontal");
        yAxis = Input.GetAxisRaw("Vertical");

        if(!(xAxis == 0 && yAxis == 0))
        {
        //Move and rotate based on joystick input
        Vector3 movement = new Vector3(xAxis, 0, yAxis) * playerSpeed * Time.deltaTime;
        Vector3 lookDir = new Vector3(movement.x, 0, movement.z);
        transform.rotation = Quaternion.LookRotation(lookDir);
        character.Move(movement);
        }
    }
}
