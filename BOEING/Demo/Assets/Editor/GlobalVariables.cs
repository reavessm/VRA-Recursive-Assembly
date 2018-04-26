using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEditor;
using UnityEditor.IMGUI.Controls;

public class GlobalVariables : EditorWindow {

  public static float ghostOffset_ = 10f;

	[MenuItem ("Window/Set Global Variables")]
	public static void ShowWindow() {
		EditorWindow.GetWindow(typeof(AnnotationWindow), true, "Set Global Variables");
	}

	/// <summary>
	/// OnGUI is called for rendering and handling GUI events.
	/// This function can be called multiple times per frame (one call per event).
	/// </summary>
	void OnGUI()
	{
		GUILayout.Label("Base Settings", EditorStyles.boldLable);

    ghostOffset_ = EditorGUILayout.Slider("Slider", ghostOffset_, 0, 20);
	}

}
