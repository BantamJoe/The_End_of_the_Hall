using UnityEngine;
using System.Collections;

public class WinControllerGenerator {

	public void generateController(Vector3 endPoint){
		GameObject winController = GameObject.Find ("WinController");
		winController.tag = "Win";
		winController.transform.position = endPoint;
		BoxCollider winCollider = winController.AddComponent<BoxCollider> ();
		winCollider.isTrigger = true;
		winCollider.size = new Vector3 (5, 5, 10);
		winCollider.center = new Vector3 (0, 2.5f, 0);
	}
		
}
