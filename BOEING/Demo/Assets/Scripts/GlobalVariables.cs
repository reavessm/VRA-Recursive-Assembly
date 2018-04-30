using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEditor;
using UnityEditor.IMGUI.Controls;

public class GlobalVariables : MonoBehaviour {//EditorWindow {

  public Vector3 ghostOffset_;
  public Vector3 partsOffset_;
  public UnityEngine.Object table_;
  public float tableHeight_ = 0.25f;
  public int snappingDistance_ = 15;
  public int autoAssembleSpeed_ = 10;
  public UnityEngine.Object head_;
  public UnityEngine.Object leftController_;
  public UnityEngine.Object rightController_;
  public UnityEngine.Object UI_;
  public Material ghost_;
  public Canvas GUICanvas_;
  public SceneSetter sceneDirector_;
  public string DefaultInfo_ = "Pick up an object to see it's info here.";
  public LayerMask teleportMask_;
  public UnityEngine.Object laserPrefab_;
  public bool brokenMode_;
  public bool moveThrough_;
  public Color ghostColor_ = new Color32(0x00, 0xF2, 0xAC, 0x5D);
  public Color ghostColorHi_ = new Color32(0x00, 0x00, 0xAC, 0xA0);
  public Color nextToPickUp_ = new Color32(255, 0, 0, 0);
  public Color setInPlace_ = new Color32(0, 255, 0, 0);
  public bool gravityMode = false;
  public bool seperation = true;
  public Material standardMaterial_;

  void Awake() {
     // Update Deploy Script
 /*     Deploy.instance.SetPartsOffset(partsOffset_);
      Deploy.instance.SetGhostOffset(ghostOffset_);
      Deploy.instance.SetTheTable((GameObject)table_);
      Deploy.instance.SetTableHeight(tableHeight_);

      // Update Controller Grab Script
      ControllerGrabObject.instance.SetGhostMaterial(ghost_);
      ControllerGrabObject.instance.SetSnapDistance(snappingDistance_);
      ControllerGrabObject.instance.SetSceneDirecotr(sceneDirector_);
      ControllerGrabObject.instance.SetGUICanvs(GUICanvas_);
      ControllerGrabObject.instance.SetObjectInfo(DefaultInfo_);

      // Update SceneScetter Script
      SceneSetter.instance.SetMoveThrough(moveThrough_);
      SceneSetter.instance.SetSpeedScale(autoAssSpeed_);
      SceneSetter.instance.SetBrokenMode(brokenMode_);
      SceneSetter.instance.SetGhostColor(ghostColor_);
      SceneSetter.instance.SetGhostColorHi(ghostColorHi_);
      SceneSetter.instance.SetSetInPlaceColor(setInPlace_);

      // Update UINew Script
      UINew.instance.SetUIPrefab((GameObject)UI_);*/
  }

  public bool GetGravityMode(){
    return gravityMode;
  }
  public bool GetSeperation(){
    return seperation;
  }
  public GameObject GetHead(){
    return (GameObject)head_;
  }
  public GameObject GetLeftController(){
    return (GameObject)leftController_;
  }
  public GameObject GetRightController(){
    return (GameObject)rightController_;
  }  
  public LayerMask GetTeleportMask(){
    return teleportMask_;
  }
  public GameObject GetLaserPrefab(){
    return (GameObject)laserPrefab_;
  }

// Deploy Script
  public Vector3 GetGhostOffset() {
    return ghostOffset_;
  }
  public Vector3 GetPartsOffset(){
    return partsOffset_;
  }
  public GameObject GetTable() {
    return (GameObject)table_;
  }
  public float GetTableHeight(){
    return tableHeight_;
  }

// Controller Grab
  public Material GetGhostMaterial(){
    return ghost_;
  }
  public int GetSnappingDistance() {
    return snappingDistance_;
  }
  public SceneSetter GetSceneSetter(){
    return sceneDirector_;
  }
  public String GetDefaultInfo(){
    return DefaultInfo_;
  }
    public Canvas GetGUICanvas(){
    return GUICanvas_;
  }

// Scene Setter
  public int GetAutoAssSpeed(){
    return autoAssembleSpeed_;
  }
  public bool GetBrokenMode(){
    return brokenMode_;
  }
  public bool GetMoveThrough(){
    return moveThrough_;
  }
  public Color GetGhostColor(){
    return ghostColor_;
  }
  public Color GetGhostColorHi(){
    return ghostColorHi_;
  }
  public Color GetNextToPickUpColor(){
    return nextToPickUp_;
  }
  public Color GetSetInPlaceColor(){
    return setInPlace_;
  }

  public Material GetStandardMaterial() {
    return standardMaterial_;
  }
// UINew
  public GameObject GetUIBlocks(){
    return (GameObject)UI_;
  }
  



	/*[MenuItem ("Window/Set Global Variables")]
  [MenuItem ("Example/Popup")] 
   void Init()
  {
    // Get existing open window or if none, make a new one
    GlobalVariables variables = (GlobalVariables)EditorWindow.GetWindow(typeof(GlobalVariables));
    variables.minSize = new Vector2(600,600);
    variables.Show();

  } */
  
/*	public  void ShowWindow() {
		EditorWindow.GetWindow(typeof(AnnotationWindow), true, "Set Global Variables");
	} */

	/// <summary>
	/// OnGUI is called for rendering and handling GUI events.
	/// This function can be called multiple times per frame (one call per event).
	/// </summary>
/*	void OnGUI()
	{
    // Labels
    // Like h1 in html/markdown
		GUILayout.Label("Base Settings", EditorStyles.boldLabel);

    // Actual data

    partsOffset_ = EditorGUILayout.Vector3Field("Parts Offset: ", partsOffset_);
    ghostOffset_ = EditorGUILayout.Vector3Field("Ghost Offset: ", ghostOffset_);

    EditorGUILayout.Space();

    EditorGUILayout.LabelField("Default text for parts with no info");
    DefaultInfo_ = EditorGUILayout.TextField("Default Text: ", DefaultInfo_);

    EditorGUILayout.Space();

    snappingDistance_ = (int)EditorGUILayout.Slider("Snapping Distance: ", (float)snappingDistance_, 5, 40);
    autoAssSpeed_ = (int)EditorGUILayout.Slider("AutoAssembly Speed: ", (float)autoAssSpeed_, 1, 50);
    tableHeight_ = EditorGUILayout.Slider("Table Height: ", tableHeight_, .001f, .5f);

    EditorGUILayout.Space();

    EditorGUILayout.LabelField("HTC Vive Headset");
    head_ = EditorGUILayout.ObjectField(head_, typeof(GameObject), true);

    EditorGUILayout.LabelField("HTC Vive Left Controller");
    leftController_ = EditorGUILayout.ObjectField(leftController_, typeof(GameObject), true);

    EditorGUILayout.LabelField("HTC Vive Right Controller");
    rightController_ = EditorGUILayout.ObjectField(rightController_, typeof(GameObject), true);

    EditorGUILayout.LabelField("UI Blocks Prefab");
    UI_ = EditorGUILayout.ObjectField(UI_, typeof(GameObject), true);

    EditorGUILayout.LabelField("Laser Prefab");
    laserPrefab_ = EditorGUILayout.ObjectField(laserPrefab_, typeof(GameObject), true);

    EditorGUILayout.LabelField("Table Prefab");
    table_ = EditorGUILayout.ObjectField(table_, typeof(GameObject), true);

    EditorGUILayout.LabelField("Ghost Material");
    ghost_ = (Material)EditorGUILayout.ObjectField(ghost_, typeof(Material), true);

    EditorGUILayout.LabelField("GUI Canvas for Parts Info");
    GUICanvas_ = (Canvas)EditorGUILayout.ObjectField(GUICanvas_, typeof(Canvas), true);

    EditorGUILayout.LabelField("Scene Setter");
    sceneDirector_ = (SceneSetter)EditorGUILayout.ObjectField(sceneDirector_, typeof(SceneSetter), true);
    //teleportMask_ = EditorGUILayout.ObjectField(teleportMask_, typeof(LayerMask), true);

    EditorGUILayout.Space();

    ghostColor_ = EditorGUILayout.ColorField("Ghost Color: ", ghostColor_);

    EditorGUILayout.Space();

    ghostColorHi_ = EditorGUILayout.ColorField("Highlighted Ghost Color: ", ghostColorHi_);

    EditorGUILayout.Space();


    nextToPickUp_ = EditorGUILayout.ColorField("Highlight Next To Pick Up: ", nextToPickUp_);

    EditorGUILayout.Space();


    setInPlace_ = EditorGUILayout.ColorField("Snapped Color: ", setInPlace_);

    EditorGUILayout.Space();

    EditorGUILayout.LabelField("Allow objects to move through each other");
    moveThrough_ = EditorGUILayout.Toggle("Move Through: ", moveThrough_);

    EditorGUILayout.LabelField("Allow objects to break out of your hand (Not Recommended)");
    brokenMode_ = EditorGUILayout.Toggle("Broken Mode", brokenMode_);

    EditorGUILayout.Space();

    string text = "Click Update to update!";
    EditorGUILayout.LabelField(text);

    if(GUILayout.Button("Update")){
      if (GUICanvas_ == null) {
        // show popup
        text = "GUICanvas Can't be null";
      }
      // Update Deploy Script
      Deploy.instance.SetPartsOffset(partsOffset_);
      Deploy.instance.SetGhostOffset(ghostOffset_);
      Deploy.instance.SetTheTable((GameObject)table_);
      Deploy.instance.SetTableHeight(tableHeight_);

      // Update Controller Grab Script
      ControllerGrabObject.instance.SetGhostMaterial(ghost_);
      ControllerGrabObject.instance.SetSnapDistance(snappingDistance_);
      ControllerGrabObject.instance.SetSceneDirecotr(sceneDirector_);
      ControllerGrabObject.instance.SetGUICanvs(GUICanvas_);
      ControllerGrabObject.instance.SetObjectInfo(DefaultInfo_);

      // Update SceneScetter Script
      SceneSetter.instance.SetMoveThrough(moveThrough_);
      SceneSetter.instance.SetSpeedScale(autoAssSpeed_);
      SceneSetter.instance.SetBrokenMode(brokenMode_);
      SceneSetter.instance.SetGhostColor(ghostColor_);
      SceneSetter.instance.SetGhostColorHi(ghostColorHi_);
      SceneSetter.instance.SetSetInPlaceColor(setInPlace_);

      // Update UINew Script
      UINew.instance.SetUIPrefab((GameObject)UI_);
    } 

	}*/

}
