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
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

public class ControllerGrabObject : MonoBehaviour
{
    private GlobalVariables variables;
    private Material ghostMaterial;
    private float snapDistance;
    private Canvas GUICanvas;
    private SceneSetter sceneDirector;
    private SteamVR_TrackedObject trackedObj;

    private GameObject collidingObject;
    private GameObject objectInHand;
    private Rigidbody objectRigidbody;
    private bool uiIsUp = false; // This changes whenever the UI is pulled up
    private float guiDistance; // how far to place the gui infront of the player

    private Text textbox;
    public String defaultObjInfo = "Pick up an object to see it's info here.";

    // Get Vive Controllers
    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    // Runs before 'Start'
    void Awake()
    {
	// Get Global Variables
        variables = GameObject.Find("GlobalVariables").GetComponent<GlobalVariables>();
        ghostMaterial = variables.GetGhostMaterial();
        snapDistance = variables.GetSnappingDistance();
        sceneDirector = variables.GetSceneSetter();
        defaultObjInfo = variables.GetDefaultInfo();
        GUICanvas = variables.GetGUICanvas();
        
	// Ensure we have a Scene Setter
	if (sceneDirector == null)
        {
            sceneDirector = new SceneSetter();
        }

	// Prepare to do some work
        sceneDirector.CustomInit();
        CustomInit();
    }

    private void CustomInit()
    {
	// Miscellaneous initializations
        DontDestroyOnLoad(GUICanvas); // Keep Canvas on Reload
        trackedObj = GetComponent<SteamVR_TrackedObject>(); // Reference Controllers
        textbox = GUICanvas.transform.Find("Tool Space").transform.Find("Object Info").GetComponent<Text>(); // Find the textbox to display part info
        GUICanvas.gameObject.SetActive(true); // Make sure part info canvas is visible
        //guiDistance = 0.2f; // can change this if needed // not needed???

        Debug.Log("Scene Ready!"); // what's one more debug log?
    }


    // These next scripts are how Unity and VRTK handle registering when the controller is moved into an object, and when the trigger is pressed
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

    // Runs once per frame
    void Update()
    {
	// If you press the trigger
        if (Controller.GetHairTriggerDown())
        {
            try
            {
		// Check if that object is allowed to be picked up
                if (collidingObject.tag == "Pickupable")
                {
                    GrabObject();
                }
            }
	    // What to do if you don't have anything close enough to be picked up
            catch (NullReferenceException e)
            {
            }
        }

	// If you release trigger
        if (Controller.GetHairTriggerUp())
        {
	    // If you have something in your hand
            if (objectInHand)
            {
                ReleaseObject();
            }
        }
	// Snap/slerp the object in your hand to where it needs to go
        sceneDirector.SlurpToGhost();
    }

    // Check to make sure we aren't "skipping backwards" in the build order.
    // Ex if 1, 2, 3, 4 have been built, make sure you can't remove 1, 2, 3.
    private void GrabObject()
    {
	// Unity and VRTK methods for attaching an object to the controller
        objectInHand = collidingObject;
        objectRigidbody = objectInHand.GetComponent<Rigidbody>();
        objectInHand.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        collidingObject = null;
        var joint = AddFixedJoint();
        objectRigidbody.isKinematic = false; // Object in hand doesn't obey physics
        joint.connectedBody = objectInHand.GetComponent<Rigidbody>();
        objectInHand.GetComponent<Rigidbody>().useGravity = false;

	// Check to see if the object is next in order and not built
        if (objectInHand.GetComponent<Metadata>().isNextInOrder() && !objectInHand.GetComponent<Metadata>().getBuilt())
        {
            sceneDirector.HighlightGhost(objectInHand);
            //objectInHand.GetComponent<Metadata>().setBuilt(true);
        }

	// Delete?
        if (objectInHand.GetComponent<Metadata>().getBuilt())
        {
            //sceneDirector.UnHighlightGhost(objectInHand);
        }

	// Change text in parts info canvas to show metadata
        textbox.text = objectInHand.GetComponent<Metadata>().PrettyPrint();
    }

    // Unity and VRTK methods to specify how hard to attach part to controller
    // We chose infinity but feel free to change it if you so desire
    private FixedJoint AddFixedJoint()
    {
        FixedJoint fx = gameObject.AddComponent<FixedJoint>();
        fx.breakForce = Mathf.Infinity;
        fx.breakTorque = Mathf.Infinity;
        return fx;
    }

    // Stop Objects from flying away if they break out of your hand
    void OnJointBreak(float breakForce)
    {
	// Broken mode allows them to fly away for funsies
	// Configurable in 'GlobalVariables'
        if (!sceneDirector.brokenMode)
        {
            objectInHand.GetComponent<Rigidbody>().velocity = Vector3.zero;
            sceneDirector.ColorNext();
            objectRigidbody.isKinematic = true;
	    
	       // Change text back to default if you are not holding an object
	       if (textbox != null) {
		       textbox.text = defaultObjInfo;
	       }
        }
    }

    // Specify what happens when you let go of an object
    private void ReleaseObject()
    {
	// Only has FixedJoint when the part is attached to controller
        if (GetComponent<FixedJoint>())
        {
            try
            {
		        // Delete FixedJoint
		        // Doubles as a way to make sure you can't release a released object
                GetComponent<FixedJoint>().connectedBody = null;
		        // Probably redundant, but we wan tot make sure  its released
                Destroy(GetComponent<FixedJoint>());

                objectRigidbody = objectInHand.GetComponent<Rigidbody>();
                objectRigidbody.isKinematic = true; // Give it back it's physics
        		// FindGhost returns the 'ghost' object corresponding to the object in hand
		        // The 'ghost' is the final destination
                GameObject ghostObject = sceneDirector.FindGhost(objectInHand);
		        // Find distance between part and destination
                float realDistance = Vector3.Distance(objectInHand.transform.position, ghostObject.transform.position);
                objectInHand.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                
		// If object is close enough to snap
		if (realDistance < snapDistance)
                {
		            // Snap it to place
                    sceneDirector.SnapToGhost(objectInHand, ghostObject);
                }
                else
                {
                    sceneDirector.UnsnapToGhost(objectInHand, ghostObject);
                }
                //Debug.Log("Ending Release");
		        // Color the next item in order to be red
                sceneDirector.ColorNext();
            }
            catch (KeyNotFoundException e)
            {
            }
        }

	    // This is where we actually release the object
        objectInHand = null;

	    // Revert the textbox to default text after releasing
	    if (textbox != null) {
	        textbox.text = defaultObjInfo;
	    }
    }
}
