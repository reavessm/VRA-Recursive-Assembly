using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GlobalVariables : MonoBehaviour {

  // All the variables that can be set throughout our scripts
  public Vector3 ghostOffset_;
  public Vector3 partsOffset_;
  public Color ghostColor_ = new Color32(0x00, 0xF2, 0xAC, 0x5D);
  public Color ghostColorHi_ = new Color32(0x00, 0x00, 0xAC, 0xA0);
  public Color nextToPickUp_ = new Color32(255, 0, 0, 0);
  public Color setInPlace_ = new Color32(0, 255, 0, 0);
  public Material ghost_;
  public Material standardMaterial_;
  public int snappingDistance_ = 15;
  public int autoAssembleSpeed_ = 10;
  public int teleportRange_ = 10;
  public bool gravityMode = false;
  public bool seperation = true;
  public bool brokenMode_;
  public bool moveThrough_;
  public UnityEngine.Object head_;
  public UnityEngine.Object leftController_;
  public UnityEngine.Object rightController_;
  public UnityEngine.Object UI_;
  public UnityEngine.Object laserPrefab_;
  public UnityEngine.Object table_;
  public float tableHeight_ = 0.25f;
  public SceneSetter sceneDirector_;
  public LayerMask teleportMask_;
  public Canvas GUICanvas_;
  public string DefaultInfo_ = "Pick up an object to see it's info here.";

  // Following are the getters for each of the global variables

  // Gets the ghost offset for the Deploy script
  public Vector3 GetGhostOffset() {
    return ghostOffset_;
  }
  // Gets the parts offset for the Deploy script
  public Vector3 GetPartsOffset(){
    return partsOffset_;
  }
  // Gets the color of the ghost object for the Scene Setter script
  public Color GetGhostColor(){
    return ghostColor_;
  }
  // Gets the highlighted ghost color for the Scene Setter script
  public Color GetGhostColorHi(){
    return ghostColorHi_;
  }
  // Gets the color for the next object to be picked up for the Scene Setter script
  public Color GetNextToPickUpColor(){
    return nextToPickUp_;
  }
  // Gets the color for objects that are set in place for the Scene Setter script
  public Color GetSetInPlaceColor(){
    return setInPlace_;
  }
  // Gets the ghost material for the Controller Grab Object script
  public Material GetGhostMaterial(){
    return ghost_;
  }
  // Gets the standard material for the assembly for the Scene Setter script
  // Do not break, this is vital
  public Material GetStandardMaterial() {
    return standardMaterial_;
  }
  // Gets the snapping distance for the Controller Grab Object script
  public int GetSnappingDistance() {
    return snappingDistance_;
  }
  // Gets the auto assembly speed for the Scene Setter script
  public int GetAutoAssSpeed(){
    return autoAssembleSpeed_;
  }
  // Gets the teleport range for the Teleport script
  public int GetTeleportRange(){
    return teleportRange_;
  }
  // Gets if gravity is turned on for the Deploy script
  public bool GetGravityMode(){
    return gravityMode;
  }
  // Gets if seperation mode is set for Deploy script
  public bool GetSeperation(){
    return seperation;
  }
  // Gets if broken mode is enabled for the Scene Setter script
  public bool GetBrokenMode(){
    return brokenMode_;
  }
  // Gets if parts are able to move through each other for the Scene Setter script
  public bool GetMoveThrough(){
    return moveThrough_;
  }
  // Gets the headset
  // Deprecated
  public GameObject GetHead(){
    return (GameObject)head_;
  }
  // Gets the left controller
  // Deprecated
  public GameObject GetLeftController(){
    return (GameObject)leftController_;
  }
  // Gets the right controller
  // Deprecated
  public GameObject GetRightController(){
    return (GameObject)rightController_;
  }
  // Gets the UI Blocks to be displayed when the user presses the grip buttons 
  // for the UI New script
  public GameObject GetUIBlocks(){
    return (GameObject)UI_;
  }
  // Gets the laser prefav
  // Has been deprecated
  public GameObject GetLaserPrefab(){
    return (GameObject)laserPrefab_;
  }
  // Gets the table object for the Deploy script
  public GameObject GetTable() {
    return (GameObject)table_;
  }
  // Gets the table height for the Deploy script
  public float GetTableHeight(){
    return tableHeight_;
  }
  // Gets the scene setter for the Controller Grab Object script
  public SceneSetter GetSceneSetter(){
    return sceneDirector_;
  }
  // Gets the teleport mask for Teleport script
  public LayerMask GetTeleportMask(){
    return teleportMask_;
  }
  // Gets the GUI canvas for the Controller Grab Object script
    public Canvas GetGUICanvas(){
    return GUICanvas_;
  }
  // Gets the default GUI info for the Controller Grab Object script
  public String GetDefaultInfo(){
    return DefaultInfo_;
  }
}
