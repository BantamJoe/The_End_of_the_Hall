using UnityEngine;
using System.Collections.Generic;

public class TimeSpentInEachRoom : MonoBehaviour {

	public static List<List<float>> timeInEachRoomPerPlay;
	List<RoomObject> roomsList;
	List<float> timeInEachRoom;
	public int roomThePlayerIsIn;
	static public List<int> roomsPlayerEnters;
	int[] inRoomCountersArray;

	void Start () {
		roomsPlayerEnters = new List<int> ();
		if (timeInEachRoomPerPlay == null) {
			timeInEachRoomPerPlay = new List<List<float>> ();
		}
		roomsList = MapGenerator.completedRooms;
		timeInEachRoom = new List<float> ();
		for (int i = 0; i < roomsList.Count; i++) {
			float time = 0;
			timeInEachRoom.Add (time);
		}
		inRoomCountersArray = initializeArray ();
	}
	
	void Update () {
		for (int i = 0; i < roomsList.Count; i++) {
			if (inRoomsBoundary (roomsList [i])) {
				timeInEachRoom [i] += Time.deltaTime;
				roomThePlayerIsIn = i;
				inRoomCountersArray[i]++;
				if (inRoomCountersArray[i] == 1) {
					roomsPlayerEnters.Add (roomThePlayerIsIn);
				}
			} else {
				roomThePlayerIsIn = -1;
				inRoomCountersArray[i] = 0;
			}
		}
	}

	bool inRoomsBoundary(RoomObject room){
		bool result = false;
		Vector3[,] boundary = room.npc.boundary;
		if (withinXBoundary (boundary) && withinZBoundary (boundary)) {
			result = true;
		}
		return result;
	}

	bool withinXBoundary(Vector3[,] boundary){
		bool result = false;
		float maxX = boundary [0, 0].x;
		float minX = boundary [1, 0].x;
		float currentX = this.transform.position.x;
		if (currentX <= maxX && currentX >= minX) {
			result = true;
		}
		return result;
	}

	bool withinZBoundary(Vector3[,] boundary){
		bool result = false;
		float maxZ = boundary [0, 0].z;
		float minZ = boundary [0, 1].z;
		float currentZ = this.transform.position.z;
		if (currentZ <= maxZ && currentZ >= minZ) {
			result = true;
		}
		return result;
	}
		
	public void addTimesToStaticList(){
		timeInEachRoomPerPlay.Add (timeInEachRoom);
	}

	public static void clearData(){
		timeInEachRoomPerPlay = null;
		roomsPlayerEnters = null;
	}

	int[] initializeArray(){
		int[] result = new int[roomsList.Count];
		for (int i = 0; i < roomsList.Count; i++) {
			result [i] = 0;
		}
		return result;
	}

}
