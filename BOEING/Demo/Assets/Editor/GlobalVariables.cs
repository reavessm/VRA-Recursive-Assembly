using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEditor;
using UnityEditor.IMGUI.Controls;

public class GlobalVariables : EditorWindow {

  public static float ghostOffset_ = 10f;
  public static float partsOffset_ = 10f;

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
    partsOffset_ = EditorGUILayout.Slider("Parts Offset", partsOffset_, 0, 20);
    ghostOffset_ = EditorGUILayout.Slider("Ghost Offset", ghostOffset_, 0, 20);
	}

}
