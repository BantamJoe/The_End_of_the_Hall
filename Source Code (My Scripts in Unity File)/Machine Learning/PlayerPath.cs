using UnityEngine;
using System.Collections.Generic;

public class PlayerPath : MonoBehaviour {

	public static List<List<Vector3>> playerPaths;
	List<Vector3> currentPlayerPath;

	void Start () {
		if (playerPaths == null) {
			playerPaths = new List<List<Vector3>> ();
		}
		currentPlayerPath = new List<Vector3> ();
	}
	
	void Update () {
		currentPlayerPath.Add (this.transform.position);
	}

	public void addPathToStaticList(){
		playerPaths.Add (currentPlayerPath);
	}

	public static void clearPathData(){
		playerPaths = null;
	}

}
