using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class SceneSetter : MonoBehaviour {

    private SortedDictionary<string, GameObject> gameObjectDictionary;
    private SortedDictionary<string, GameObject> ghostObjectDictionary;
    private SortedDictionary<string, Color> defaultMaterialDictionary;
    private SortedDictionary<string, Color> currentMaterialDictionary;

    private bool autoassemble = false;
    private KeyValuePair<string, GameObject>[] autoassemble_model;
    private KeyValuePair<string, GameObject>[] autoassemble_target;
    private int index_autoassemble;

    private Color ghostColor = new Color32(0x00, 0xF2, 0xAC, 0x5D);
    private Color ghostColorHi = new Color32(0x00, 0x00, 0xAC, 0xA0);
    private Color nextToPickUp = new Color32(255, 0, 0, 0);
    private Color setInPlace = new Color32(0, 255, 0, 0);
    private bool doneInit = false;

    public bool moveThrough = false;
    public int speedScale = 10;
    public bool brokenMode = false;
    //static Canvas GUICanvas;

    private static bool autoAssembleOnStart = false;

    // Use this for initialization
    void Start() {
        Debug.Log("This is Start()");
        CustomInit();
        if (autoAssembleOnStart)
        {
            Debug.Log("autoAssembleOnStart is true");
            this.AutoAssemble();
        }
    }


    private void Awake()
    {
    }

    void OnEnable()
    {
        Debug.Log("OnEnable called");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene aScene, LoadSceneMode aMode)
    {
        ColorNext();
    }

    public void CustomInit()
    {
        Debug.Log("Starting CustomInit()");
        autoassemble = false;
        index_autoassemble = 0;
        gameObjectDictionary = new SortedDictionary<string, GameObject>();
        ghostObjectDictionary = new SortedDictionary<string, GameObject>();
        defaultMaterialDictionary = new SortedDictionary<string, Color>();
        currentMaterialDictionary = new SortedDictionary<string, Color>();

        rebuildGODB(); // initialize db for ColorNext
        doneInit = true;
        ColorNext(); // initialize colors
    }


    public void ColorNext()
    {
        if (!doneInit)
        {
            return;
        }
        //rebuildGODB();
        foreach (KeyValuePair<string, GameObject> obj in gameObjectDictionary)
        {
            try
            {
                if (obj.Value.GetComponent<Metadata>().isNextInOrder() && !obj.Value.GetComponent<Metadata>().getBuilt())
                {
                    obj.Value.GetComponent<Renderer>().material.color = nextToPickUp;
                    obj.Value.GetComponentInChildren<Renderer>().material.color = nextToPickUp;
                }
                else {
                    obj.Value.GetComponent<Renderer>().material.color = defaultMaterialDictionary[obj.Value.name];
                    obj.Value.GetComponentInChildren<Renderer>().material.color = defaultMaterialDictionary[obj.Value.name];
                }
            }
            catch (NullReferenceException e)
            {
                obj.Value.GetComponent<Renderer>().material.color = nextToPickUp;
                obj.Value.GetComponentInChildren<Renderer>().material.color = nextToPickUp;
            }
        }
    }

    private bool rebuildGODB()
    {
        //ColorNext();
        if (gameObjectDictionary.Count == 0)
        {
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Pickupable"))
            {
                Debug.Log(obj.name);
                gameObjectDictionary.Add(obj.name, obj.gameObject);
            }
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Ghost"))
            {
                Debug.Log(obj.name);
                ghostObjectDictionary.Add(obj.name, obj.gameObject);
            }
            if (defaultMaterialDictionary.Count == 0)
            {
                foreach (KeyValuePair<string, GameObject> obj in gameObjectDictionary)
                {
                    defaultMaterialDictionary.Add(obj.Value.name, obj.Value.GetComponent<Renderer>().material.color);
                }
            }
            currentMaterialDictionary = defaultMaterialDictionary; 
        }
        if (gameObjectDictionary.Count >= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Restart()
    {
        // Should fix reset bug
        SetEverythingUnbuilt();

        autoassemble = false;
        index_autoassemble = 0;
        SceneManager.LoadScene("Demo", LoadSceneMode.Single);
    }

    public void ResetAssemble()
    {
        autoAssembleOnStart = true;
        this.Restart();
    }

    public void AutoAssemble()
    {
        rebuildGODB();
        Debug.Log("AutoAssemble Trigger: " + index_autoassemble + " " + autoassemble);
        autoassemble_model = new KeyValuePair<string, GameObject>[gameObjectDictionary.Count];
        autoassemble_target = new KeyValuePair<string, GameObject>[ghostObjectDictionary.Count];
        int count = 0;
        foreach (KeyValuePair<string, GameObject> element in gameObjectDictionary)
        {
            Debug.Log(element);
            autoassemble_model[count++] = element;
        }
        count = 0;
        foreach (KeyValuePair<string, GameObject> element in ghostObjectDictionary)
        {
            autoassemble_target[count++] = element;
        }
        autoassemble = true;
        autoAssembleOnStart = false;
    }

    public void HighlightGhost(GameObject obj)
    {
        GameObject ghost = ghostObjectDictionary[obj.name + " ghost"];
        Debug.Log("Highlight " + obj.name + " with " + ghost.name);
        ghost.GetComponent<Renderer>().material.color = ghostColorHi;
        ghost.GetComponentInChildren<Renderer>().material.color = ghostColorHi;
    }

    public void SnapToGhost(GameObject snappingObject, GameObject locationObject)
    {
        if (moveThrough)
        {
            snappingObject.GetComponent<MeshCollider>().convex = false;
            Destroy(snappingObject.GetComponent<Rigidbody>());

        }
        snappingObject.GetComponent<MeshCollider>().convex = false;
        snappingObject.transform.SetPositionAndRotation(locationObject.transform.position,
            locationObject.transform.rotation);
        Debug.Log(snappingObject.ToString());
        UnHighlightGhost(snappingObject);
        snappingObject.GetComponent<Renderer>().material.color = setInPlace;
        snappingObject.GetComponentInChildren<Renderer>().material.color = setInPlace;
        Debug.Log(snappingObject.transform.position);
        snappingObject.GetComponent<Rigidbody>().isKinematic = true;
        snappingObject.tag = "Untagged";
        snappingObject.GetComponent<MeshCollider>().convex = false;
        snappingObject.GetComponent<Metadata>().setBuilt(true);
        ColorNext();
    }

/*    public void SlerpTogether(GameObject thing)
    {
        GameObject snappingObject = thing;
        GameObject locationObject = ghostObjectDictionary[thing.name + " ghost"];
        SlerpTogether(snappingObject, locationObject);
    }

    public void SlerpTogether(GameObject snappingObject, GameObject locationObject)
    {
        snappingObject.GetComponent<Metadata>().setBuilt(true);
        snappingObject.GetComponent<MeshCollider>().convex = false;
        // This didn't work.
        //snappingObject.transform.SetPositionAndRotation(Vector3.MoveTowards(snappingObject.transform.position, locationObject.transform.position, 10 * Time.deltaTime), Quaternion.LookRotation(Vector3.RotateTowards(snappingObject.transform.rotation * Vector3.one, locationObject.transform.rotation * Vector3.one, 10 * Time.deltaTime, 0.0f)));
        //   snappingObject.transform.SetPositionAndRotation(Vector3.MoveTowards(snappingObject.transform.position, locationObject.transform.position, speedScale * Time.deltaTime), locationObject.transform.rotation);
        snappingObject.transform.rotation = locationObject.transform.rotation;
        snappingObject.transform.position = Vector3.Slerp(snappingObject.transform.position, locationObject.transform.position, speedScale * Time.deltaTime);
        UnHighlightGhost(snappingObject);
        snappingObject.GetComponent<Renderer>().material.color = setInPlace;
        snappingObject.GetComponentInChildren<Renderer>().material.color = setInPlace;
        Debug.Log(snappingObject.transform.position);
        ColorNext();
        snappingObject.GetComponent<MeshCollider>().convex = false;
        if (snappingObject.transform.position == locationObject.transform.position)
        {
            Debug.Log(index_autoassemble);
            Debug.Log(gameObjectDictionary.Count);
            Debug.Log(ghostObjectDictionary.Count);
            if (index_autoassemble < (gameObjectDictionary.Count - 1))
            {
                index_autoassemble++;
            }
            else
            {
                Debug.Log("Done?");
                autoassemble = false;
            }
        }
    } */

    public void SlurpToGhost()
    {
        this.SlurpToGhost(index_autoassemble);
    }

    public void SlurpToGhost(int index)
    {
        if (!autoassemble)
        {
            return;
        }
/*
        bool done = false;
         while (!done) {
              done = true;
              foreach (KeyValuePair<string, GameObject> thing in gameObjectDictionary) {
                  if (!thing.Value.GetComponent<Metadata>().getBuilt()) {
                      done = false;
                      SlerpTogether(thing.Value);
                  }
              }
         } */
         
        Debug.Log("SlurpToGhost Trigger: " + index + " " + autoassemble + " " + autoassemble_model.Length);
        Debug.Log("Slurp me");
        GameObject snappingObject = autoassemble_model[index].Value;
        GameObject locationObject = autoassemble_target[index].Value;
        Debug.Log(snappingObject.name);
        Debug.Log(locationObject.name);
        snappingObject.GetComponent<Metadata>().setBuilt(true);
        if (moveThrough)
        {
            snappingObject.GetComponent<MeshCollider>().convex = false;
        }
        snappingObject.GetComponent<MeshCollider>().convex = false;
        snappingObject.tag = "Untagged";
        snappingObject.GetComponent<Rigidbody>().isKinematic = true;
        // This didn't work.
        //snappingObject.transform.SetPositionAndRotation(Vector3.MoveTowards(snappingObject.transform.position, locationObject.transform.position, 10 * Time.deltaTime), Quaternion.LookRotation(Vector3.RotateTowards(snappingObject.transform.rotation * Vector3.one, locationObject.transform.rotation * Vector3.one, 10 * Time.deltaTime, 0.0f)));
        //   snappingObject.transform.SetPositionAndRotation(Vector3.MoveTowards(snappingObject.transform.position, locationObject.transform.position, speedScale * Time.deltaTime), locationObject.transform.rotation);
        snappingObject.transform.rotation = locationObject.transform.rotation;
        snappingObject.transform.position = Vector3.Slerp(snappingObject.transform.position, locationObject.transform.position, speedScale * Time.deltaTime);
        UnHighlightGhost(snappingObject);
        snappingObject.GetComponent<Renderer>().material.color = setInPlace;
        snappingObject.GetComponentInChildren<Renderer>().material.color = setInPlace;
        Debug.Log(snappingObject.transform.position);
        ColorNext();
        snappingObject.GetComponent<MeshCollider>().convex = false;
        if (snappingObject.transform.position == locationObject.transform.position)
        {
            Debug.Log(index_autoassemble);
            Debug.Log(gameObjectDictionary.Count);
            Debug.Log(ghostObjectDictionary.Count);
            if (index_autoassemble < (gameObjectDictionary.Count - 1))
            {
                index_autoassemble++;
            }
            else
            {
                Debug.Log("Done?");
                autoassemble = false;
            }
        } 
    }

    public void UnHighlightGhost(GameObject obj)
    {
        GameObject ghost = ghostObjectDictionary[obj.name + " ghost"];
        Debug.Log("UnHighlight " + obj.name + " with " + ghost.name);
        ghost.GetComponent<Renderer>().material.color = Color.clear;
        foreach (KeyValuePair<string, Color> col in defaultMaterialDictionary)
        {
            Debug.Log("Default Material Dictionary: " + col.Key + " || " + col.Value);
        }
        Debug.Log("Default Material: " + defaultMaterialDictionary[obj.name].ToString());
        obj.GetComponent<Renderer>().material.color = defaultMaterialDictionary[obj.name];
        obj.GetComponentInChildren<Renderer>().material.color = defaultMaterialDictionary[obj.name];
    }

    public void UnsnapToGhost(GameObject snappingObject, GameObject locationObject)
    {
        snappingObject.GetComponent<Metadata>().setBuilt(false);
        //snappingObject.transform.position = locationObject.transform.position;
        //UnHighlightGhost(snappingObject);
        locationObject.SetActive(true);
        snappingObject.GetComponent<Renderer>().material.color = nextToPickUp;
        snappingObject.GetComponentInChildren<Renderer>().material.color = nextToPickUp;
        ColorNext();
        autoassemble = false;
        index_autoassemble = 0;

    }

    public void SetAllBuilt(SortedDictionary<string, GameObject> dict, bool boolean)
    {
        try
        {
            foreach (KeyValuePair<string, GameObject> obj in dict)
            {
                obj.Value.GetComponent<Metadata>().setBuilt(boolean);
            }
        }
        catch (NullReferenceException e)
        {
        }
    }

    public GameObject FindGhost(GameObject obj)
    {
        string ghostName = obj.name + " ghost";
        Debug.Log("Find Ghost: " + ghostName);
        GameObject ghost = ghostObjectDictionary[ghostName];
        Debug.Log("FindGhost Part 2: " + ghost.name);
        return ghost;
    }

    private void SetEverythingBuilt()
    {
        SetAllBuilt(gameObjectDictionary, true);
    }

    private void SetEverythingUnbuilt()
    {
        SetAllBuilt(gameObjectDictionary, false);
    }



    // Update is called once per frame
    void Update () {
    }
}
