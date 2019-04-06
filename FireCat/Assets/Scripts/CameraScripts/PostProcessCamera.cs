using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostProcessCamera : MonoBehaviour
{
    [SerializeField] Material mat;

    Camera currentCamera;
    
	// Use this for initialization
	void Start () {
        currentCamera = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
		

	}


    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        mat.SetInt("_ScreenWidth", currentCamera.pixelWidth);
        mat.SetInt("_ScreenHeight", currentCamera.pixelHeight);

        // Copy the source Render Texture to the destination,
        // applying the material along the way.
        Graphics.Blit(source, destination, mat);
    }
}
