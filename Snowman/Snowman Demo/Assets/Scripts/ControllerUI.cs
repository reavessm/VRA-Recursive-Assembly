﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerUI : MonoBehaviour {

	private SteamVR_TrackedObject trackedObj;
    private GameObject[] lightSource;  // UnityException FindGameObjectWithTag is not allowed to be called from a MonoBehaviour Constructor, moved to 'Start()' -SR	
    private bool lightsDimmed = false;

	private SteamVR_Controller.Device Controller
	{
		get { return SteamVR_Controller.Input((int)trackedObj.index); }
	}

	// Use this for initialization
	void Start () {
        lightSource = GameObject.FindGameObjectsWithTag("Light");  // UnityException FindGameObjectWithTag is not allowed to be called from a MonoBehaviour Constructor, moved to 'Start()' -SR	
    }
	
	// Update is called once per frame
	void Update () {
		if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
		{
			if (!lightsDimmed)
				DimLights();
		}
	}

	void DimLights()
	{
		for (int i = 0; i < lightSource.Length; i++)
		{
			lightSource[i].GetComponent<Light>().intensity = .75F;
		}
	}

	void BrightenLights()
	{
		for (int i = 0; i < lightSource.Length; i++)
		{
			lightSource[i].GetComponent<Light>().intensity = 1;
		}
	}

}
