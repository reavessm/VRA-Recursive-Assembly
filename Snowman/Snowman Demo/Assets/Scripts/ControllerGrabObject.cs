/*
 * Copyright (c) 2016 Razeware LLC
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using UnityEngine;
using UnityEngine.SceneManagement;
﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

public class ControllerGrabObject : MonoBehaviour
{
    public Material ghostMaterial;
  	public float snapDistance;
    public Canvas GUICanvas;
    private SteamVR_TrackedObject trackedObj;

    private GameObject collidingObject;
    private GameObject objectInHand;
    //private GameObject[] ghostObject;
    //private GameObject[] gameObjectArray; // do we need two GameObject[]? -SR //was only using one, to highlight multiple of something (eyes) as well as a temporary array to store all children of ghost prefab, then sorting through
    private SortedDictionary<string, GameObject> gameObjectDictionary;
    private SortedDictionary<string, GameObject> ghostObjectDictionary;
    private SortedDictionary<string, Color> defaultMaterialDictionary;
    private SortedDictionary<string, Color> currentMaterialDictionary;
    private Rigidbody objectRigidbody;
  	private Color ghostColor = new Color32(0x00, 0xF2, 0xAC, 0x5D);
  	private Color ghostColorHi = new Color32(0x00, 0xF2, 0xAC, 0xA0);
    private Color nextToPickUp = new Color32(255, 0, 0, 0);
    private Color setInPlace = new Color32(0, 255, 0, 0);
    private bool uiIsUp = false; // This changes whenever the UI is pulled up
    private float guiDistance; // how far to place the gui infront of the player
    public bool moveThrough;
    private Text textbox;
    public String defaultObjInfo = "Pick up an object to see it's info here.";
    private bool autoassemble = false;
    private KeyValuePair<string, GameObject>[] autoassemble_model;
    private KeyValuePair<string, GameObject>[] autoassemble_target;
    private int index_autoassemble;

    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    void Awake()
    {
        CustomInit();
    }

    private void CustomInit() {
        autoassemble = false;
        index_autoassemble = 0;
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        gameObjectDictionary = new SortedDictionary<string, GameObject>();
        ghostObjectDictionary = new SortedDictionary<string, GameObject>();
        defaultMaterialDictionary = new SortedDictionary<string, Color>();
        currentMaterialDictionary = new SortedDictionary<string, Color>();
        foreach (Transform element in GUICanvas.transform) {
            if (element.gameObject.name == "Object Info") {
                textbox = element.GetComponent<Text>();
            }
        }
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Pickupable"))
        {
            gameObjectDictionary.Add(obj.name, obj.gameObject);
        }
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Ghost"))
        {
            ghostObjectDictionary.Add(obj.name, obj);
        }
        foreach (KeyValuePair<string,GameObject> obj in gameObjectDictionary)
        {
            defaultMaterialDictionary.Add(obj.Value.name, obj.Value.GetComponent<Renderer>().material.color);
        }
        currentMaterialDictionary = defaultMaterialDictionary;
        //gameObjectArray = GameObject.FindGameObjectsWithTag("Pickupable");		//kind of broken right now -IF                              // Moved to 'Awake' -SR
        //ghostObject = GameObject.FindGameObjectsWithTag("Ghost");           //since ghostObject is an array, search all possible -IF    // Moved to 'Awake()' -SR
        GUICanvas.gameObject.SetActive(false); // Hides UI initially
        guiDistance = 0.2f; // can change this if needed
        ColorNext();
        Debug.Log("Scene Ready!");
    }

    public void ColorNext()
    {
        foreach (KeyValuePair<string,GameObject> obj in gameObjectDictionary)
        {
            try {
                if (obj.Value.GetComponent<Metadata>().isNextInOrder())
                {
                    obj.Value.GetComponent<Renderer>().material.color = nextToPickUp;
                    obj.Value.GetComponentInChildren<Renderer>().material.color = nextToPickUp;
                }
            }
            catch (NullReferenceException e) {
                obj.Value.GetComponent<Renderer>().material.color = nextToPickUp;
                obj.Value.GetComponentInChildren<Renderer>().material.color = nextToPickUp;
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        SetCollidingObject(other);
    }

    public void OnTriggerStay(Collider other)
    {
        SetCollidingObject(other);
    }

    public void OnTriggerExit(Collider other)
    {
        if (!collidingObject)
        {
            return;
        }

        collidingObject = null;
    }

    private void SetCollidingObject(Collider col)
    {
        if (collidingObject || !col.GetComponent<Rigidbody>())
        {
            return;
        }

        collidingObject = col.gameObject;
    }

    void Update()
    {
        if (Controller.GetHairTriggerDown())
        {
            try {
                if (collidingObject.tag == "Restart")
                {
                    // Should fix reset bug
                    setEverythingUnbuilt();

                    autoassemble = false;
                    index_autoassemble = 0;
                    SceneManager.LoadScene("Snowman", LoadSceneMode.Single);
                }
                else if (collidingObject.tag == "AutoAssemble")
                {
                    Debug.Log("AutoAssemble Trigger: " + index_autoassemble + " " + autoassemble);
                    autoassemble_model = new KeyValuePair<string, GameObject>[gameObjectDictionary.Count];
                    autoassemble_target = new KeyValuePair<string, GameObject>[ghostObjectDictionary.Count];
                    int count= 0;
                    foreach (KeyValuePair<string, GameObject> element in gameObjectDictionary) {
                        Debug.Log(element);
                        autoassemble_model[count++] = element;
                    }
                    count = 0;
                    foreach (KeyValuePair<string, GameObject> element in ghostObjectDictionary) {
                        autoassemble_target[count++] = element;
                    }
                    autoassemble = true;
                }
                else if (collidingObject.tag == "Pickupable")
                {
                    GrabObject();
                }
            }
            catch (NullReferenceException e) {
                Debug.Log("Object In Hand Not Configured -- Pickup");
            }
        }

		if (Controller.GetHairTriggerUp())
		{
			if (objectInHand)
			{
				ReleaseObject();
			}
		}

        if (autoassemble) {
            slurpToGhost(index_autoassemble);
        }

    }


    // Check to make sure we aren't "skipping backwards" in the build order.
    // Ex if 1, 2, 3, 4 have been built, make sure you can't remove 1, 2, 3.
    private void GrabObject()
    {
        objectInHand = collidingObject;
        objectRigidbody = objectInHand.GetComponent<Rigidbody>();
        objectInHand.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        collidingObject = null;
        var joint = AddFixedJoint();
        objectRigidbody.isKinematic = false; // was false
        //objectRigidbody.constraints = RigidbodyConstraints.None;
        joint.connectedBody = objectInHand.GetComponent<Rigidbody>();
        objectInHand.GetComponent<Rigidbody>().useGravity = false;
        if (objectInHand.GetComponent<Metadata>().isNextInOrder() && !objectInHand.GetComponent<Metadata>().getBuilt()) {
            highlightGhost(objectInHand);
            objectInHand.GetComponent<Metadata>().setBuilt(true);
            //ColorNext();
        }
        if (objectInHand.GetComponent<Metadata>().getBuilt()) {
            unHighlightGhost(objectInHand);
        }
        textbox.text = objectInHand.GetComponent<Metadata>().PrettyPrint();

    }

    private void highlightGhost(GameObject heldObject)
    {

		/*int j = 0;
		for (int i = 0; i < gameObjectArray.Length; i++)
		{
			if (gameObjectArray[i].name.Equals(heldObject.name))
			{
				ghostObject[j] = gameObjectArray[i];
				j++;
			}
		}
		for (int i = 0; i < ghostObject.Length; i++)
		{
			ghostObject[i].GetComponent<Renderer>().material.color = ghostColorHi;
			ghostObject[i].GetComponentInChildren<Renderer>().material.color = ghostColorHi;
		}*/

        foreach (KeyValuePair<string,GameObject> ghost in ghostObjectDictionary)
        {
            if (ghost.Key == heldObject.name) {
                ghost.Value.GetComponent<Renderer>().material.color = ghostColorHi;
                ghost.Value.GetComponentInChildren<Renderer>().material.color = ghostColorHi;
            }
        }
    }

    private void unHighlightGhost(GameObject heldObject)
    {
        //GameObject[] arr = GameObject.FindGameObjectsWithTag("Ghost"); // use gameObjectArray from 'Awake()' -SR
        /*int j = 0;
		for (int i = 0; i < gameObjectArray.Length; i++)
		{
			if (gameObjectArray[i].name.Equals(heldObject.name))
			{
				ghostObject[j] = gameObjectArray[i];
				j++;
			}
		}
		for (int i = 0; i < ghostObject.Length; i++)
		{
			ghostObject[i].GetComponent<Renderer>().material.color = ghostColor;
			ghostObject[i].GetComponentInChildren<Renderer>().material.color = ghostColor;
		}*/

	}
	//will find an object to snap to, uses snap distance to find distance
	private void snapToGhost(GameObject snappingObject, GameObject locationObject)
	{
            snappingObject.GetComponent<Metadata>().setBuilt(true);
            if (moveThrough)
            {
                snappingObject.GetComponent<MeshCollider>().convex = false;
                Destroy(snappingObject.GetComponent<Rigidbody>());

            }
            snappingObject.GetComponent<MeshCollider>().convex = false;
            snappingObject.transform.SetPositionAndRotation(locationObject.transform.position,
                locationObject.transform.rotation);
            unHighlightGhost(snappingObject);
            snappingObject.GetComponent<Renderer>().material.color = setInPlace;
            snappingObject.GetComponentInChildren<Renderer>().material.color = setInPlace;
            Debug.Log(snappingObject.transform.position);
            ColorNext();
            Destroy(snappingObject.GetComponent<Rigidbody>());
            snappingObject.GetComponent<MeshCollider>().convex = false;
	}

    private void slurpToGhost(int index) {
            Debug.Log("SlurpToGhost Trigger: " + index + " " + autoassemble + " " + autoassemble_model.Length);
            GameObject snappingObject = autoassemble_model[index].Value;
            GameObject locationObject = autoassemble_target[index].Value;
            Debug.Log(snappingObject.name);
            Debug.Log(locationObject.name);
            snappingObject.GetComponent<Metadata>().setBuilt(true);
            if (moveThrough)
            {
                snappingObject.GetComponent<MeshCollider>().convex = false;
            }
            snappingObject.GetComponent<MeshCollider>().convex = false;
            snappingObject.transform.position = Vector3.MoveTowards(snappingObject.transform.position, locationObject.transform.position, 10 * Time.deltaTime);
            unHighlightGhost(snappingObject);
            snappingObject.GetComponent<Renderer>().material.color = setInPlace;
            snappingObject.GetComponentInChildren<Renderer>().material.color = setInPlace;
            Debug.Log(snappingObject.transform.position);
            ColorNext();
            snappingObject.GetComponent<MeshCollider>().convex = false;
            if (snappingObject.transform.position == locationObject.transform.position) {
                Debug.Log(index_autoassemble);
                Debug.Log(gameObjectDictionary.Count);
                Debug.Log(ghostObjectDictionary.Count);
                if (index_autoassemble < (gameObjectDictionary.Count - 1)) {
                    index_autoassemble++;
                }
                else {
                    Debug.Log("Done?");
                    autoassemble = false;
                }
            }
    }

    private void unsnapToGhost(GameObject snappingObject, GameObject locationObject)
    {
        snappingObject.GetComponent<Metadata>().setBuilt(false);
        //snappingObject.transform.position = locationObject.transform.position;
        unHighlightGhost(snappingObject);
        snappingObject.GetComponent<Renderer>().material.color = nextToPickUp;
        snappingObject.GetComponentInChildren<Renderer>().material.color = nextToPickUp;
        ColorNext();
    }

    private FixedJoint AddFixedJoint()
    {
        FixedJoint fx = gameObject.AddComponent<FixedJoint>();
        fx.breakForce = 2000000;
        fx.breakTorque = 2000000;
        return fx;
    }

    private void setAllBuilt(SortedDictionary<string, GameObject> dict, bool boolean) {
        try {
            foreach (KeyValuePair<string, GameObject> obj in dict) { 
                obj.Value.GetComponent<Metadata>().setBuilt(boolean);
            }
        }
        catch (NullReferenceException e) {
        }
    }

    private void setEverythingBuilt() {
      setAllBuilt(gameObjectDictionary, true);
    }

    private void setEverythingUnbuilt() {
      setAllBuilt(gameObjectDictionary, false);
    }

    private void ReleaseObject()
    {
        if (GetComponent<FixedJoint>())
        {
            try {
                GetComponent<FixedJoint>().connectedBody = null;
                Destroy(GetComponent<FixedJoint>());

                objectRigidbody = objectInHand.GetComponent<Rigidbody>();
                objectRigidbody.isKinematic = true;
                //objectRigidbody.constraints = RigidbodyConstraints.FreezeAll;
                //objectRigidbody.velocity = Controller.velocity;
                //objectRigidbody.angularVelocity = Controller.angularVelocity;
                //objectInHand.GetComponent<Rigidbody>().useGravity = true;
                //unHighlightGhost(objectInHand);
                string objectInHandName = objectInHand.name + " ghost";
                Debug.Log(objectInHandName);
                GameObject ghostObject = ghostObjectDictionary[objectInHandName];
                float realDistance = Vector3.Distance(objectInHand.transform.position, ghostObject.transform.position);
                Debug.Log(realDistance);
                objectInHand.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                if (realDistance < snapDistance)
                {
                    snapToGhost(objectInHand, ghostObject);
                } else
                {
                    unsnapToGhost(objectInHand, ghostObject);
                }
            }
            catch (KeyNotFoundException e) {
                Debug.Log("Object In Hand Not Configured -- Release");
            }
        }

		/*for (int i = 0; i < ghostObjectDictionary.Length; i++)
		{
			float realDistance = Vector3.Distance(objectInHand.transform.position, ghostObject[i].transform.position);
			if (realDistance < snapDistance)
			{
				snapToGhost(objectInHand, ghostObject[i]);
			}
		}*/

	objectInHand = null;
    textbox.text = defaultObjInfo;

    }
}
