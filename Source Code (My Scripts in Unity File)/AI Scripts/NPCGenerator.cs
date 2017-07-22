using UnityEngine;
using System.Collections.Generic;

public class NPCGenerator {

	string SKELETON_FOLDER = "Skeleton";
	string SKELETON = "skeleton_animated";

	public List<RoomObject> generateNPCs(List<RoomObject> roomsWithoutNPCReference){
		List<RoomObject> roomsWithNPCReference = chooseNPCs (roomsWithoutNPCReference);
		createNPCs (roomsWithNPCReference);
		return roomsWithNPCReference;
	}

	public void regenerateNPCs(List<RoomObject> roomsWithNewNPCData){
		createNPCs (roomsWithNewNPCData);
	}

	List<RoomObject> chooseNPCs(List<RoomObject> rooms){
		NPCSelector selector = new NPCSelector ();
		List<RoomObject> roomsWithReference = new List<RoomObject> ();
		for (int i = 0; i < rooms.Count; i++) {
			RoomObject roomObj = new RoomObject ();
			roomObj = rooms [i];
			NPC npcForGivenRoom = selector.selectNPC (roomObj);
			roomObj.npc = npcForGivenRoom;
			roomObj.npcPatrolPoints = new List<GridCell> ();
			roomsWithReference.Add (roomObj);
		}
		return roomsWithReference;
	}
		
	void createNPCs(List<RoomObject> rooms){
		for (int i = 0; i < rooms.Count; i++) {
			GameObject npc = GameObject.Instantiate(Resources.Load(getPathToNPC(rooms[i].npc.name), typeof(GameObject))) as GameObject;
			npc.name = SKELETON + "_" + i;
			setupGivenNPC (npc, rooms [i].npc, i);
			adjustSpawnPositionBasedOnRoom (rooms[i], npc);
		}
	}

	string getPathToNPC(string npcName){
		string path = "";
		if (npcName.Equals (SKELETON)) {
			path = SKELETON_FOLDER + "/" + SKELETON;
		}
		return path;
	}

	void setupGivenNPC (GameObject npc, NPC npcObject, int roomIndex){
		Animator animator = npc.GetComponent<Animator> ();
		animator.runtimeAnimatorController = Resources.Load(getPathToAnimatorController(npcObject)) as RuntimeAnimatorController;
		npc.transform.localScale = new Vector3 (3f, 3f, 3f);
		addComponentsToNPC (npc, npcObject, roomIndex);
		npc.tag = "Enemy";
	}

	string getPathToAnimatorController(NPC npc){
		string path = "";
		AggressionState npcAggresstionState = npc.aggressionState;
		if (npc.name.Equals (SKELETON)) {
			if (npcAggresstionState.isLook ()) {
				path = SKELETON_FOLDER + "/" + "skeletonLookController";
			} else if (npcAggresstionState.isFollow ()) {
				path = SKELETON_FOLDER + "/" + "skeletonFollowController";
			} else if (npcAggresstionState.isAttack ()) {
				path = SKELETON_FOLDER + "/" + "skeletonAttackController";
			} else {
				Debug.Log ("Could not find animator controller path based on aggression state for Skeleton");
			}
		}
		return path;
	}

	void addComponentsToNPC(GameObject npc, NPC npcObject, int roomIndex){
		addRigidBody (npc);
		addCollider (npc);
		addCorrespondingScript (npc, npcObject, roomIndex);
		addAudioSource (npc);
	}

	void addRigidBody(GameObject npc){
		Rigidbody npcRigidBody = npc.AddComponent<Rigidbody> ();
		npcRigidBody.useGravity = true;
		npcRigidBody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
	}

	void addCollider(GameObject npc){
		CapsuleCollider collider = npc.AddComponent<CapsuleCollider> ();
		collider.center = new Vector3 (0, 0.16f, 0);
		collider.radius = 0.2f;
		collider.height = 0.82f;
	}

	void addCorrespondingScript(GameObject npc, NPC npcObject, int roomIndex){
		AggressionState npcAggressionState = npcObject.aggressionState;
		if (npcAggressionState.isLook ()) {
			npc.AddComponent<LookScript> ();
		} else if (npcAggressionState.isFollow ()) {
			FollowScript follow = npc.AddComponent<FollowScript> ();
			follow.roomIndex = roomIndex;
		} else if (npcAggressionState.isAttack ()) {
			AttackScript attack = npc.AddComponent<AttackScript> ();
			attack.roomIndex = roomIndex;
		} else {
			Debug.Log ("NPC's current state has no script association");
		}
	}

	void addAudioSource(GameObject npc){
		AudioSource source = npc.AddComponent<AudioSource> ();
		source.clip = Resources.Load ("hitSound") as AudioClip;
	}

	void adjustSpawnPositionBasedOnRoom(RoomObject room, GameObject npc){
		float zDisplacement = getZDisplacement (room);
		switch (room.name) {
			case "office":
				npc.transform.position = new Vector3 (room.positionPointX + 5f, 0.7f, zDisplacement);
				break;
			case "classroom":
				npc.transform.position = new Vector3 (room.positionPointX + 7f, 0.7f, zDisplacement);
				break;
			case "loungeAndKitchen":
				npc.transform.position = new Vector3 (room.positionPointX + 5f, 0.7f, zDisplacement);
				break;
			case "bedroom":
				npc.transform.position = new Vector3 (room.positionPointX, 0.7f, zDisplacement);
				break;
			case "storage":
				npc.transform.position = new Vector3 (room.positionPointX + 5f, 0.7f, zDisplacement);
				break;
			default:
				npc.transform.position = new Vector3 (room.positionPointX, 0.7f, zDisplacement);
				break;
		}
	}

	float getZDisplacement(RoomObject room){
		float result;
		if (room.onRightSideOfHall) {
			result = -room.roomDimensionZ;
		} else {
			result = room.roomDimensionZ;
		}
		return result;
	}
		
}
