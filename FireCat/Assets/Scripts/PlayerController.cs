using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int playerNumber;

    [Header("Player Movement")]
    public float playerSpeed;
    public float playerRotationSpeed;

    [Header("Hose Stats")]
    public float sprayDistance;
    public LayerMask targetLayer;
    public ParticleSystem water;

    private float xAxis;
    private float yAxis;
    private float xAxisAim;
    private float yAxisAim;
    private float triggerAxis;

    private Transform topHalf;
    private Transform bottomHalf;
    private Transform hose;

    private bool spraying;

	// Use this for initialization
	void Start ()
    {

        topHalf = transform.GetChild(0);
        bottomHalf = transform.GetChild(1);
        hose = topHalf.transform.GetChild(0);

        spraying = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
        xAxis = Input.GetAxisRaw("Horizontal" + playerNumber.ToString());
        yAxis = Input.GetAxisRaw("Vertical" + playerNumber.ToString());

        xAxisAim = Input.GetAxisRaw("HorizontalAim" + playerNumber.ToString());
        yAxisAim = Input.GetAxisRaw("VerticalAim" + playerNumber.ToString());

        triggerAxis = Input.GetAxisRaw("Fire" + playerNumber.ToString());

        //Move based on left joystick input
        Vector3 movement = new Vector3(xAxis, 0, -yAxis) * playerSpeed * Time.deltaTime;
        transform.position += movement;

        //Rotate bottom half based on right joystick input
        if (!(xAxis == 0 && yAxis == 0))
        {
            Vector3 lookDir = new Vector3(xAxis, 0, -yAxis).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(lookDir);
            bottomHalf.rotation = Quaternion.RotateTowards(bottomHalf.rotation, lookRotation, playerRotationSpeed * Time.deltaTime);
        }

        //Rotate top half based on right joystick input
        if (!(xAxisAim == 0 && yAxisAim == 0))
        {
            Vector3 lookDir = new Vector3(xAxisAim, 0, -yAxisAim).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(lookDir);
            topHalf.rotation = Quaternion.RotateTowards(topHalf.rotation, lookRotation, playerRotationSpeed * Time.deltaTime);
        }

        //Fire water jet if right trigger is pressed down
        if(triggerAxis < 0 && !spraying)
        {
            RaycastHit hit;

            spraying = true;
            water.Play();

            if(Physics.SphereCast(hose.position, 1, topHalf.forward, out hit, sprayDistance, targetLayer))
            {
                
            }
        }

        if(!(triggerAxis < 0))
        {
            water.Stop();
            spraying = false;
        }
    }
}
