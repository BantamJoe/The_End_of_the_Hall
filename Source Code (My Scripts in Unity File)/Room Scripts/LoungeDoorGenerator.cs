using UnityEngine;
using System.Collections;

public class LoungeDoorGenerator {

	string DOOR_STRING = "door";
	string DOOR_TAG = "Door";
	float DOOR_UPRIGHT = -90f;

	public void generate(RoomObject room){
		GameObject lounge = room.roomsAsGameObject;
		float zRotation = doorRotationBasedOnSide (room.onRightSideOfHall);
		GameObject loungeDoorRight = GameObject.Instantiate(Resources.Load(DOOR_STRING, typeof(GameObject))) as GameObject;
		loungeDoorRight.transform.position = new Vector3(lounge.transform.position.x + room.rightDoorPositionX, lounge.transform.position.y, lounge.transform.position.z + room.doorPositionZ);
		loungeDoorRight.transform.localEulerAngles = new Vector3(DOOR_UPRIGHT, 0, zRotation);
		setupDoorScripts (loungeDoorRight, room);
		GameObject loungeDoorLeft = GameObject.Instantiate(Resources.Load(DOOR_STRING, typeof(GameObject))) as GameObject;
		loungeDoorLeft.transform.position = new Vector3(lounge.transform.position.x + room.leftDoorPositionX, lounge.transform.position.y, lounge.transform.position.z + room.doorPositionZ);
		loungeDoorLeft.transform.localEulerAngles = new Vector3(DOOR_UPRIGHT, 0, zRotation);
		setupDoorScripts (loungeDoorLeft, room);
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
