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
    public SceneSetter sceneDirector;
    private SteamVR_TrackedObject trackedObj;

    private GameObject collidingObject;
    private GameObject objectInHand;
    private Rigidbody objectRigidbody;
    private bool uiIsUp = false; // This changes whenever the UI is pulled up
    private float guiDistance; // how far to place the gui infront of the player

    private Text textbox;
    public String defaultObjInfo = "Pick up an object to see it's info here.";


    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    void Awake()
    {
        if (sceneDirector == null)
        {
            sceneDirector = new SceneSetter();
        }
        sceneDirector.CustomInit();
        CustomInit();
    }

    private void CustomInit() {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        textbox = GUICanvas.transform.Find("Tool Space").transform.Find("Object Info").GetComponent<Text>();
      /*  foreach (Transform element in GUICanvas.transform) {
            Debug.Log("Next GUI Transform: " + element.name);
            if (element.gameObject.name == "Tool Space") {
                foreach
                textbox = element.gameObject.GetChild("Object Info").GetComponent<Text>();
                Debug.Log(textbox.text);
            }
        } */
        
       // textbox = GameObject.FindGameObjectWithTag("PartInfo").GetComponent<Text>();
        // GUICanvas.gameObject.SetActive(false); // Hides UI initially
        GUICanvas.gameObject.SetActive(true);
        guiDistance = 0.2f; // can change this if needed

        Debug.Log("Scene Ready!");
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
                    sceneDirector.Restart();
                }
                else if (collidingObject.tag == "AutoAssemble")
                {
                    sceneDirector.AutoAssemble();
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
        sceneDirector.SlurpToGhost();
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
        joint.connectedBody = objectInHand.GetComponent<Rigidbody>();
        objectInHand.GetComponent<Rigidbody>().useGravity = false;
        if (objectInHand.GetComponent<Metadata>().isNextInOrder() && !objectInHand.GetComponent<Metadata>().getBuilt()) {
            sceneDirector.HighlightGhost(objectInHand);
            //objectInHand.GetComponent<Metadata>().setBuilt(true);
        }
        if (objectInHand.GetComponent<Metadata>().getBuilt()) {
           //sceneDirector.UnHighlightGhost(objectInHand);
        }
        textbox.text = objectInHand.GetComponent<Metadata>().PrettyPrint();
        Debug.Log(objectInHand.GetComponent<Metadata>().PrettyPrint());
        Debug.Log("Is object built? " + objectInHand.GetComponent<Metadata>().getBuilt());

    }

    private FixedJoint AddFixedJoint()
    {
        FixedJoint fx = gameObject.AddComponent<FixedJoint>();
        fx.breakForce = 2000000;
        fx.breakTorque = 2000000;
        return fx;
    }

    void OnJointBreak(float breakForce)
    {
        if (!sceneDirector.brokenMode) {
            objectInHand.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            sceneDirector.ColorNext();
            objectRigidbody.isKinematic = true;
            objectInHand = null;
        }
        else {
            return;
        }
    }

    private void ReleaseObject()
    {
        if (GetComponent<FixedJoint>())
        {
            try {
                Debug.Log("Entering Release Try/Catch");
                GetComponent<FixedJoint>().connectedBody = null;
                Destroy(GetComponent<FixedJoint>());

                objectRigidbody = objectInHand.GetComponent<Rigidbody>();
                objectRigidbody.isKinematic = true;
                Debug.Log("Starting Ghost Key Retrieval");
                GameObject ghostObject = sceneDirector.FindGhost(objectInHand);
                Debug.Log("Got Past SceneSetter");
                float realDistance = Vector3.Distance(objectInHand.transform.position, ghostObject.transform.position);
                Debug.Log(realDistance);
                objectInHand.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                if (realDistance < snapDistance)
                {
                    sceneDirector.SnapToGhost(objectInHand, ghostObject);
                } else
                {
                    sceneDirector.UnsnapToGhost(objectInHand, ghostObject);
                }
                Debug.Log("Ending Release");
                sceneDirector.ColorNext();
            }
            catch (KeyNotFoundException e) {
                Debug.Log("Object In Hand Not Configured -- Release: " + e.Message);
            }
        }

	    objectInHand = null;

        if (textbox == null)
        {
            Debug.Log("textbox is null");
        } else
        {
            textbox.text = defaultObjInfo;
            Debug.Log(defaultObjInfo);
        }
       

    }
}
