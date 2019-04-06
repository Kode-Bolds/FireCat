using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float playerSpeed;
    public float playerRotationSpeed;
    public int playerNumber;

    private float xAxis;
    private float yAxis;
    private float xAxisAim;
    private float yAxisAim;

	// Use this for initialization
	void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {
        xAxis = Input.GetAxisRaw("Horizontal" + playerNumber.ToString());
        yAxis = Input.GetAxisRaw("Vertical" + playerNumber.ToString());

        xAxisAim = Input.GetAxisRaw("HorizontalAim" + playerNumber.ToString());
        yAxisAim = Input.GetAxisRaw("VerticalAim" + playerNumber.ToString());

        //Move and rotate based on joystick input
        Vector3 movement = new Vector3(xAxis, 0, -yAxis) * playerSpeed * Time.deltaTime;
        transform.position += movement;

        Vector3 lookDir = new Vector3(xAxisAim, 0, -yAxisAim).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(lookDir);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, playerRotationSpeed * Time.deltaTime);

    }
}
