using UnityEngine;
using System.Collections;

public class ClassroomDoorGenerator {

	string DOOR_STRING = "door";
	string DOOR_TAG = "Door";
	float DOOR_UPRIGHT = -90f;

	public void generate(RoomObject room){
		GameObject classroom = room.roomsAsGameObject;
		float zRotation = doorRotationBasedOnSide (room.onRightSideOfHall);
		GameObject classroomDoorRight = GameObject.Instantiate(Resources.Load(DOOR_STRING, typeof(GameObject))) as GameObject;
		classroomDoorRight.transform.position = new Vector3(classroom.transform.position.x + room.rightDoorPositionX, classroom.transform.position.y, classroom.transform.position.z + room.doorPositionZ);
		classroomDoorRight.transform.localEulerAngles = new Vector3(DOOR_UPRIGHT, 0, zRotation);
		setupDoorScripts (classroomDoorRight, room);
		GameObject classroomDoorLeft = GameObject.Instantiate(Resources.Load(DOOR_STRING, typeof(GameObject))) as GameObject;
		classroomDoorLeft.transform.position = new Vector3(classroom.transform.position.x + room.leftDoorPositionX, classroom.transform.position.y, classroom.transform.position.z + room.doorPositionZ);
		classroomDoorLeft.transform.localEulerAngles = new Vector3(DOOR_UPRIGHT, 0, zRotation);
		setupDoorScripts (classroomDoorLeft, room);
	}

	void setupDoorScripts(GameObject door, RoomObject room){
		DoorOpen doorOpenScript = door.AddComponent<DoorOpen> ();
		doorOpenScript.setDoorSide (room.onRightSideOfHall);
		door.tag = DOOR_TAG;
	}

	float doorRotationBasedOnSide(bool rightSide){
		float result = 0;
		if (rightSide) {
			result = 180f;
		}
		return result;
	}

}
