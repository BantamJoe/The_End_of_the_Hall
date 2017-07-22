using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DeathListener : MonoBehaviour {

	public Transform canvas;
	public GameObject player;
	public Transform flashlight;

	PlayerPath playerPathScript;
	TimeSpentInEachRoom timeScript;
	PatrolClearer patrolClearer;

	void Start(){
		playerPathScript = player.GetComponent<PlayerPath> ();
		timeScript = player.GetComponent<TimeSpentInEachRoom> ();
		patrolClearer = new PatrolClearer ();
	}

	public void playerDied(){
		Cursor.visible = true;
		canvas.gameObject.SetActive (true);
		Time.timeScale = 0;
		player.GetComponent<FirstPersonController>().enabled = false;
		flashlight.GetComponent<FlashlightMovement>().enabled = false;
	}

	public void restart(){
		Time.timeScale = 1;
		playerPathScript.addPathToStaticList ();
		timeScript.addTimesToStaticList ();
		SceneManager.LoadScene ("Learning");
	}

	public void quit(){
		clearMemoryOfRoomsAndMachineLearning ();
		Time.timeScale = 1;
		SceneManager.LoadScene ("StartMenu");
	}

	void clearMemoryOfRoomsAndMachineLearning(){
		patrolClearer.clearAllPatrols ();
		MapGenerator.clearRoomMemory ();
		PlayerPath.clearPathData ();
		TimeSpentInEachRoom.clearData ();
	}

}
