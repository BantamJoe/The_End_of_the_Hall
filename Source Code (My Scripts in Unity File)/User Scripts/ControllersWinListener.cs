using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ControllersWinListener : MonoBehaviour {

	PlayerPath playerPathScript;
	TimeSpentInEachRoom timeScript;
	PatrolClearer patrolClearer;

	void Start(){
		playerPathScript = GameObject.Find ("Player").GetComponent<PlayerPath> ();
		timeScript = GameObject.Find ("Player").GetComponent<TimeSpentInEachRoom> ();
		patrolClearer = new PatrolClearer ();
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
