using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class LearningScript : MonoBehaviour {

	Text textField;
	string learningText;
	int maxIndex = 11;
	int currentIndex = 6;
	string LEARNING = "Learning";
	float dotSpeed = 0;

	void Start () {
		textField = GetComponent<Text> ();
		learningText = textField.text;
		ProcessPaths pathProcessor = new ProcessPaths ();
		ProcessTime timeProcessor = new ProcessTime ();
		List<RoomObject> roomWithNewPatrolPoints = pathProcessor.processPaths ();
		timeProcessor.processTimeInRooms (roomWithNewPatrolPoints);
		MapGenerator.completedRooms = roomWithNewPatrolPoints;
		StartCoroutine(load ());
	}

	void Update () {
		if (dotSpeed > 1f) {
			if (currentIndex == maxIndex) {
				learningText = LEARNING;
				currentIndex = 6;
			} else {
				learningText = learningText + ".";
				currentIndex++;
			}
			textField.text = learningText;
			dotSpeed = 0;
		}
		dotSpeed += 2f * Time.deltaTime;
	}

	IEnumerator load(){
		yield return new WaitForSeconds (5);
		SceneManager.LoadScene ("Game");
	}

}
