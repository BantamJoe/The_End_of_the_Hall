using UnityEngine;
using System.Collections;

public class OfficeDoorGenerator {

	string DOOR_STRING = "door";
	string DOOR_TAG = "Door";
	float DOOR_UPRIGHT = -90f;

	public void generate(RoomObject room){
		GameObject office = room.roomsAsGameObject;
		float zRotation = doorRotationBasedOnSide (room.onRightSideOfHall);
		GameObject officeDoorRight = GameObject.Instantiate(Resources.Load(DOOR_STRING, typeof(GameObject))) as GameObject;
		officeDoorRight.transform.position = new Vector3(office.transform.position.x + room.rightDoorPositionX, office.transform.position.y, office.transform.position.z + room.doorPositionZ);
		officeDoorRight.transform.localEulerAngles = new Vector3(DOOR_UPRIGHT, 0, zRotation);
		setupDoorScripts (officeDoorRight, room);
		GameObject officeDoorLeft = GameObject.Instantiate(Resources.Load(DOOR_STRING, typeof(GameObject))) as GameObject;
		officeDoorLeft.transform.position = new Vector3(office.transform.position.x + room.leftDoorPositionX, office.transform.position.y, office.transform.position.z + room.doorPositionZ);
		officeDoorLeft.transform.localEulerAngles = new Vector3(DOOR_UPRIGHT, 0, zRotation);
		setupDoorScripts (officeDoorLeft, room);
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
