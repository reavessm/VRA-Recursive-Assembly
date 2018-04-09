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

public class LaserPointer : ScriptableObject
{
	public int range;
	private SteamVR_TrackedObject trackedObj;
	public GameObject laserPrefab; // The laser prefab
	private GameObject laser; // A reference to the spawned laser
	private Transform laserTransform; // The transform component of the laser for ease of use
	public Vector3 hitPoint; // Point where the raycast hits
	public LayerMask laserMask;


	private SteamVR_Controller.Device Controller
	{
		get { return SteamVR_Controller.Input((int)trackedObj.index); }
	}

	//new
	void Start()
	{
        laser = Instantiate(laserPrefab);
        laserTransform = laser.transform;
	}


	public void TurnOn(bool active, SteamVR_TrackedObject obj, GameObject laserPrefab)
	{
       
        if (active)
		{
			RaycastHit hit;

			// Send out a raycast from the controller
			if (Physics.Raycast(obj.transform.position, obj.transform.forward, out hit, range) && hit.collider != null)
			{
				hitPoint = hit.point;
				ShowLaser(hit, obj, laserPrefab);

				//Show teleport reticle
				if ((hitPoint.x < obj.transform.position.x + range) && (hitPoint.x > obj.transform.position.x - range) && (hitPoint.z < obj.transform.position.z + range) && (hitPoint.z > obj.transform.position.z - range))
				{
					laser.SetActive(true);
				}
				else
				{
					laser.SetActive(false);     //really janky way of hiding the laser if you drag it over the horizon
				}
			}
		}
		else
		{
			laser.SetActive(false);
		}
	}

	private void ShowLaser(RaycastHit hit, SteamVR_TrackedObject obj, GameObject laserPrefab)
	{
       
        laser = laserPrefab;
        if (laser == null)
       
        laserTransform = laser.transform;
		laser.SetActive(true); //Show the laser
		laserTransform.position = Vector3.Lerp(obj.transform.position, hitPoint, .5f); // Move laser to the middle between the controller and the position the raycast hit
		laserTransform.LookAt(hitPoint); // Rotate laser facing the hit point
		laserTransform.localScale = new Vector3(laserTransform.localScale.x, laserTransform.localScale.y, hit.distance); // Scale laser so it fits exactly between the controller & the hit point
	}
	public LaserPointer()
	{
		
	}

    public LaserPointer GetLaserPointer()
    {
        return this;
    }

    public GameObject GetLaser()
    {
        return this.laser;
    }
}
