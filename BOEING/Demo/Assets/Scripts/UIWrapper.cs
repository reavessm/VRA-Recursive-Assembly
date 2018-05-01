using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class UIWrapper : MonoBehaviour {

	public Canvas GUICanvas;
	// This changes whenever the UI is pulled up
	private bool uiIsUp;
	// how far to place the gui infront of the player
	private float guiDistance;
	private SteamVR_TrackedObject trackedObj;
    private GameObject laser;
	public GameObject laserPrefab;
    private RaycastHit hit;
    private SortedDictionary<string,Button> buttonList;
	public int range;
	public LayerMask UIMask;

	// for keeping track of individual hands
	int handIndex;
	int guiIndex;

    // The transform component of the laser for ease of use
	private Transform laserTransform;
	// Point where the raycast hits
	private Vector3 hitPoint;

	private SteamVR_Controller.Device Controller
	{
		get { return SteamVR_Controller.Input((int)trackedObj.index); }
	}

    // Ran when the script starts
	void Awake () {
		trackedObj = GetComponent<SteamVR_TrackedObject>();
        GUICanvas.gameObject.SetActive(false); // Hides UI initially
		guiDistance = 2f; // can change this if needed
		uiIsUp = false; // This changes whenever the UI is pulled up
        handIndex = (int)trackedObj.index; // to keep track of individual hands
        guiIndex = 0;
	}

    // Ran after Awake()
	private void Start()
	{
		laser = Instantiate(laserPrefab);
		laserTransform = laser.transform;
	}

	// Update is called once per frame
	void Update () {
		// If the grip buttons are pressed down and the UI is not now, display
		if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
		{
			if (!uiIsUp)
			{
				ShowUI();
				RaycastHit hit;

				// Send out a raycast from the controller
				if (Physics.Raycast(trackedObj.transform.position, transform.forward, out hit, 100, UIMask) && hit.collider != null)
				{
					hitPoint = hit.point;
					ShowLaser(hit);
				}
				if (laser == null)
                {
                    Debug.Log("laser does not exists");
                }
                if (trackedObj == null)
                {
                    Debug.Log("tracked obj does not exists");
                }
                if (laserPrefab == null)
                {
                    Debug.Log("laserPrefab does not exists");
                }
                //TurnOnLaser(true);
                if (Controller.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
                {
                    Debug.Log("TouchPad is Pressed");
                    if (Physics.Raycast(transform.position, transform.forward, out hit, 2.0f))
                    {
                        Debug.Log("RaycastHit");
                        if(hit.transform.tag == "FloatingButton")
                        {
                            Debug.Log("Tag is Floating BUtton");
                            if (hit.transform.name == "reset")
                            {
                                Debug.Log("Calling REsetScene");
                                ResetScene();
                            }
                        }
                    }
                }
            }
			
		}
        // When the grip buttons are released, hid the UI
		if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Grip))
		{
			HideUI();
			laser.SetActive(false);
		}
		uiIsUp = GUICanvas.gameObject.activeSelf;
    }

    // Was an attempt to make a raycast selected menu
	// Deprecated
	void RaycastWorldUI() {
		Debug.Log("Enter Raycast World UI");
		PointerEventData pointerData = new PointerEventData(EventSystem.current);

		pointerData.position = trackedObj.transform.position;

		List<RaycastResult> results = new List<RaycastResult>();

		EventSystem.current.RaycastAll(pointerData, results);

		Debug.Log(results.ToString());

		if (results.Count > 0) {
			if (results[0].gameObject.layer == LayerMask.NameToLayer("UI")) {
				string dbg = "";
				Debug.Log(string.Format(dbg, results[results.Count - 1].gameObject.name, results[0].gameObject.name));
				results.Clear();
			}
		}
	}

    // Displays the UI boxes
	private void ShowUI()
	{
		// Sets the UI to be up and transforms the objects so that it is oriented towards the user
		uiIsUp = true;
		GUICanvas.gameObject.transform.position = Camera.main.transform.position + Camera.main.transform.forward * guiDistance; // Transform to be in front of player, i hope...
		GUICanvas.gameObject.transform.position = new Vector3(GUICanvas.gameObject.transform.position.x, 1.5f, GUICanvas.gameObject.transform.position.z);
		GUICanvas.gameObject.transform.rotation = Camera.main.transform.rotation;
		GUICanvas.gameObject.transform.Rotate(new Vector3(-GUICanvas.gameObject.transform.rotation.x, 0, -GUICanvas.gameObject.transform.rotation.z));

		guiIndex = handIndex;
        Controller.TriggerHapticPulse(1000); // buzz buzz
	}

    // Hides the UI interface
	private void HideUI()
	{
		// If the hand that released the grip button is the one that pressed it, hide it
    	if (guiIndex == handIndex) {
			GUICanvas.gameObject.SetActive(false);
			uiIsUp = false;
      		guiIndex = 0;
      		Controller.TriggerHapticPulse(500); // buzz
    	}
	}

	// Resets the scene
    private void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

	// Shows the laser and moves it based on where its pointed
	private void ShowLaser(RaycastHit hit)
	{
		laser.SetActive(true); //Show the laser
		laserTransform.position = Vector3.Lerp(trackedObj.transform.position, hitPoint, .5f); // Move laser to the middle between the controller and the position the raycast hit
		laserTransform.LookAt(hitPoint); // Rotate laser facing the hit point
		laserTransform.localScale = new Vector3(laserTransform.localScale.x, laserTransform.localScale.y,
			hit.distance); // Scale laser so it fits exactly between the controller & the hit point
	}
}


