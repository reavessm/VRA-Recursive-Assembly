using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UINew : MonoBehaviour
{
    private GlobalVariables variables;
    private GameObject UIPrefab;

    private SteamVR_TrackedObject trackedObj;
    private GameObject UI;
    private GameObject collidingObject;
    private SceneSetter sceneDirector;
    private SortedDictionary<string, GameObject> uiDict;
    private GameObject resetter;
    private GameObject auto;


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

    void Start()
    {
        UI = Instantiate(UIPrefab);
        UI.SetActive(false);
        uiDict = new SortedDictionary<string, GameObject>();
        /* foreach(Transform child in UI.transform) {
             uiDict.Add(child.gameObject.name, child.gameObject);
         } */

        /* uiDict.Add("reset",GameObject.FindGameObjectWithTag("Restart"));
        uiDict.Add("auto", GameObject.FindGameObjectWithTag("AutoAssemble"));
        Debug.Log(uiDict.ToString()); */
        resetter = GameObject.FindGameObjectWithTag("Restart");
        auto = GameObject.FindGameObjectWithTag("AutoAssemble");
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



    // Update is called once per frame

    void Update()
    {
        if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
        {
            UI.SetActive(true);
            UI.transform.position = trackedObj.transform.position;
            UI.transform.rotation = Quaternion.Euler(0f, trackedObj.transform.rotation.eulerAngles.y, 0f);
            //UI.transform.Rotate(new Vector3(0f, trackedObj.transform.rotation.y, 0f));		
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

        // Rotate UI Blocks
        /* foreach (KeyValuePair<string, GameObject> obj in uiDict) {
          obj.Value.transform.Rotate(new Vector3(15,30,45) * Time.deltaTime);
        } */
        /* if (UI.activeSelf)
         {
             resetter.transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
             auto.transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
         } */

    }

}
