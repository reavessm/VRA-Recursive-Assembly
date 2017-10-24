using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
	private Camera mycam;
	// Use this for initialization
	void Start ()
	{
		mycam = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt(mycam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, mycam.nearClipPlane)), Vector3.up);
	}
}
