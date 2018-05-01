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
	private Vector3 center = Vector3.zero;

	public bool gravityMode = false;
	public bool separation = false;


	// Runs before 'Start'
	// Used to pull variables from 'GlobalVariables'
	void Awake() {
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
			Destroy(temp.GetComponent<Deploy>()); // Get rid of 'Deploy' Script
			//parts_pile = Instantiate(temp, new Vector3(0, 0, 0), Quaternion.identity);
			parts_pile = Instantiate(temp, null, true); // Create GameObject based on prefab
			// Move parts based on 'PartsOffset'
			// Configurable in 'GlobalVariables'
			parts_pile.gameObject.transform.position = (OriginalLocation + PartsOffset); 
			// Move parts on to table height
			parts_pile.gameObject.transform.position += new Vector3(0, tableHeight, 0);
			// Append name to make distiguish from parts and ghostie bois
			parts_pile.name = temp.name + "_parts";
			
			// Foreach part in total assembly, assign Unity
			// components to make them behave
			foreach (Transform element in parts_pile.transform) 
			{
				element.name = element.name; // Delete?
				// Can only pick things up with certain tag
				element.gameObject.tag = "Pickupable";
				element.gameObject.AddComponent<Rigidbody>();
				// Force a Mesh Collider
				if (element.gameObject.GetComponent<MeshCollider>() == null) 
				{
					element.gameObject.AddComponent<MeshCollider>();
				}
				element.gameObject.GetComponent<Rigidbody>().useGravity = false;
				element.gameObject.GetComponent<Rigidbody>().mass = 100;
				element.gameObject.GetComponent<Rigidbody>().isKinematic = false;
				element.gameObject.GetComponent<MeshCollider>().convex = true;
				element.gameObject.AddComponent<PartStopper>();
				// Find aggregate center of each part for tabe placement
				center += element.gameObject.GetComponent<Renderer>().bounds.center;
			}
			// Divide aggregate center by number of parts in assembly to find average center
			center /= parts_pile.transform.childCount; // center is average center of parts pile
			// Determine bounds for sizing of table
			bounds = new Bounds(center, Vector3.zero);

			// Each part in parts_pile has bounds automatically changed by Unity
			foreach (Transform element in parts_pile.transform)
			{
				// Unity's 'Encapsulate' function will enlarge 'bounds' to cover parts
				bounds.Encapsulate(element.gameObject.GetComponent<Renderer>().bounds);
			}

			// Essentially project parts onto floor (x and z coordinates) for table length and width
			// Table Height is determined by 'GlobalVariables'
			table.transform.localScale = new Vector3(bounds.size.x,tableHeight,bounds.size.z);

			// Place table under parts using the generated center
			table.transform.localPosition = new Vector3(center.x, -tableHeight/2f, center.z);

			// Foreach part in table, assign Unity
			// components to make them behave
			foreach (Transform part in table.transform) 
			{
				part.gameObject.AddComponent<Rigidbody>();
				part.gameObject.GetComponent<Rigidbody>().mass = 1000;
				part.gameObject.GetComponent<Rigidbody>().centerOfMass = Vector3.zero;
				part.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
				part.gameObject.GetComponent<Rigidbody>().isKinematic = true;
				part.gameObject.AddComponent<BoxCollider>();
				part.gameObject.GetComponent<BoxCollider>().enabled = true;
			}

			// Assign Gravity to each part of assembly
			if (gravityMode) 
			{ 
				// Set 'gravityMode' to false in 'GlobalVariables' to disable
				foreach (Transform element in parts_pile.transform) 
				{
					element.gameObject.GetComponent<Rigidbody>().useGravity = true;
					element.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(UnityEngine.Random.Range(-10F, 10F), UnityEngine.Random.Range(-10F, 10F), UnityEngine.Random.Range(-10F, 10F));
					element.gameObject.GetComponent<Rigidbody>().drag = 50;
				}
			}
			// If 'gravityMode' is false, enter 'SeparationMode'
			// This will allow parts to be suspended in space, slightly separated
			// This is the default because it makes build
			else 
			{ 
				foreach (Transform element in parts_pile.transform) 
				{
					element.gameObject.GetComponent<Rigidbody>().useGravity = false;
					element.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
					element.gameObject.GetComponent<Rigidbody>().freezeRotation = true;
					if (!separation) 
					{
						element.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
						// Allow it to use physics
						element.gameObject.GetComponent<Rigidbody>().isKinematic = true;
					}
					element.gameObject.GetComponent<Rigidbody>().drag = 30;
				}
			}

			// Basically repeat the previous steps but for the final location, or 'ghost'
			ghost = Instantiate(temp, null, true);		
			// Same location as parts plus the 'GhostOffset'
			// Configurable in 'GlobalVariables'
			ghost.gameObject.transform.position = (OriginalLocation + PartsOffset + GhostOffset);
			ghost.name = temp.name + "_ghost";
			foreach (Transform element in ghost.transform) 
			{
				element.name = element.name + " ghost";
				element.gameObject.tag = "Ghost";
				element.gameObject.AddComponent<Rigidbody>();
				if (element.gameObject.GetComponent<MeshCollider>() == null) 
				{
					element.gameObject.AddComponent<MeshCollider>();
				}
				element.gameObject.GetComponent<Rigidbody>().useGravity = false;
				element.gameObject.GetComponent<Rigidbody>().isKinematic = true;
				element.gameObject.GetComponent<Renderer>().material = Resources.Load("Ghost") as Material;
				Destroy(element.gameObject.GetComponent<MeshCollider>());
				element.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
			}
			Destroy(DeployPrefab.gameObject);
		}
		catch (MissingComponentException e) {
			Debug.LogError("The Deploy script is attached to invalid model: . Please use an appropriate model and try again. The error is as follows:\n" + e.Message);
			Application.Quit();
		}
	}
}
