using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScript : MonoBehaviour {

	Text textField;
	string loadingText;
	int maxIndex = 11;
	int currentIndex = 6;
	string LOADING = "Loading";
	float dotSpeed = 0;

	void Start () {
		textField = GetComponent<Text> ();
		loadingText = textField.text;
		StartCoroutine(load ());
	}

	void Update () {
		if (dotSpeed > 1f) {
			if (currentIndex == maxIndex) {
				loadingText = LOADING;
				currentIndex = 6;
			} else {
				loadingText = loadingText + ".";
				currentIndex++;
			}
			textField.text = loadingText;
			dotSpeed = 0;
		}
		dotSpeed += 2f * Time.deltaTime;
	}

	IEnumerator load(){
		yield return new WaitForSeconds (5);
		SceneManager.LoadScene ("Game");
	}

}
