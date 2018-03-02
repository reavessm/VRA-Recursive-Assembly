using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AnnotationWindow : EditorWindow {

	private class mData {
		public int Depth {get; set;}
		public GameObject Obj {get; set;}
		public Component Metadata {get; set;}

		public mData() {;
			Depth = 0;
			Obj = null;
			Metadata = null;
		}

		public mData(int level, GameObject anObject, Component aComponent) {
			Depth = level;
			Obj = anObject;
			Metadata = aComponent;
		} 
	}

	private GameObject currentobj;
	private List<mData> rawtree; 

	/// <summary>
	/// OnGUI is called for rendering and handling GUI events.
	/// This function can be called multiple times per frame (one call per event).
	/// </summary>
	void OnGUI()
	{
		currentobj = Selection.activeGameObject;
		rawtree = buildRawTree(currentobj);
		// Window Code Goes Here
		GUILayout.Label("Annotate Objects");
		List<string> names = new List<string>();
		List<string> tags = new List<string>();
		foreach (mData element in rawtree) {
			EditorGUI.indentLevel = element.Depth;
			names.Add(EditorGUILayout.TextField("Object Name", element.Obj.name));
			EditorGUI.indentLevel = element.Depth + 1;
			if (element.Obj.GetComponent("Metadata") != null) {
				Metadata md = element.Obj.GetComponent("Metadata") as Metadata;
				tags.Add(EditorGUILayout.TextField("Tag Notation", md.kvtagstring));
			}
		}			
	}

	void OnSelectionChange() {
		currentobj = Selection.activeGameObject;
	}

	List<mData> buildRawTree(GameObject current) {
		Transform currentT = current.transform;
		List<mData> temp = new List<mData>();
		temp.Add(new mData(0, current.gameObject, current.GetComponent("metadata")));
		foreach (Transform child in currentT) {
			temp.Add(new mData(1, child.gameObject, child.GetComponent("metadata")));
		}
		return temp;
	}

	[MenuItem ("Window/Annotate Scene")]
	public static void ShowWindow() {
		EditorWindow.GetWindow(typeof(AnnotationWindow));
	}
}
