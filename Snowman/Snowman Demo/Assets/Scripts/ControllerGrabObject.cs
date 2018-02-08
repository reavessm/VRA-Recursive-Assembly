﻿/*
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

public class ControllerGrabObject : MonoBehaviour
{
    public Material ghostMaterial;
	public float snapDistance;

    private SteamVR_TrackedObject trackedObj;

    private GameObject collidingObject;
    private GameObject objectInHand;
    private GameObject ghostObject;
    private Rigidbody objectRigidbody;
	private Color ghostColor = new Color32(0x00, 0xF2, 0xAC, 0x5D);
	private Color ghostColorHi = new Color32(0x00, 0xF2, 0xAC, 0xA0);



	private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
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
            if (collidingObject.tag == "Restart")
			{
                SceneManager.LoadScene("Snowman");
            }

            if (collidingObject.tag == "Pickupable")
            {
                GrabObject();
            }
        }

        if (Controller.GetHairTriggerUp())
        {
            if (objectInHand)
            {
                ReleaseObject();
            }
        }
    }

    private void GrabObject()
    {
        objectInHand = collidingObject;
        objectRigidbody = objectInHand.GetComponent<Rigidbody>();
        collidingObject = null;
        var joint = AddFixedJoint();
        objectRigidbody.isKinematic = false;
        //objectRigidbody.constraints = RigidbodyConstraints.None;            
        joint.connectedBody = objectInHand.GetComponent<Rigidbody>();
        objectInHand.GetComponent<Rigidbody>().useGravity = false;
        highlightGhost(objectInHand);
    }

    private void highlightGhost(GameObject heldObject)
    {
        string nameOfGhost = heldObject.name + "Ghost";
        ghostObject = GameObject.Find(nameOfGhost);
		ghostObject.GetComponent<Renderer>().material.color = ghostColorHi;
    }

    private void unHighlightGhost(GameObject heldObject)
    {
        string nameOfGhost = heldObject.name + "Ghost";
        ghostObject = GameObject.Find(nameOfGhost);
        ghostObject.GetComponent<Renderer>().material.color = ghostColor;
    }

	private void snapToGhost(GameObject snappingObject)								//will find an object to snap to, uses snap distance to find distance
	{
		string nameOfGhost = snappingObject.name + "Ghost";
		ghostObject = GameObject.Find(nameOfGhost);
		float realDistance = Vector3.Distance(snappingObject.transform.position, ghostObject.transform.position);
		if (realDistance < snapDistance)
		{
			//snappingObject.transform.position.Set = ghostObject.transform.position;				//TODO
		}

	}

    private FixedJoint AddFixedJoint()
    {
        FixedJoint fx = gameObject.AddComponent<FixedJoint>();
        fx.breakForce = 20000;
        fx.breakTorque = 20000;
        return fx;
    }

    private void ReleaseObject()
    {
        if (GetComponent<FixedJoint>())
        {
            GetComponent<FixedJoint>().connectedBody = null;
            Destroy(GetComponent<FixedJoint>());

            objectRigidbody = objectInHand.GetComponent<Rigidbody>();
            objectRigidbody.isKinematic = true;
            //objectRigidbody.constraints = RigidbodyConstraints.FreezeAll;
            //objectRigidbody.velocity = Controller.velocity;
            //objectRigidbody.angularVelocity = Controller.angularVelocity;
            //objectInHand.GetComponent<Rigidbody>().useGravity = true;
            unHighlightGhost(objectInHand);
			snapToGhost(objectInHand);
        }

        objectInHand = null;
    }
}
