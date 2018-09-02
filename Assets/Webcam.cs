using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Webcam : MonoBehaviour
{

	// Use this for initialization
	void Start()
	{
		WebCamTexture webcamTexture = new WebCamTexture();
		GetComponent<RawImage>().texture = webcamTexture;
		webcamTexture.Play();
	}

	// Update is called once per frame
	void Update()
	{

	}
}
