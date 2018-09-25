using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotLightController : MonoBehaviour
{
	// Use this for initialization
	private void Start ()
    {
		
	}
	
	// Update is called once per frame
	private void LateUpdate ()
    {
        transform.LookAt(Camera.main.transform.position);
	}
}
