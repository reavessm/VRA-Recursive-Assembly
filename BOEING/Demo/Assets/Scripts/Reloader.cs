using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reloader : MonoBehaviour {

    public SceneSetter sceneDirector;

	// Use this for initialization
	void Awake () {
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    
    public void Reloadify()
    {
        sceneDirector.Restart();
    }

    public void ReloadifyAndAutoassemble()
    {
        Invoke("sceneDirector.AutoAssemble()", 5);
        sceneDirector.Restart();
    }
}
