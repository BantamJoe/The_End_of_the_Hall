using UnityEngine;
using System.Collections;

public class PlayersWinListener : MonoBehaviour {

	public Canvas winCanvas;
	public Transform flashlight;
	public static bool playerWon;

	string WIN_TAG = "Win";

	void Start(){
		playerWon = false;
	}

	void OnTriggerEnter(Collider collider){
		if (collider.gameObject.tag == WIN_TAG) {
			playerWon = true;
			Cursor.visible = true;
			winCanvas.gameObject.SetActive (true);
			Time.timeScale = 0;
			this.GetComponent<FirstPersonController>().enabled = false;
			flashlight.GetComponent<FlashlightMovement>().enabled = false;
			this.GetComponent<AudioSource>().enabled = false;
		}
	}

}
