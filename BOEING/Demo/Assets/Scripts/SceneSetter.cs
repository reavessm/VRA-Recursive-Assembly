using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class SceneSetter : MonoBehaviour {

    private GlobalVariables variables;
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
    private bool doneColor = false;

    private bool moveThrough = false;
    private int speedScale = 10;
    public bool brokenMode = false;

    private static bool autoAssembleOnStart = false;

    // Use this for initialization
    void Start() {
        CustomInit();
        if (autoAssembleOnStart)
        {
            this.AutoAssemble();
        }
    }


    private void Awake()
    {
        variables = GameObject.Find("GlobalVariables").GetComponent<GlobalVariables>();
        speedScale = variables.GetAutoAssSpeed();
        brokenMode = variables.GetBrokenMode();
        moveThrough = variables.GetMoveThrough();
        ghostColor = variables.GetGhostColor();
        ghostColorHi = variables.GetGhostColorHi();
        nextToPickUp = variables.GetNextToPickUpColor();
        setInPlace = variables.GetSetInPlaceColor();
    }

    void OnEnable()
    {
        Debug.Log("OnEnable called");
        SceneManager.sceneLoaded += OnSceneLoaded;
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

    // Is required but is not used
    private void OnSceneLoaded(Scene aScene, LoadSceneMode aMode)
    {
        //ColorNext();
    }

    // Initialize the colors for the model and ghost
    // Adds functionality to change 
    public void ColorNext()
    {
        if (!doneInit)
        {
            return;
        }
        doneColor = true;

        // Set the next ordered object to the designated next object color and change the previous object to the original color
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
                try {
                    obj.Value.GetComponent<Renderer>().material.color = nextToPickUp;
                    obj.Value.GetComponentInChildren<Renderer>().material.color = nextToPickUp;
                }
                catch {
                }
            }
            catch (KeyNotFoundException e) {

            }
        }
    }

    // Allow us to search for parts and ghosts by their corresponding names
    // Also, restructures DB after a part has been set
    private bool rebuildGODB()
    {
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
                    try {
                        if (!doneColor) 
                        {
                            defaultMaterialDictionary.Add(obj.Value.name, obj.Value.GetComponent<Renderer>().material.color);
                        }
                        else 
                        {
                            defaultMaterialDictionary.Add(obj.Value.name, variables.GetStandardMaterial().color);
                        }
                    }
                    catch {
                    }
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

    // Restarts the scene and resets the necessary variables 
    public void Restart()
    {
        // Should fix reset bug
        SetEverythingUnbuilt();

        autoassemble = false;
        index_autoassemble = 0;
        SceneManager.LoadScene("Demo", LoadSceneMode.Single);
    }

    // Same as Restart() but also calls autoassemble
    public void ResetAssemble()
    {
        autoAssembleOnStart = true;
        this.Restart();
    }

    // Automatically slurp parts from initial spawn to ghost in designated order
    // Build the model imported from FBX files
    public void AutoAssemble()
    {
        // Ensure the DB is structured properly before assembly process
        rebuildGODB();

        // Get the model and target (aka ghost) from the dictionary
        Debug.Log("AutoAssemble Trigger: " + index_autoassemble + " " + autoassemble);
        autoassemble_model = new KeyValuePair<string, GameObject>[gameObjectDictionary.Count];
        autoassemble_target = new KeyValuePair<string, GameObject>[ghostObjectDictionary.Count];
        int count = 0;

        // Set the current element in the dictionary to autoassemble_model
        foreach (KeyValuePair<string, GameObject> element in gameObjectDictionary)
        {
            Debug.Log(element);
            autoassemble_model[count++] = element;
        }
        count = 0;

        // Get the current element in the dictionary to autoassemble_target
        foreach (KeyValuePair<string, GameObject> element in ghostObjectDictionary)
        {
            autoassemble_target[count++] = element;
        }

        autoassemble = true;
        autoAssembleOnStart = false;
    }

    // Highlight the correct part on the ghost object
    public void HighlightGhost(GameObject obj)
    {
        GameObject ghost = ghostObjectDictionary[obj.name + " ghost"];
        Debug.Log("Highlight " + obj.name + " with " + ghost.name);
        ghost.GetComponent<Renderer>().material.color = ghostColorHi;
        ghost.GetComponentInChildren<Renderer>().material.color = ghostColorHi;
    }

    // Set all appropriate variables and lock object when snapped to correct position
    // Also, change the color of the current object to default color after it has been set
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

    // Helper function
    public void SlurpToGhost()
    {
        this.SlurpToGhost(index_autoassemble);
    }

    // Called every frame to move the current object from initiallized/current position to ghost
    public void SlurpToGhost(int index)
    {
        if (!autoassemble)
        {
            return;
        }

        GameObject snappingObject = autoassemble_model[index].Value;
        GameObject locationObject = autoassemble_target[index].Value;
        snappingObject.GetComponent<Metadata>().setBuilt(true);
        if (moveThrough)
        {
            snappingObject.GetComponent<MeshCollider>().convex = false;
        }
        snappingObject.GetComponent<MeshCollider>().convex = false;
        snappingObject.tag = "Untagged";
        snappingObject.GetComponent<Rigidbody>().isKinematic = true;

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

    // Removes the ghost highlighting after part has been set
    public void UnHighlightGhost(GameObject obj)
    {
        GameObject ghost = ghostObjectDictionary[obj.name + " ghost"];
        
        try {  
            Debug.Log("UnHighlight " + obj.name + " with " + ghost.name);
            ghost.GetComponent<Renderer>().material.color = Color.clear;
            
            Debug.Log("Default Material: " + defaultMaterialDictionary[obj.name].ToString());
            obj.GetComponent<Renderer>().material.color = variables.GetStandardMaterial().color;
            obj.GetComponentInChildren<Renderer>().material.color = variables.GetStandardMaterial().color;
        }
        catch (KeyNotFoundException e) {
            obj.GetComponent<Renderer>().material.color = variables.GetStandardMaterial().color;
            obj.GetComponentInChildren<Renderer>().material.color = variables.GetStandardMaterial().color;
        }
        catch (NullReferenceException e) {
        }
    }

    // This function has deprecated and out of scope for our 492 project
    public void UnsnapToGhost(GameObject snappingObject, GameObject locationObject)
    {
        snappingObject.GetComponent<Metadata>().setBuilt(false);
        locationObject.SetActive(true);
        snappingObject.GetComponent<Renderer>().material.color = nextToPickUp;
        snappingObject.GetComponentInChildren<Renderer>().material.color = nextToPickUp;
        ColorNext();
        autoassemble = false;
        index_autoassemble = 0;
    }

    // Setting all the objects in the passed dictionary to a given boolean value
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

    // Find the ghost for the designated gameObject for assembly
    public GameObject FindGhost(GameObject obj)
    {
        string ghostName = obj.name + " ghost";
        Debug.Log("Find Ghost: " + ghostName);
        GameObject ghost = ghostObjectDictionary[ghostName];
        Debug.Log("FindGhost Part 2: " + ghost.name);
        return ghost;
    }

    // Same as SetAllBuilt() except setting bool to explicitly true
    private void SetEverythingBuilt()
    {
        SetAllBuilt(gameObjectDictionary, true);
    }

    // Same as SetAllBuilt() except setting bool to explicitly false
    private void SetEverythingUnbuilt()
    {
        SetAllBuilt(gameObjectDictionary, false);
    }

    // Update is called once per frame
    void Update () {
    }
}
