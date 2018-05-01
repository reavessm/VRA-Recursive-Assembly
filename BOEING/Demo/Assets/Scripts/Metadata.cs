using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Metadata : MonoBehaviour {
	public string kvtagstring;
	public GameObject rootObject;
	public Boolean built = false;
	private SortedDictionary<string, string> kvtags;

	// Called when the script is loaded or a value is changed in the
	// inspector (Called in the editor only).
	void OnValidate() {
		updateTags();
	}

	// Start is called on the frame when a script is enabled just before
	// any of the Update methods is called the first time.
	void Start()
	{
		updateTags();
		built = false;
	}
  
  // Update is called once per frame
	void Update () {}

	// Updates the KV tags that have been changed, called in OnValidate
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
		}
	}

  // Appends the new tags to the end of the current tags
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

  // Gets the object currently set as the root object
	public GameObject getRootObject() {
		return rootObject;
	}

  // Sets the root object
	public void setRootObject(GameObject newRootObject) {
		rootObject = newRootObject;
	}

  // Sets an object to be built. Called after an object has been snapped into
	// place
	public void setBuilt(bool new_truth) {
		built = new_truth;
	}

    // Gets if an object has been built yet
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
			return false;
		}

		// We need to find the starting order number, to handle the multiple assembly case.
		int startorder;
		if (rootObject.GetComponent<Metadata>().getOrder() == -1) {
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

  // Sets the KV Tags
	public void setTags(string raw_tag) {
		kvtagstring = raw_tag;
		updateTags();
	}

  // Gets the KV tags, returning the sorted dictionary of keys
	public SortedDictionary<string, string> getTags() {
		return kvtags;
	}

  // Clears all the KV tags in the sorted dictionary
	public void clearKVTags() {
		kvtagstring = "";
		kvtags = new SortedDictionary<string, string>();
	}

  // Makes the text much more humanly readable
  public string PrettyPrint() {
		string temp = "";

    if (kvtags.Count == 0) {
      temp += "Please use the Metadata Annotation Window to add metadata";
      temp += " tags to objects.  Then you will see the data here.";
    } else {
      temp += "Here is a list of metadata tags:\n";
		  foreach (KeyValuePair<string, string> entry in kvtags) {
			  temp += entry.Key + " = " + entry.Value + "\n";
		  }
    }
		return temp;
	}

  // Creates a human readable version of the kv tags
	public override string ToString() {
		string temp = "";
		foreach (KeyValuePair<string, string> entry in kvtags) {
			temp += entry.Key + ":" + entry.Value + ";";
		}
		return temp;
	}

  // Removes the specified KV tag based on its key
	public void clearKVTag(string key) {
		kvtags.Remove(key);
		kvtagstring = ToString();
	}

  // Gets the value of a KV tag based on its key
	public string getValueAtTag(string key) {
		string value = "";
		if (kvtags.ContainsKey(key)) {
			value = kvtags[key];
		}
		return value;
	}

  // Gets the assembly order of the object
	public Int32 getOrder() {
		updateTags();
		Int32 temp = -1;
		if (kvtags.ContainsKey("order")) {
			Int32.TryParse(kvtags["order"], out temp);
		}
		return temp;
	}
}
