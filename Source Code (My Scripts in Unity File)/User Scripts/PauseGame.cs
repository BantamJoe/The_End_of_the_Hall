using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour {

    public Transform canvas;
    public Transform player;
    public Transform flashlight;
	PatrolClearer patrolClearer;

	void Start(){
		patrolClearer = new PatrolClearer ();
	}
		
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(canvas.gameObject.activeInHierarchy == false)
            {
                Cursor.visible = true;
                canvas.gameObject.SetActive(true);
                Time.timeScale = 0;
				player.GetComponent<FirstPersonController>().enabled = false;
                flashlight.GetComponent<FlashlightMovement>().enabled = false;
            }
            else
            {
                Cursor.visible = false;
                canvas.gameObject.SetActive(false);
                Time.timeScale = 1;
				player.GetComponent<FirstPersonController>().enabled = true;
                flashlight.GetComponent<FlashlightMovement>().enabled = true;
            }
        }
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
