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

 //this script will undergo major modification to inherit from the laser pointer script
 

using UnityEngine;

public class Teleport : MonoBehaviour
{
    private GlobalVariables variables;
    private GameObject laser; // A reference to the spawned laser
    private Transform laserTransform; // The transform component of the laser for ease of use
    private GameObject reticle; // A reference to an instance of the reticle
    private Transform teleportReticleTransform; // Stores a reference to the teleport reticle transform for ease of use
    private SteamVR_TrackedObject trackedObj;
    private Vector3 hitPoint; // Point where the raycast hits
    private bool shouldTeleport; // True if there's a valid teleport target

    public Transform cameraRigTransform;
    public Transform headTransform; // The camera rig's head
    public Vector3 teleportReticleOffset; // Offset from the floor for the reticle to avoid z-fighting
    public LayerMask teleportMask; // Mask to filter out areas where teleports are allowed
	public int range;
    public GameObject laserPrefab; // The laser prefab
    public GameObject teleportReticlePrefab; // Stores a reference to the teleport reticle prefab.

    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        variables = GameObject.Find("GlobalVariables").GetComponent<GlobalVariables>();
        teleportMask = variables.GetTeleportMask();
        range = variables.GetTeleportRange();
    }

    // Called after Awake()
    // Initializes necessary variable for teleport functionality
    void Start()
    {
        laser = Instantiate(laserPrefab);
        laserTransform = laser.transform;
        reticle = Instantiate(teleportReticlePrefab);
        teleportReticleTransform = reticle.transform;
    }

    // Called every frame
    // Capture collider for raycast and update values
    void Update()
    {
        // Is the touchpad held down?
        if (Controller.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
        {
            RaycastHit hit;

            // Send out a raycast from the controller
            if (Physics.Raycast(trackedObj.transform.position, transform.forward, out hit, 100, teleportMask) && hit.collider != null)
            {
                hitPoint = hit.point;

                //Show teleport reticle
                if ((hitPoint.x < trackedObj.transform.position.x + range) 
					&& (hitPoint.x > trackedObj.transform.position.x - range) 
					&& (hitPoint.z < trackedObj.transform.position.z + range) 
					&& (hitPoint.z > trackedObj.transform.position.z - range) 
                    && (hit.collider.tag.Equals("CanTeleport"))) 
                { 
                    reticle.SetActive(true);
                    teleportReticleTransform.position = hitPoint + teleportReticleOffset;
					shouldTeleport = true;
                } 
                else 
                {
					laser.SetActive(false);
                    reticle.SetActive(false);
                    shouldTeleport = false;
                }

            }
        }
        // Touchpad not held down, hide laser & teleport reticle
        else
        {
            reticle.SetActive(false);
        }

        // Touchpad released this frame & valid teleport position found
        if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad) && shouldTeleport)
        {
            DoTeleport();
        }
    }

    // Set necessary flags to reflect state of teleport
    private void DoTeleport()
    {
        // Teleport in progress, no need to do it again until the next touchpad release
        shouldTeleport = false;
        // Hide reticle
        reticle.SetActive(false); 
        // Calculate the difference between the center of the virtual room & the player's head
        Vector3 difference = cameraRigTransform.position - headTransform.position; 
        // Don't change the final position's y position, it should always be equal to that of the hit point
        difference.y = 0; 

        // Change the camera rig position to where the the teleport reticle was. 
        // Also add the difference so the new virtual room position is relative to the player position, allowing the player's new position to be exactly where they pointed. (see illustration)
        cameraRigTransform.position = hitPoint + difference; 
    }
}