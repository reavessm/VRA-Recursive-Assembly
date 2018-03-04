using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWrapper : MonoBehaviour {

	public Canvas GUICanvas;
	private bool uiIsUp; // This changes whenever the UI is pulled up
	private float guiDistance; // how far to place the gui infront of the player
	private SteamVR_TrackedObject trackedObj;
	private LaserPointer laser = new LaserPointer();
	public GameObject laserPrefab;

  // for keeping track of individual hands
  int handIndex;
  int guiIndex;
  //SteamVR_Controller.Device rightHandController;
  //SteamVR_Controller.Device leftHandController;




	private SteamVR_Controller.Device Controller
	{
		get { return SteamVR_Controller.Input((int)trackedObj.index); }
	}

	void Awake () {
		trackedObj = GetComponent<SteamVR_TrackedObject>();
		GUICanvas.gameObject.SetActive(false); // Hides UI initially
		guiDistance = 2f; // can change this if needed
		uiIsUp = false; // This changes whenever the UI is pulled up
		laser.laserMask = 5;

    // to keep track of individual hands
    handIndex = (int)trackedObj.index;
    guiIndex = 0;
	}

// Update is called once per frame
	void Update () {
		if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
		{
			//Debug.Log(gameObject.name + " Grip Press");
			if (!uiIsUp)
			{
				ShowUI();
				laser.TurnOn(true, trackedObj, laserPrefab);
			}
		}



		if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Grip))
		{
			//Debug.Log(gameObject.name + " Grip Release");
			HideUI();
			//laser.hitPoint
			laser.TurnOn(false, trackedObj, laserPrefab);
		}

		uiIsUp = GUICanvas.gameObject.activeSelf;
	}

	private void ShowUI()
	{
		//Debug.Log("Showing UI stuff");
		GUICanvas.gameObject.SetActive(true);
		uiIsUp = true;
		GUICanvas.gameObject.transform.position = Camera.main.transform.position + Camera.main.transform.forward * guiDistance; // Transform to be in front of player, i hope...
		GUICanvas.gameObject.transform.rotation = Camera.main.transform.rotation;
    guiIndex = handIndex;
    Controller.TriggerHapticPulse(1000); // buzz buzz
	}

	private void HideUI()
	{
    if (guiIndex == handIndex) {
		  //Debug.Log("Hiding UI stuff");
		  GUICanvas.gameObject.SetActive(false);
		  uiIsUp = false;
      guiIndex = 0;
      Controller.TriggerHapticPulse(500); // buzz
    }
	}
}
