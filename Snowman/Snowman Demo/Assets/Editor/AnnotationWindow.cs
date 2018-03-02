using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEditor;
using UnityEditor.IMGUI.Controls;

public class AnnotationWindow : EditorWindow {

	[Serializable]
	public class MDVals {
		public int order;
		public int length;
		public int height;
		public int width;
		public int partid;
		public string misc;
	}

	

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
		if (GUILayout.Button("Generate Metadata Skeleton")){
			foreach (mData element in rawtree) {;
				if (element.Obj.GetComponent<Metadata>() == null) {
					element.Obj.AddComponent<Metadata>();
				}
			}
		}

		if (GUILayout.Button("Set Current Object as Root")) {
			foreach (mData element in rawtree) {
				if (element.Obj.GetComponent<Metadata>() != null) {
					element.Obj.GetComponent<Metadata>().setRootObject(currentobj);
				}
			}
		}

		if (GUILayout.Button("Validate Ordering")) {
			Metadata md = currentobj.GetComponent<Metadata>();
			Debug.Log(md.isNextInOrder());
		}

		foreach (mData element in rawtree) {
			EditorGUI.indentLevel = element.Depth;
			Metadata md = element.Obj.GetComponent("Metadata") as Metadata;
			if (md != null) {
				md.kvtagstring = EditorGUILayout.TextField(element.Obj.name, md.kvtagstring);
			}
		}			
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

	[MenuItem ("Window/Annotate Object Metadata")]
	public static void ShowWindow() {
		EditorWindow.GetWindow(typeof(AnnotationWindow), true, "Annotate Object Metadata");
	}
}
