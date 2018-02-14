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
		readXMLFile();
		IEnumerable<XElement> elements = from el in xmlDOM.Root.Elements("scene") select el;
		foreach (XElement el in elements)
		{
			Debug.Log(el.FirstAttribute);
		}
		//Debug.Log(xmlDOM.Elements("group1").ToString());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void readXMLFile()
	{
		xmlDOM = XDocument.Load(xmlFilePath);
	}
}
