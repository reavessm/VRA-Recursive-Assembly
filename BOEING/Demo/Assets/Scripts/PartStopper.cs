using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartStopper : MonoBehaviour {

	GameObject go;
	Rigidbody rb;
	int stop = 0;
	Collision lastCollision = null;

	public int stoplimit = 120;
	public bool stopping = true;

	// Use this for initialization
	void Start () {
		go = gameObject;
		rb = go.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {		
	}

	// Once a collision is detected update the rigidbody value of current object
	void OnCollisionEnter(Collision other)
	{
		if (rb == null) 
		{
			if (go != null) 
			{
				rb = go.GetComponent<Rigidbody>();
			}
		}

		if (stopping) 
		{
			if (rb != null) 
			{
				if (stop < stoplimit) 
				{
					if (other.gameObject.tag == "Pickupable") 
					{
						stop++;
					}
					if (other.gameObject.name == "top") 
					{
						stop += 4;
					}
				}
				else if (stop >= stoplimit) 
				{
					if (rb != null) 
					{
						rb.freezeRotation = true;
						rb.isKinematic = true;
						stopping = false;
					}
				}
			}
		}
		else 
		{
			if ((other.gameObject.name == "top") && (lastCollision.gameObject.name != other.gameObject.name)) 
			{
				rb.velocity = Vector3.zero;
			}
			if ((other.gameObject.tag == "Pickupable") || (other.gameObject.tag == "CanTeleport")) 
			{
				rb.velocity = Vector3.zero;
			}
		}
		lastCollision = other;
	}

	// Required but unused method
	void OnCollisionStay(Collision other)
	{

	}
}
