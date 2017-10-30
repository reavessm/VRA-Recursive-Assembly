using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour {

	public void StartTheGame() {
		SceneManager.LoadScene ("rollaball++");
	}

	public void WinTheGame() {
		SceneManager.LoadScene ("endgame");
	}

	public void YouLose() {
		SceneManager.LoadScene ("youlose");
	}

	public void EndTheGame() {
		Application.Quit ();
	}
}
