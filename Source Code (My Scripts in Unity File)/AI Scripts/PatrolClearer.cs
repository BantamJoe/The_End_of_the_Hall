using UnityEngine;
using System.Collections.Generic;

public class PatrolClearer {

	public void clearAllPatrols(){
		List<GameObject> npcs = getAllNPCGameObjects ();
		for (int x = 0; x < npcs.Count; x++) {
			if (hasFollowScript (npcs [x])) {
				clearFollowPatrol (npcs[x]);
			} else if (hasAttackScript (npcs [x])) {
				clearAttackPatrol (npcs[x]);
			}
		}
	}

	void clearFollowPatrol(GameObject npc){
		FollowScript follow = npc.GetComponent<FollowScript> ();
		follow.clearData ();
	}

	void clearAttackPatrol(GameObject npc){
		AttackScript attack = npc.GetComponent<AttackScript> ();
		attack.clearData ();
	}

	List<GameObject> getAllNPCGameObjects(){
		List<RoomObject> rooms = MapGenerator.completedRooms;
		List<GameObject> npcs = new List<GameObject> ();
		for (int i = 0; i < rooms.Count; i++) {
			npcs.Add(GameObject.Find("skeleton_animated_" + i));
		}
		return npcs;
	}

	bool hasFollowScript(GameObject givenNPC){
		bool result = false;
		FollowScript script = givenNPC.GetComponent<FollowScript> ();
		if (script != null) {
			result = true;
		}
		return result;
	}

	bool hasAttackScript(GameObject givenNPC){
		bool result = false;
		AttackScript script = givenNPC.GetComponent<AttackScript> ();
		if (script != null) {
			result = true;
		}
		return result;
	}

}
