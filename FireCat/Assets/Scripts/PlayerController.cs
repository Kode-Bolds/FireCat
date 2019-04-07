using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int playerNumber;

    [Header("Player Movement")]
    public float playerSpeed;
    public float playerRotationSpeed;

    [Header("Screen Wrapping")]
    public float minX;
    public float maxX;
    public float minZ;
    public float maxZ;

    [Header("Hose Stats")]
    public float sprayDistance;
    public float sprayRadius;
    public float hoseRotationSpeed;
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

    private ParticleSystem waterInstance;
    private ParticleSystem deleteInstance;
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
        Moving();
        ScreenWrap();
        Aiming();
        Particles();
        SprayRay();
    }

    /// <summary>
    /// Manages the aiming and rotation of the top half of the player
    /// </summary>
    void Aiming()
    {
        //Retrieves input from right joystick
        xAxisAim = Input.GetAxisRaw("HorizontalAim" + playerNumber.ToString());
        yAxisAim = Input.GetAxisRaw("VerticalAim" + playerNumber.ToString());

        //Rotate top half based on right joystick input
        if (!(xAxisAim == 0 && yAxisAim == 0))
        {
            Vector3 lookDir = new Vector3(xAxisAim, 0, -yAxisAim).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(lookDir);
            topHalf.rotation = Quaternion.RotateTowards(topHalf.rotation, lookRotation, hoseRotationSpeed * Time.deltaTime);
        }
    }

    /// <summary>
    /// Manages movement of player and rotation of bottom half of player
    /// </summary>
    void Moving()
    {
        //Retrieves input from left joystick
        xAxis = Input.GetAxisRaw("Horizontal" + playerNumber.ToString());
        yAxis = Input.GetAxisRaw("Vertical" + playerNumber.ToString());

        //Move based on left joystick input
        Vector3 movement = new Vector3(xAxis, 0, -yAxis) * playerSpeed * Time.deltaTime;
        transform.position += movement;

        //Rotate bottom half of player based on left joystick input
        if (!(xAxis == 0 && yAxis == 0))
        {
            Vector3 lookDir = new Vector3(xAxis, 0, -yAxis).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(lookDir);
            bottomHalf.rotation = Quaternion.RotateTowards(bottomHalf.rotation, lookRotation, playerRotationSpeed * Time.deltaTime);
        }
    }

    /// <summary>
    /// Manages water particle system
    /// </summary>
    void Particles()
    {
        //Retrieves input from right trigger
        triggerAxis = Input.GetAxisRaw("Fire" + playerNumber.ToString());

        //Activate particle system if right trigger is pressed
        if (triggerAxis < 0 && !spraying)
        {

            spraying = true;
            waterInstance = Instantiate(water, hose);
            waterInstance.Play();
        }

        //De-activate particloe system if right trigger is released
        if (!(triggerAxis < 0) && waterInstance)
        {
            waterInstance.Stop();
            deleteInstance = waterInstance;
            waterInstance = null;
            Destroy(deleteInstance, 2);
            spraying = false;
        }
    }

    /// <summary>
    /// Casts a sphere ray from the hose
    /// </summary>
    void SprayRay()
    {
        if (spraying)
        {
            //Casts sphere ray from hose in direction of top halves forward with a given spray distance and radius
            RaycastHit hit;
            if (Physics.SphereCast(hose.position, sprayRadius, topHalf.forward, out hit, sprayDistance, targetLayer))
            {
                //print("hit Node");
                hit.collider.gameObject.GetComponent<FireNode>().OnHit();
            }
        }
    }

    /// <summary>
    /// Wraps player around the screen if they go outside the bounds of the screen
    /// </summary>
    void ScreenWrap()
    {
        if(transform.position.x > maxX)
        {
            transform.position = new Vector3(minX + 1, transform.position.y, transform.position.z);
        }

        if (transform.position.x < minX)
        {
            transform.position = new Vector3(maxX - 1, transform.position.y, transform.position.z);
        }

        if (transform.position.z > maxZ)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, minZ + 1);
        }

        if (transform.position.z < minZ)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, maxZ - 1);
        }
    }
}
