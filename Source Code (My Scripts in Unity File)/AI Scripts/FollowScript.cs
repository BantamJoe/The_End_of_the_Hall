using UnityEngine;
using System.Collections.Generic;

public class FollowScript : MonoBehaviour {

	public float enemyLookDistance = 10f;
	public float enemyPursueDistance = 1.2f;
	public float enemyRunSpeed = 0.04f;
	public float enemyWalkSpeed = 0.02f;
	public int roomIndex = -1;
	public GameObject player;
	float damping = 2f;
	Animator anim;
	Rigidbody rigidBody;
	bool playerSeen;
	NPC npcReference;
	Vector3[,] boundariesForNPC;
	Vector3 nextPoint;
	bool npcReachedNextPatrolPoint;
	float distanceNPCIsFromPatrolPoint;
	float prevDistanceNPCIsFromPatrolPoint;
	int npcStuckCounter;
	int npcIdleCounter;
	static List<GridCell> patrolCells;

	string IDLE = "isIdle";
	string WALKING = "isWalking";
	string RUNNING = "isRunning";

	// Use this for initialization
	void Start () {
		if (patrolCells == null) {
			patrolCells = new List<GridCell> ();
		}
		RoomObject roomNPCisIn = MapGenerator.completedRooms[roomIndex];
		patrolCells = roomNPCisIn.npcPatrolPoints;
		anim = GetComponent<Animator> ();
		rigidBody = GetComponent<Rigidbody> ();
		player = GameObject.Find ("Player");
		playerSeen = false;
		npcReference = GameObject.Find ("MapGeneratorObject").GetComponent<MapGenerator> ().getNPCReferenceFromRoom (roomIndex);
		boundariesForNPC = npcReference.boundary;
		npcReachedNextPatrolPoint = true;
		npcStuckCounter = 0;
		npcIdleCounter = 0;
	}

	// Update is called once per frame
	void FixedUpdate (){
		float sightAngle = Vector3.Angle (player.transform.position - this.transform.position, this.transform.forward);
		float distance = Vector3.Distance (player.transform.position, this.transform.position);
		if (distance < enemyLookDistance && (sightAngle < 60 || playerSeen)) {
			playerSeen = true;
			anim.SetBool (IDLE, false);
			lookAtPlayer ();
			if (distance > enemyPursueDistance) {
				setMovingAnimation (false, true);
				run ();
			} else {
				setMovingAnimation (false, false);
				anim.SetBool (IDLE, true);
			}
		} else {
			if (npcReachedNextPatrolPoint && npcIdleCounter < 10) {
				anim.SetBool (IDLE, true);
				npcIdleCounter++;
			} else {
				npcIdleCounter = 0;
				patrol ();
			}
			playerSeen = false;
		}
	}

	void lookAtPlayer() {
		Vector3 direction = player.transform.position - this.transform.position;
		direction.y = 0;
		Quaternion rotation = Quaternion.LookRotation(direction);
		transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
	}

	void run(){
		rigidBody.MovePosition (this.transform.position + this.transform.forward * (enemyRunSpeed * damping));
	}

	void patrol(){
		if (npcReachedNextPatrolPoint) {
			nextPoint = chooseNextPatrolPoint();
		}
		npcReachedNextPatrolPoint = false;
		lookAtNextPoint (nextPoint);
		walk ();
		if (distanceNPCIsFromPatrolPoint < 1f) {
			npcReachedNextPatrolPoint = true;
		}
		handleNPCGettingStuck ();
	}

	Vector3 chooseNextPatrolPoint(){
		npcStuckCounter = 0;
		int randomNumber = Random.Range (0, 100);
		float xPoint;
		float zPoint;
		if (randomNumber % 2 == 0 || patrolCells.Count < 1) {
			xPoint = Random.Range (boundariesForNPC[1,0].x + 0.1f, boundariesForNPC[0,0].x);
			zPoint = Random.Range (boundariesForNPC[0,1].z + 0.1f, boundariesForNPC[0,0].z);
		} else {
			int randomIndex = Random.Range (0, patrolCells.Count - 1);
			GridCell cellSelected = patrolCells[randomIndex];
			xPoint = Random.Range (cellSelected.cell[1,0].x + 0.1f, cellSelected.cell[0,0].x);
			zPoint = Random.Range (cellSelected.cell[0,1].z + 0.1f, cellSelected.cell[0,0].z);
		}
		return new Vector3 (xPoint, 0, zPoint);
	}

	void lookAtNextPoint(Vector3 point){
		Vector3 direction = point - this.transform.position;
		direction.y = 0;
		Quaternion rotation = Quaternion.LookRotation(direction);
		transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
	}

	void walk(){
		setMovingAnimation (true, false);
		prevDistanceNPCIsFromPatrolPoint = distanceNPCIsFromPatrolPoint;
		rigidBody.MovePosition (this.transform.position + this.transform.forward * (enemyWalkSpeed * damping));
		distanceNPCIsFromPatrolPoint = Vector3.Distance (this.transform.position, nextPoint);
	}

	void handleNPCGettingStuck(){
		if (System.Math.Abs(distanceNPCIsFromPatrolPoint - prevDistanceNPCIsFromPatrolPoint) < .01f) {
			npcStuckCounter++;
		}
		if (npcStuckCounter == 5) {
			npcStuckCounter = 0;
			npcReachedNextPatrolPoint = true;
		}
	}

	void setMovingAnimation(bool walking, bool running){
		anim.SetBool (RUNNING, running);
		anim.SetBool (WALKING, walking);
	}

	public void addPatrolCell(GridCell cell){
		Debug.Log ("Cell addded!");
		patrolCells.Add (cell);
	}

	public void resetPatrolCells(){
		patrolCells = new List<GridCell> ();
	}

	public void clearData(){
		patrolCells = null;
	}

}
