using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.IMGUI.Controls;

public class FBXImporter : EditorWindow {
	void OnGUI()
	{
		
	}

	public void Update() {
		Repaint();
	}

	[MenuItem ("Window/FBX Importer")]
	public static void ShowWindow() {
		EditorWindow.GetWindow(typeof(FBXImporter), true, "FBX Importer");
	}
}
