using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomData {

	/*	
	*	ID of each room
	* 	1 = Classroom
	*	2 = Office
	*	3 = Lounge and Kitchen
	*	4 = Split Room
	*	5 = Storage
	*/

	public string OFFICE = "office";
	public string CLASSROOM = "classroom";
	public string LOUNGE_AND_KITCHEN = "loungeAndKitchen";
	public string BEDROOM = "bedroom";
	public string STORAGE = "storage";

	public List<RoomObject> getRoomData () {
		RoomObject office = setupOffice ();
		RoomObject classroom = setupClassroom ();
		RoomObject loungeAndKitchen = setupLoungeAndKitchen ();
		RoomObject bedroomRoom = setupBedroom ();
		RoomObject storage = setupStorage ();
		List<RoomObject> rooms = new List<RoomObject> ();
		rooms.Add (office);
		rooms.Add (classroom);
		rooms.Add (loungeAndKitchen);
		rooms.Add (bedroomRoom);
		rooms.Add (storage);
		return rooms;
	}

	RoomObject setupClassroom(){
		RoomObject classroom = new RoomObject();
		classroom.ID = 1;
		classroom.name = CLASSROOM;
		classroom.roomDimensionX = 12f;
		classroom.roomDimensionZ = 13.3f;
		classroom.leftDoorPositionX = 12f;
		classroom.rightDoorPositionX = 10.5f;
		classroom.doorPositionZ = 8.2f;
		classroom.scaleOfRoomX = 2.5f;
		return classroom;
	}

	RoomObject setupOffice(){
		RoomObject office = new RoomObject();
		office.ID = 2;
		office.name = OFFICE;
		office.roomDimensionX = 14f;
		office.roomDimensionZ = 12.3f;
		office.leftDoorPositionX = 14f;
		office.rightDoorPositionX = 12.5f;
		office.doorPositionZ = 7.2f;
		office.scaleOfRoomX = 3f;
		return office;
	}

	RoomObject setupLoungeAndKitchen(){
		RoomObject loungeAndKitchen = new RoomObject();
		loungeAndKitchen.ID = 3;
		loungeAndKitchen.name = LOUNGE_AND_KITCHEN;
		loungeAndKitchen.roomDimensionX = 14f;
		loungeAndKitchen.roomDimensionZ = 12.3f;
		loungeAndKitchen.leftDoorPositionX = 14f;
		loungeAndKitchen.rightDoorPositionX = 12.5f;
		loungeAndKitchen.doorPositionZ = 7.2f;
		loungeAndKitchen.scaleOfRoomX = 3f;
		return loungeAndKitchen;
	}

	RoomObject setupBedroom(){
		RoomObject bedroom = new RoomObject();
		bedroom.ID = 4;
		bedroom.name = BEDROOM;
		bedroom.roomDimensionX = 14f;
		bedroom.roomDimensionZ = 12.3f;
		bedroom.leftDoorPositionX = 14f;
		bedroom.rightDoorPositionX = 12.5f;
		bedroom.doorPositionZ = 7.2f;
		bedroom.scaleOfRoomX = 3f;
		return bedroom;
	}

	RoomObject setupStorage(){
		RoomObject storage = new RoomObject();
		storage.ID = 5;
		storage.name = STORAGE;
		storage.roomDimensionX = 25f;
		storage.roomDimensionZ = 15.3f;
		storage.leftDoorPositionX = 24.98f;
		storage.rightDoorPositionX = 23.47f;
		storage.doorPositionZ = 10.2f;
		storage.scaleOfRoomX = 4.97f;
		return storage;
	}

}