using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deploy : MonoBehaviour {
	public Transform DeployPrefab;
	private Transform parts_pile;
	private Transform ghost;

	// Use this for initialization
	void Start () {
		Transform temp = DeployPrefab;
		Destroy(temp.GetComponent<Deploy>());
		//parts_pile = Instantiate(temp, new Vector3(0, 0, 0), Quaternion.identity);
		parts_pile = Instantiate(temp, null, true);
		parts_pile.gameObject.transform.position = (new Vector3(100,-7.5f,0));
		parts_pile.name = temp.name + "_parts";
		foreach (Transform element in parts_pile.transform) {
			element.name = element.name;
			element.gameObject.tag = "Pickupable";
			element.gameObject.AddComponent<Rigidbody>();
			element.gameObject.GetComponent<Rigidbody>().useGravity = false;
			element.gameObject.GetComponent<Rigidbody>().mass = 100;
			//element.gameObject.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic; 
			element.gameObject.GetComponent<Rigidbody>().isKinematic = true;
			element.gameObject.GetComponent<MeshCollider>().convex = true;
			element.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
		}
		//ghost = Instantiate(temp, new Vector3(0,0,0), Quaternion.identity);
		ghost = Instantiate(temp, null, true);		
		ghost.gameObject.transform.position = (new Vector3(90,-7.5f,0));
		ghost.name = temp.name + "_ghost";
		foreach (Transform element in ghost.transform) {
			element.name = element.name + " ghost";
			element.gameObject.tag = "Ghost";
			element.gameObject.AddComponent<Rigidbody>();
			element.gameObject.GetComponent<Rigidbody>().useGravity = false;
			element.gameObject.GetComponent<Rigidbody>().isKinematic = true;
			//element.gameObject.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic; 
			element.gameObject.GetComponent<Renderer>().material = Resources.Load("Ghost") as Material;
			Destroy(element.gameObject.GetComponent<MeshCollider>());
			element.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
		}
		Destroy(DeployPrefab.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}