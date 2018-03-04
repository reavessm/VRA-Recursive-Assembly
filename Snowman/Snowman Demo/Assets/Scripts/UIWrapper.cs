using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIWrapper : MonoBehaviour {

	public Canvas GUICanvas;
	private bool uiIsUp; // This changes whenever the UI is pulled up
	private float guiDistance; // how far to place the gui infront of the player
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
	//SteamVR_Controller.Device rightHandController;
	//SteamVR_Controller.Device leftHandController;

	
	private Transform laserTransform; // The transform component of the laser for ease of use
	private Vector3 hitPoint; // Point where the raycast hits



	private SteamVR_Controller.Device Controller
	{
		get { return SteamVR_Controller.Input((int)trackedObj.index); }
	}

	void Awake () {
		trackedObj = GetComponent<SteamVR_TrackedObject>();
		//laser = ScriptableObject.CreateInstance<LaserPointer>();
        GUICanvas.gameObject.SetActive(false); // Hides UI initially
		guiDistance = 2f; // can change this if needed
		uiIsUp = false; // This changes whenever the UI is pulled up
        // to keep track of individual hands
        handIndex = (int)trackedObj.index;
        guiIndex = 0;
        /*foreach (GameObject obj in GameObject.FindGameObjectsWithTag("FloatingButton"))
        {
            buttonList.Add(obj.name, obj.GetComponent<Button>());
        }*/
	}

	private void Start()
	{
		laser = Instantiate(laserPrefab);
		laserTransform = laser.transform;
	}

	// Update is called once per frame
	void Update () {
		if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
		{
			//Debug.Log(gameObject.name + " Grip Press");
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



		if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Grip))
		{
			//Debug.Log(gameObject.name + " Grip Release");
			HideUI();
			//laser.hitPoint
			laser.SetActive(false);
		}

		uiIsUp = GUICanvas.gameObject.activeSelf;

        

    }

	private void ShowUI()
	{
		//Debug.Log("Showing UI stuff");
		GUICanvas.gameObject.SetActive(true);
		uiIsUp = true;
		GUICanvas.gameObject.transform.position = Camera.main.transform.position + Camera.main.transform.forward * guiDistance; // Transform to be in front of player, i hope...
		GUICanvas.gameObject.transform.position = new Vector3(GUICanvas.gameObject.transform.position.x, 1.5f, GUICanvas.gameObject.transform.position.z);
		GUICanvas.gameObject.transform.rotation = Camera.main.transform.rotation;
		GUICanvas.gameObject.transform.Rotate(new Vector3(-GUICanvas.gameObject.transform.rotation.x, 0, -GUICanvas.gameObject.transform.rotation.z));

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

    private void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

	private void ShowLaser(RaycastHit hit)
	{
		laser.SetActive(true); //Show the laser
		laserTransform.position = Vector3.Lerp(trackedObj.transform.position, hitPoint, .5f); // Move laser to the middle between the controller and the position the raycast hit
		laserTransform.LookAt(hitPoint); // Rotate laser facing the hit point
		laserTransform.localScale = new Vector3(laserTransform.localScale.x, laserTransform.localScale.y,
			hit.distance); // Scale laser so it fits exactly between the controller & the hit point
	}
}


