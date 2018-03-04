using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Metadata : MonoBehaviour {
	public string kvtagstring;
	public GameObject rootObject;
	public Boolean built = false;
	private SortedDictionary<string, string> kvtags;

	/// <summary>
	/// Called when the script is loaded or a value is changed in the
	/// inspector (Called in the editor only).
	/// </summary>
	void OnValidate() {
		updateTags();
	}

	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	void Start()
	{
		updateTags();
		//built = false;
	}

	private void updateTags() {
		kvtags = new SortedDictionary<string, string>();
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
			//Debug.Log(e.Message);
		}
	}

	public void appendTags(string tag_append) {
		foreach (string outer in tag_append.Split(';')) {
			if (kvtagstring.Length != 0) {
				if (kvtagstring[kvtagstring.Length - 1] != ';') {
					kvtagstring += ';';
				}
			}
			if (outer.Contains(":")) {
				kvtagstring += outer + ';';
			}
		}
		updateTags();
	}

	public GameObject getRootObject() {
		return rootObject;
	}

	public void setRootObject(GameObject newRootObject) {
		rootObject = newRootObject;
	}

	public void setBuilt(bool new_truth) {
		built = new_truth;
	}

	public Boolean getBuilt() {
		return built;
	}

	// This will tell you if the current object is "next in order" relative to the root object.
	// Will return true so long as no misordering violation is detected--this means is should be
	// callable for unordered assembly too.
	public bool isNextInOrder() {
        if (this.getBuilt())
        {
            return false;
        }
        updateTags();
		// This is the goal number.
		int thisorder = getOrder() - 1;
		
		// If the current object's order is -1, it means the object is unordered and should
		// be considered buildable.
		if (thisorder < 0) {
			return true;
		}
		
		// Check to see if the root object *has* metadata. If not, the program has been misconfigured. 
		if (rootObject.GetComponent<Metadata>() == null) {
			Debug.Log("The root object has an improper Metadata initialization, please create Metadata Skeleton and set the root object.", rootObject);
			return false;
		}
		
		// We need to find the starting order number, to handle the multiple assembly case.
		int startorder;
		if (rootObject.GetComponent<Metadata>().getOrder() == -1) {
			Debug.Log("The root object does not have an order. If this is not intentional, check the root object kvtagstring.", rootObject);
			startorder = 0;
		}
		else {
			startorder = rootObject.GetComponent<Metadata>().getOrder();
		}

		// We're now going to check to see if there is an ordering discontinuity. If there is, this
		// object is not the next object in order.		
		bool[] ary = new bool[rootObject.transform.childCount + 1];
		foreach (Transform element in rootObject.transform) {
			int orderindex = element.gameObject.GetComponent<Metadata>().getOrder();
			if (orderindex > 0) {
				if (!ary[orderindex-1]) {
					if (element.gameObject.GetComponent<Metadata>().getBuilt()) {
						ary[orderindex-1] = true;
					}
				}
			}
		}
		for (int i = startorder; i < thisorder; i++) {
			if(!ary[i]) {
				return false;
			}
		}
		return true;
	}

	public void setTags(string raw_tag) {
		kvtagstring = raw_tag;
		updateTags();
	}

	public SortedDictionary<string, string> getTags() {
		return kvtags;
	}

	public void clearKVTags() {
		kvtagstring = "";
		kvtags = new SortedDictionary<string, string>();
	}

	public override string ToString() {
		string temp = "";
		foreach (KeyValuePair<string, string> entry in kvtags) {
			temp += entry.Key + ":" + entry.Value + ";";
		}
		return temp;
	}

	public void clearKVTag(string key) {
		kvtags.Remove(key);
		kvtagstring = ToString();
	}

	public string getValueAtTag(string key) {
		string value = "";
		if (kvtags.ContainsKey(key)) {
			value = kvtags[key];
		}
		return value;
	}

	public Int32 getOrder() {
		updateTags();
		Int32 temp = -1;
		if (kvtags.ContainsKey("order")) {
			Int32.TryParse(kvtags["order"], out temp);
		}
		return temp;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
