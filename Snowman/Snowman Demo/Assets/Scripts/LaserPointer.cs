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

 //this script is exactly the same as teleport except without anything involving teleporting
 //at some point i'll make it so that that script inherits this one
 //the ruler script will also inherit from here when i get around to writing it
 //not attached to anything until i do the ui stuff - ian

using UnityEngine;

public class LaserPointer : MonoBehaviour
{
	public Transform cameraRigTransform;
	public Transform headTransform; // The camera rig's head
	public int range;
	private SteamVR_TrackedObject trackedObj;
	public GameObject laserPrefab; // The laser prefab
	private GameObject laser; // A reference to the spawned laser
	private Transform laserTransform; // The transform component of the laser for ease of use

	private Vector3 hitPoint; // Point where the raycast hits


	private SteamVR_Controller.Device Controller
	{
		get { return SteamVR_Controller.Input((int)trackedObj.index); }
	}

	void Awake()
	{
		trackedObj = GetComponent<SteamVR_TrackedObject>();
	}

	//new
	void Start()
	{
		laser = Instantiate(laserPrefab);
		laserTransform = laser.transform;
	}

	void Update()
	{
		// Is the trigger held down?
		if (Controller.GetHairTriggerDown())
		{
			RaycastHit hit;

			// Send out a raycast from the controller
			if (Physics.Raycast(trackedObj.transform.position, transform.forward, out hit, range) && hit.collider != null)
			{
				hitPoint = hit.point;
				ShowLaser(hit);

				//Show teleport reticle
				if ((hitPoint.x < trackedObj.transform.position.x + range) && (hitPoint.x > trackedObj.transform.position.x - range) && (hitPoint.z < trackedObj.transform.position.z + range) && (hitPoint.z > trackedObj.transform.position.z - range)) { }
				else
				{
					laser.SetActive(false);		//really janky way of hiding the laser if you drag it over the horizon
				}
			}
		}
		else// Touchpad not held down, hide laser & teleport reticle
		{
			laser.SetActive(false);
		}
	}

	private void ShowLaser(RaycastHit hit)
	{
		laser.SetActive(true); //Show the laser
		laserTransform.position = Vector3.Lerp(trackedObj.transform.position, hitPoint, .5f); // Move laser to the middle between the controller and the position the raycast hit
		laserTransform.LookAt(hitPoint); // Rotate laser facing the hit point
		laserTransform.localScale = new Vector3(laserTransform.localScale.x, laserTransform.localScale.y, hit.distance); // Scale laser so it fits exactly between the controller & the hit point
	}
}
