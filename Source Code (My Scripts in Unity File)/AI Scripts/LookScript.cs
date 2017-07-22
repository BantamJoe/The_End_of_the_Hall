using UnityEngine;
using System.Collections;

public class LookScript : MonoBehaviour {

	public GameObject player;
	float damping = 2f;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
	}

	// Update is called once per frame
	void FixedUpdate (){
		lookAtPlayer ();
	}

	void lookAtPlayer() {
		Vector3 direction = player.transform.position - this.transform.position;
		direction.y = 0;
		Quaternion rotation = Quaternion.LookRotation(direction);
		transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
	}

}
