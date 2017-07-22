using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class StartMenu : MonoBehaviour {

	public Canvas mainCanvas;
	public Canvas numberCanvas;
	public Canvas instructionCanvas;

	public void LoadGame(){
		mainCanvas.gameObject.SetActive (false);
		numberCanvas.gameObject.SetActive (true);
	}

	public void LoadInstructions(){
		mainCanvas.gameObject.SetActive (false);
		instructionCanvas.gameObject.SetActive (true);
	}

	public void goBackToStart(){
		instructionCanvas.gameObject.SetActive (false);
		mainCanvas.gameObject.SetActive (true);
	}

	public void ExitGame(){
		Application.Quit ();
	}
}
