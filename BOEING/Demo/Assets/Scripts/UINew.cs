using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UINew : MonoBehaviour
{
    private GlobalVariables variables;
    private SteamVR_TrackedObject trackedObj;
    private SceneSetter sceneDirector;
    private SortedDictionary<string, GameObject> uiDict;
    private GameObject UI;
    private GameObject collidingObject;
    private GameObject resetter;
    private GameObject auto;
    private GameObject UIPrefab;



    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }
    
    // Use this for initialization
    void Awake()
    {
        variables = GameObject.Find("GlobalVariables").GetComponent<GlobalVariables>();
        UIPrefab = variables.GetUIBlocks();
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        if (sceneDirector == null)
        {
            sceneDirector = new SceneSetter();
        }
        sceneDirector.CustomInit();
        // Get all UI blocks

    }

    // Called after Awake()
    // Initialize dictionaries
    void Start()
    {
        UI = Instantiate(UIPrefab);
        UI.SetActive(false);
        uiDict = new SortedDictionary<string, GameObject>();
        resetter = GameObject.FindGameObjectWithTag("Restart");
        auto = GameObject.FindGameObjectWithTag("AutoAssemble");
    }

    // Update passed collider when pressing the trigger
    public void OnTriggerEnter(Collider other)
    {
        SetCollidingObject(other);
    }

    // Update passed collider when holding the trigger
    public void OnTriggerStay(Collider other)
    {
        SetCollidingObject(other);
    }

    // Update passed collider when releasing the tigger
    public void OnTriggerExit(Collider other)
    {
        if (!collidingObject)
        {
            return;
        }
        collidingObject = null;
    }

    // Set the colliding object when collision is detected
    private void SetCollidingObject(Collider col)
    {
        if (collidingObject || !col.GetComponent<Rigidbody>())
        {
            return;
        }
        collidingObject = col.gameObject;
    }



    // Update is called once per frame
    // Capture user input on controller collision with scene option boxes
    void Update()
    {
        if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
        {
            UI.SetActive(true);
            UI.transform.position = trackedObj.transform.position;
            UI.transform.rotation = Quaternion.Euler(0f, trackedObj.transform.rotation.eulerAngles.y, 0f);
        }
        if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Grip))
        {

            if (collidingObject)
            {
                switch (collidingObject.tag)
                {
                    case "Restart":
                        sceneDirector.Restart();
                        break;
                    case "AutoAssemble":
                        sceneDirector.AutoAssemble();
                        break;
                    case "ResetAssemble":
                        sceneDirector.ResetAssemble();
                        break;
                    case "Exit":
                        Debug.Log("Quit");
                        Application.Quit();
                        break;
                }
            }
            UI.SetActive(false);
        }
        sceneDirector.SlurpToGhost();
    }
}
