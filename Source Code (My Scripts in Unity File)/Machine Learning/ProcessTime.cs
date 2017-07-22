using UnityEngine;
using System.Collections.Generic;

public class ProcessTime {

	List<RoomObject> roomReferences;
	List<List<float>> timesInEachRoomByGame;
	int[] numberOfTimesPlayerDidntReachRoom;


	public void processTimeInRooms(List<RoomObject> rooms){
		roomReferences = rooms;
		timesInEachRoomByGame = TimeSpentInEachRoom.timeInEachRoomPerPlay;
		numberOfTimesPlayerDidntReachRoom = initializeArray ();
		analyzeTimes ();
	}

	void analyzeTimes(){
		Dictionary<int, float> meanTimeSpentInEachRoom = getMeanTimeSpentInEachRoom ();
		for (int room = 0; room < roomReferences.Count; room++) {
			Debug.Log ("NPC: " + room);
			//If Player 1 increase all aggression states
			if (PlayersWinListener.playerWon) {
				Debug.Log ("Player Won: Inc Aggression");
				roomReferences [room].npc.aggressionState.increaseAggression ();
			}
			//Mean time spent in the given room is less than 30 seconds and the player died in that room
			else if (meanTimeSpentInEachRoom [room] < 30f && PlayerHealth.playerWasKilled && PlayerHealth.roomPlayerDiedIn == room) {
				Debug.Log ("Player died in room in less than 20 secs: Dec Aggression");
				roomReferences [room].npc.aggressionState.decreaseAggression ();
			}
			//Mean time spent in given room is less than 20 seconds
			if (meanTimeSpentInEachRoom [room] < 20f) {
				Debug.Log ("Player Spent less than 20 secs in room: Inc Aggression");
				roomReferences [room].npc.aggressionState.increaseAggression ();
			}
			//Mean time spent in the given room is less than 1 minute.
			else if (meanTimeSpentInEachRoom [room] > 60f) {
				Debug.Log ("Player spent more than a min in the room: Inc Aggression");
				roomReferences [room].npc.aggressionState.increaseAggression ();
			}
			if (playerEntersSameRoomMultipleTimes (room)) {
				Debug.Log ("Player entered room multiple times");
				roomReferences [room].npc.aggressionState.increaseAggression ();
			}
		}
	}

	Dictionary<int, float> getMeanTimeSpentInEachRoom(){
		Dictionary<int, float> meanTimeSpentInEachRoom = getTotalTimeSpentInEachRoom ();
		for (int room = 0; room < roomReferences.Count; room++) {
			float totalTime = meanTimeSpentInEachRoom [room];
			meanTimeSpentInEachRoom [room] = totalTime / (timesInEachRoomByGame [0].Count - numberOfTimesPlayerDidntReachRoom[room]);
		}
		return meanTimeSpentInEachRoom;
	}

	Dictionary<int, float> getTotalTimeSpentInEachRoom(){
		Dictionary<int, float> totalTimesPerRoom = initializeTotalTimeDictionary();
		for (int game = 0; game < timesInEachRoomByGame.Count; game++) {
			for (int room = 0; room < roomReferences.Count; room++) {
				float prevTime= totalTimesPerRoom [room];
				if (timesInEachRoomByGame [game] [room] == 0) {
					numberOfTimesPlayerDidntReachRoom [room]++;
				} else {
					totalTimesPerRoom [room] = prevTime + timesInEachRoomByGame [game] [room];
				}
			}
		}
		return totalTimesPerRoom;
	}

	Dictionary<int, float> initializeTotalTimeDictionary(){
		Dictionary<int, float> result = new Dictionary<int, float> ();
		for (int i = 0; i < timesInEachRoomByGame [0].Count; i++) {
			result [i] = 0;
		}
		return result;
	}

	bool playerEntersSameRoomMultipleTimes(int roomIndex){
		bool result = false;
		int appearanceCounter = 0;
		List<int> roomsPlayerEntered = TimeSpentInEachRoom.roomsPlayerEnters;
		if (roomsPlayerEntered.Contains (roomIndex)) {
			for (int index = 0; index < roomsPlayerEntered.Count; index++) {
				if (roomsPlayerEntered [index] == roomIndex) {
					appearanceCounter++;
				}
			}
		}
		if (appearanceCounter >= 2) {
			result = true;
		}
		return result;
	}

	int[] initializeArray(){
		int[] result = new int[roomReferences.Count];
		for (int i = 0; i < roomReferences.Count; i++) {
			result [i] = 0;
		}
		return result;
	}

}
