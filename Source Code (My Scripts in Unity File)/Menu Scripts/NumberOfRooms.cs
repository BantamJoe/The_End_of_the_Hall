using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NumberOfRooms : MonoBehaviour {

	public Canvas mainCanvas;
	public InputField inputField;
	public Canvas thisCanvas;
	public GameObject errorPanel;
		
	public void getInput(string input){
		int count = 0;
		if (System.Int32.TryParse (input, out count)) {
			PlayerPrefs.SetInt ("Number of Rooms", count);
			SceneManager.LoadScene ("Loading");
		} else {
			errorPanel.SetActive (true);
		}
	}

	public void start(){
		getInput (inputField.text);
	}

	public void backToStart(){
		thisCanvas.gameObject.SetActive (false);
		mainCanvas.gameObject.SetActive (true);
	}

	public void errorScreenOkPressed(){
		errorPanel.SetActive (false);
	}

}
