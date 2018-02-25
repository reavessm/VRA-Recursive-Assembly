using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Linq;
using System.IO;
using System;
using System.Linq;

public class XMLoader : MonoBehaviour {

	public String xmlFilePath = Directory.GetCurrentDirectory() + "\\Assets\\Resources\\world.xml";
	private XDocument xmlDOM;

	// Use this for initialization
  void Start ()
	{
		readXMLFile(xmlFilePath);
		outToLog(getAllParts());
	}

	// Update is called once per frame
	void Update () {

	}

  public XMLoader() {} // default constructor

  public XMLoader(String location) {
    readXMLFile(location);
  }

	private void outToLog(IEnumerable<XElement> e) {
		Debug.Log("Printing Out The Element List...");
		foreach (XElement element in e) {
			Debug.Log("Element Name: " + element.Attribute("name") + " ID: " + element.Attribute("id") + "\n");
		}
	}

	private IEnumerable<XElement> getAllScenes() {
		IEnumerable<XElement> temp = from scene in xmlDOM.Root.Elements() select scene;
		return temp;
	}

	private IEnumerable<XElement> getAllGroups() {
		IEnumerable<XElement> temp = from groups in xmlDOM.Root.Descendants("scene").Elements() select groups;
		return temp;
	}

	private IEnumerable<XElement> getAllAssemblies() {
		IEnumerable<XElement> temp = from assemblies in xmlDOM.Root.Descendants("scene").Descendants("group").Elements() select assemblies;
		return temp;
	}

	private IEnumerable<XElement> getAllParts() {
		IEnumerable<XElement> temp = from part in xmlDOM.Root.Descendants("scene").Descendants("group").Descendants("assembly").Elements() select part;
		return temp;
	}

	private void readXMLFile(String location)
	{
		xmlDOM = XDocument.Load(location);
	}
}
