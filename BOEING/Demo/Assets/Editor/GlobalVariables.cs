using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEditor;
using UnityEditor.IMGUI.Controls;

public class GlobalVariables : EditorWindow {

  public static Vector3 ghostOffset_;
  public static Vector3 partsOffset_;
  public static int snappingDistance_ = 15;
  public static int autoAssSpeed_ = 10;
  public static UnityEngine.Object head_;
  public static UnityEngine.Object leftController_;
  public static UnityEngine.Object rightController_;
  public static UnityEngine.Object UI_;
  public static Material ghost_;
  public static Canvas GUICanvas_;
  public static SceneSetter sceneDirector_;
  public static string DefaultInfo_ = "Default Info";
  public static LayerMask teleportMask_;
  public static UnityEngine.Object laserPrefab_;
  public static bool brokenMode_;
  public static bool moveThrough_;

	[MenuItem ("Window/Set Global Variables")]
  static void Init()
  {
    // Get existing open window or if none, make a new one
    GlobalVariables variables = (GlobalVariables)EditorWindow.GetWindow(typeof(GlobalVariables));
    variables.Show();
  }
/*	public static void ShowWindow() {
		EditorWindow.GetWindow(typeof(AnnotationWindow), true, "Set Global Variables");
	} */

	/// <summary>
	/// OnGUI is called for rendering and handling GUI events.
	/// This function can be called multiple times per frame (one call per event).
	/// </summary>
	void OnGUI()
	{
    // Labels
    // Like h1 in html/markdown
		GUILayout.Label("Base Settings", EditorStyles.boldLabel);

    // Actual data
    EditorGUILayout.BeginHorizontal();
    partsOffset_ = EditorGUILayout.Vector3Field("Parts Offset: ", partsOffset_);
    EditorGUILayout.EndHorizontal();

    ghostOffset_ = EditorGUILayout.Vector3Field("Ghost Offset: ", ghostOffset_);
    snappingDistance_ = (int)EditorGUILayout.Slider("Snapping Distance: ", (float)snappingDistance_, 5, 40);
    autoAssSpeed_ = (int)EditorGUILayout.Slider("AutoAssembly Speed: ", (float)autoAssSpeed_, 1, 50);

    EditorGUILayout.LabelField("HTC Vive Headset");
    head_ = EditorGUILayout.ObjectField(head_, typeof(GameObject), true);

    EditorGUILayout.LabelField("HTC Vive Left Controller");
    leftController_ = EditorGUILayout.ObjectField(leftController_, typeof(GameObject), true);

    EditorGUILayout.LabelField("HTC Vive Right Controller");
    rightController_ = EditorGUILayout.ObjectField(rightController_, typeof(GameObject), true);

    EditorGUILayout.LabelField("UI Blocks Prefab");
    UI_ = EditorGUILayout.ObjectField(UI_, typeof(GameObject), true);

    EditorGUILayout.LabelField("GUI Canvas for Parts Info");
    GUICanvas_ = (Canvas)EditorGUILayout.ObjectField(GUICanvas_, typeof(Canvas), true);

    EditorGUILayout.LabelField("Scene Setter");
    sceneDirector_ = (SceneSetter)EditorGUILayout.ObjectField(sceneDirector_, typeof(SceneSetter), true);
    //teleportMask_ = EditorGUILayout.ObjectField(teleportMask_, typeof(LayerMask), true);

    EditorGUILayout.LabelField("Laser Prefab");
    laserPrefab_ = EditorGUILayout.ObjectField(laserPrefab_, typeof(GameObject), true);

    EditorGUILayout.LabelField("Ghost Material");
    ghost_ = (Material)EditorGUILayout.ObjectField(ghost_, typeof(Material), true);

    EditorGUILayout.LabelField("Allow objects to move through each other: ");
    moveThrough_ = EditorGUILayout.Toggle("Move Through", moveThrough_);

    EditorGUILayout.LabelField("Allow objects to break out of your hand (Not Recommended)");
    brokenMode_ = EditorGUILayout.Toggle("Broken Mode", brokenMode_);

    EditorGUILayout.LabelField("Default text for parts with no info");
    DefaultInfo_ = EditorGUILayout.TextField("Default Text", DefaultInfo_);
	}

}
