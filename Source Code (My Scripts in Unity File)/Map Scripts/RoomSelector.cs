using UnityEngine;
using System.Collections.Generic;

public class RoomSelector {

	RoomData roomDataScript = new RoomData ();
	System.Random numberGenerator = new System.Random();

	public List<RoomObject> selectRooms() {
		int numberOfRooms = PlayerPrefs.GetInt ("Number of Rooms");//numberGenerator.Next (2, 6);
		List<RoomObject> selectedRooms = selectRooms (numberOfRooms);
		List<RoomObject> alternatingRooms = alternateRoomSides (selectedRooms);
		List<RoomObject> finishedRooms = setOriginsOfRooms (alternatingRooms);
		return finishedRooms;
	}
		
	List<RoomObject> selectRooms(int numberOfRooms){
		List<RoomObject> roomData = roomDataScript.getRoomData ();
		List<RoomObject> roomsSelected = new List<RoomObject> ();
		for (int i = 0; i < numberOfRooms; i++) {
			int randomID = numberGenerator.Next (1, 6);
			roomsSelected.Add (getRoomObjectByID(randomID, roomData));
		}
		return roomsSelected;
	}

	RoomObject getRoomObjectByID(int id, List<RoomObject> roomData){
		for (int i = 0; i < roomData.Count; i++) {
			if (roomData [i].ID == id) {
				return roomData [i];
			}
		}
		return new RoomObject ();
	}

	List<RoomObject> setOriginsOfRooms(List<RoomObject> rooms){
		List<RoomObject> roomsWithXPosition = setXPositionsOfRooms (rooms);
		List<RoomObject> finishedRooms = new List<RoomObject> ();
		for (int i = 0; i < roomsWithXPosition.Count; i++) {
			RoomObject currentRoom = roomsWithXPosition [i];
			float zDisplacement;
			if (currentRoom.onRightSideOfHall) {
				zDisplacement = -currentRoom.roomDimensionZ;
			} else {
				zDisplacement = currentRoom.roomDimensionZ;
			}
			currentRoom.roomOrigin = new Vector3 (currentRoom.positionPointX, 0, zDisplacement);
			finishedRooms.Add (currentRoom);
		}
		return finishedRooms;
	}

	// There is something off/wrong with Storages dimensions, so that is the one room we
	// need to address exceptions for. 
	List<RoomObject> setXPositionsOfRooms(List<RoomObject> rooms){
		float previousPoint = 0;
		List<RoomObject> finishedRooms = new List<RoomObject> ();
		bool previousRoomWasStorage = false;
		for (int i = 0; i < rooms.Count; i++) {
			RoomObject currentRoom = rooms [i];
			if (i == 0) {
				currentRoom.positionPointX = currentRoom.roomDimensionX;
				if (currentRoom.name.Equals (roomDataScript.STORAGE)) {
					previousRoomWasStorage = true;
				}
			} else if (currentRoom.name.Equals (roomDataScript.STORAGE)) {
				if (previousRoomWasStorage) {
					currentRoom.positionPointX = previousPoint + (currentRoom.roomDimensionX * 2);
				} else {
					currentRoom.positionPointX = previousPoint + (currentRoom.roomDimensionX * 2) - 12.5f;
				}
				previousRoomWasStorage = true;
			} else if (previousRoomWasStorage) {
				currentRoom.positionPointX = previousPoint + (currentRoom.roomDimensionX * 2) + 11.5f;
				if (!currentRoom.name.Equals (roomDataScript.STORAGE)) {
					previousRoomWasStorage = false;
				}
			}else{
				currentRoom.positionPointX = previousPoint + (currentRoom.roomDimensionX * 2);
			}
			previousPoint = currentRoom.positionPointX;
			finishedRooms.Add (currentRoom);
		}
		return finishedRooms;
	}

	List<RoomObject> alternateRoomSides(List<RoomObject> rooms){
		float rotation = 90f;
		List<RoomObject> finishedRooms = new List<RoomObject> ();
		for (int i = 0; i < rooms.Count; i++) {
			RoomObject currentRoom = rooms [i];
			if(i % 2 == 0){
				currentRoom.onRightSideOfHall = true;
				currentRoom.roomRotation = rotation;
			}
			else{
				currentRoom.onRightSideOfHall = false;
				currentRoom.roomRotation = -rotation;
			}
			finishedRooms.Add (currentRoom);
		}
		return finishedRooms;
	}

}
