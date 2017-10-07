using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        // Update is called once per frame
       transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime); //Time.deltaTime added to increase smoothness
    }
}
