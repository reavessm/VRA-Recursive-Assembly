using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Metadata : MonoBehaviour {
	public string kvtagstring;

	private SortedDictionary<string, string> kvtags;

	/// <summary>
	/// Called when the script is loaded or a value is changed in the
	/// inspector (Called in the editor only).
	/// </summary>
	void OnValidate() {
		updateTags();
	}

	private void updateTags() {
		try {
			if (!String.IsNullOrEmpty(kvtagstring)) {
				if (kvtagstring.Contains(":") && !kvtagstring.EndsWith(":")) {
					string[] outer = kvtagstring.Split(';');
					foreach (string tag in outer) {
						string[] inner = tag.Split(':');
						kvtags.Add(inner[0], inner[1]);
					}
				}
			}	
		}
		catch (Exception e) {
			Debug.Log(e.Message);
		}
	}

	public void setTags(string raw_tag) {
		kvtagstring = raw_tag;
		updateTags();
	}

	public SortedDictionary<string, string> getTags() {
		return kvtags;
	}

	public int getOrder() {
		int temp = -1;
		Int32.TryParse(kvtags["order"], out temp);
		return temp;
	}
	

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
