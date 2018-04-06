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
    private Color ghostColorHi = new Color32(0x00, 0xF2, 0xAC, 0xA0);
    private Color nextToPickUp = new Color32(255, 0, 0, 0);
    private Color setInPlace = new Color32(0, 255, 0, 0);

    public static bool moveThrough = false;
    //static Canvas GUICanvas;

    // Use this for initialization
    void Start () {
        Debug.Log("This is Start()");
        CustomInit();
	}

    private void Awake()
    {
        Debug.Log("This is Awake()");
        //CustomInit();
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

        ColorNext();
    }

    void ColorNext()
    {
        rebuildGODB();
        foreach (KeyValuePair<string, GameObject> obj in gameObjectDictionary)
        {
            try
            {
                if (obj.Value.GetComponent<Metadata>().isNextInOrder())
                {
                    obj.Value.GetComponent<Renderer>().material.color = nextToPickUp;
                    obj.Value.GetComponentInChildren<Renderer>().material.color = nextToPickUp;
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
        if (gameObjectDictionary.Count == 0)
        {
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Pickupable"))
            {
                gameObjectDictionary.Add(obj.name, obj.gameObject);
            }
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Ghost"))
            {
                ghostObjectDictionary.Add(obj.name, obj);
            }
            foreach (KeyValuePair<string, GameObject> obj in gameObjectDictionary)
            {
                defaultMaterialDictionary.Add(obj.Value.name, obj.Value.GetComponent<Renderer>().material.color);
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
        SceneManager.LoadScene("Snowman", LoadSceneMode.Single);
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
    }

    public void HighlightGhost(GameObject obj)
    {
        foreach (KeyValuePair<string, GameObject> ghost in ghostObjectDictionary)
        {
            if (ghost.Key == obj.name)
            {
                ghost.Value.GetComponent<Renderer>().material.color = ghostColorHi;
                ghost.Value.GetComponentInChildren<Renderer>().material.color = ghostColorHi;
            }
        }
    }

    public void SnapToGhost(GameObject snappingObject, GameObject locationObject)
    {
        snappingObject.GetComponent<Metadata>().setBuilt(true);
        if (moveThrough)
        {
            snappingObject.GetComponent<MeshCollider>().convex = false;
            Destroy(snappingObject.GetComponent<Rigidbody>());

        }
        snappingObject.GetComponent<MeshCollider>().convex = false;
        snappingObject.transform.SetPositionAndRotation(locationObject.transform.position,
            locationObject.transform.rotation);
        UnHighlightGhost(snappingObject);
        snappingObject.GetComponent<Renderer>().material.color = setInPlace;
        snappingObject.GetComponentInChildren<Renderer>().material.color = setInPlace;
        Debug.Log(snappingObject.transform.position);
        ColorNext();
        Destroy(snappingObject.GetComponent<Rigidbody>());
        snappingObject.GetComponent<MeshCollider>().convex = false;
        snappingObject.GetComponent<Metadata>().setBuilt(true);
    }

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
        snappingObject.transform.position = Vector3.MoveTowards(snappingObject.transform.position, locationObject.transform.position, 10 * Time.deltaTime);
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

    }

    public void UnsnapToGhost(GameObject snappingObject, GameObject locationObject)
    {
        snappingObject.GetComponent<Metadata>().setBuilt(false);
        //snappingObject.transform.position = locationObject.transform.position;
        UnHighlightGhost(snappingObject);
        snappingObject.GetComponent<Renderer>().material.color = nextToPickUp;
        snappingObject.GetComponentInChildren<Renderer>().material.color = nextToPickUp;
        ColorNext();
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
        Debug.Log(ghostName);
        GameObject ghost = ghostObjectDictionary[ghostName];
        Debug.Log(ghost.name);
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

     /*   if (autoassemble)
        {
            SlurpToGhost(index_autoassemble);
        } */
    }
}
