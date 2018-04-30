using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Deploy : MonoBehaviour {
	private GlobalVariables variables;
	private Vector3 PartsOffset;
	private Vector3 GhostOffset;
    private GameObject table;
    private float tableHeight = 0.25f;

    private Vector3 OriginalLocation;
    private Vector3 tableLocation;
	private Transform DeployPrefab;
	private Transform parts_pile;
	private Transform ghost;
    private Bounds bounds;
	private float timeleft = 5.0f;
	private bool timer = true;
	public bool gravityMode = false;
	public bool separation = false;

    private Vector3 center = Vector3.zero;

	void Awake(){
		variables = GameObject.Find("GlobalVariables").GetComponent<GlobalVariables>();
		GhostOffset = variables.GetGhostOffset();
		PartsOffset = variables.GetPartsOffset();
		table = variables.GetTable();
		tableHeight = variables.GetTableHeight();
		gravityMode = variables.GetGravityMode();
		separation = variables.GetSeperation();
	}

	// Use this for initialization
	void Start () {
		try {
			DeployPrefab = gameObject.transform;
			OriginalLocation = DeployPrefab.position;
			tableLocation = table.transform.position;
			Transform temp = DeployPrefab;
			Destroy(temp.GetComponent<Deploy>());
			//parts_pile = Instantiate(temp, new Vector3(0, 0, 0), Quaternion.identity);
			parts_pile = Instantiate(temp, null, true);
			parts_pile.gameObject.transform.position = (OriginalLocation + PartsOffset);
			parts_pile.gameObject.transform.position += new Vector3(0, tableHeight, 0);
			parts_pile.name = temp.name + "_parts";
			foreach (Transform element in parts_pile.transform) {
				element.name = element.name;
				element.gameObject.tag = "Pickupable";
				element.gameObject.AddComponent<Rigidbody>();
				if (element.gameObject.GetComponent<MeshCollider>() == null) {
					element.gameObject.AddComponent<MeshCollider>();
				}
				element.gameObject.GetComponent<Rigidbody>().useGravity = false;
				element.gameObject.GetComponent<Rigidbody>().mass = 100;
				//element.gameObject.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic; 
				element.gameObject.GetComponent<Rigidbody>().isKinematic = false;
				element.gameObject.GetComponent<MeshCollider>().convex = true;
				element.gameObject.AddComponent<PartStopper>();
				center += element.gameObject.GetComponent<Renderer>().bounds.center;
			}
			center /= parts_pile.transform.childCount; // center is average center of parts pile
			bounds = new Bounds(center, Vector3.zero);

			foreach (Transform element in parts_pile.transform)
			{
				bounds.Encapsulate(element.gameObject.GetComponent<Renderer>().bounds);
			}

			table.transform.localScale = new Vector3(bounds.size.x,tableHeight,bounds.size.z);
			//Debug.Log("Center: " + center + " Parts Pos " + parts_pile.transform.position);
			//Debug.Log("Adding: " + (center.x + parts_pile.transform.position.x));
			table.transform.localPosition = new Vector3(center.x, -tableHeight/2f, center.z);

			foreach (Transform part in table.transform) {
				part.gameObject.AddComponent<Rigidbody>();
				part.gameObject.GetComponent<Rigidbody>().mass = 1000;
				part.gameObject.GetComponent<Rigidbody>().centerOfMass = Vector3.zero;
				part.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
				part.gameObject.GetComponent<Rigidbody>().isKinematic = true;
				part.gameObject.AddComponent<BoxCollider>();
				part.gameObject.GetComponent<BoxCollider>().enabled = true;
			}

			if (gravityMode) {
				foreach (Transform element in parts_pile.transform) {
					Debug.Log("Gravity Mode On!");
					element.gameObject.GetComponent<Rigidbody>().useGravity = true;
					element.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(UnityEngine.Random.Range(-10F, 10F), UnityEngine.Random.Range(-10F, 10F), UnityEngine.Random.Range(-10F, 10F));
					element.gameObject.GetComponent<Rigidbody>().drag = 50;
				}
			}
			else {
				foreach (Transform element in parts_pile.transform) {
					Debug.Log("Gravity Mode Off!");
					element.gameObject.GetComponent<Rigidbody>().useGravity = false;
					element.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
					element.gameObject.GetComponent<Rigidbody>().freezeRotation = true;
					if (!separation) {
						element.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
						element.gameObject.GetComponent<Rigidbody>().isKinematic = true;
					}
					element.gameObject.GetComponent<Rigidbody>().drag = 30;
				}
			}


			//ghost = Instantiate(temp, new Vector3(0,0,0), Quaternion.identity);
			ghost = Instantiate(temp, null, true);		
			ghost.gameObject.transform.position = (OriginalLocation + PartsOffset + GhostOffset);
			ghost.name = temp.name + "_ghost";
			foreach (Transform element in ghost.transform) {
				element.name = element.name + " ghost";
				element.gameObject.tag = "Ghost";
				element.gameObject.AddComponent<Rigidbody>();
				if (element.gameObject.GetComponent<MeshCollider>() == null) {
					element.gameObject.AddComponent<MeshCollider>();
				}
				element.gameObject.GetComponent<Rigidbody>().useGravity = false;
				element.gameObject.GetComponent<Rigidbody>().isKinematic = true;
				//element.gameObject.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic; 
				element.gameObject.GetComponent<Renderer>().material = Resources.Load("Ghost") as Material;
				Destroy(element.gameObject.GetComponent<MeshCollider>());
				element.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
			}
			Destroy(DeployPrefab.gameObject);
		}
		catch (MissingComponentException e) {
			Debug.LogError("The Deploy script is attached to invalid model: . Please use an appropriate model and try again. The error is as follows:\n" + e.Message);
			UnityEditor.EditorApplication.isPlaying = false;
			Application.Quit();
		}
	}
	
	// Update is called once per frame
	void Update () {
	}
}