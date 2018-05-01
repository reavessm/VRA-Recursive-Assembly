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
    
    // Restart the scene 
    public void Reloadify()
    {
        sceneDirector.Restart();
    }

    // Restart then autoassemble scene
    public void ReloadifyAndAutoassemble()
    {
        Invoke("sceneDirector.AutoAssemble()", 5);
        sceneDirector.Restart();
    }
}
