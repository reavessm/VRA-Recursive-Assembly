using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AnnotationWindow : EditorWindow {
	/// <summary>
	/// OnGUI is called for rendering and handling GUI events.
	/// This function can be called multiple times per frame (one call per event).
	/// </summary>
	void OnGUI()
	{
		// Window Code Goes Here
			
	}

	[MenuItem ("Window/Annotate Scene")]
	public static void ShowWindow() {
		EditorWindow.GetWindow(typeof(AnnotationWindow));
	}
}
