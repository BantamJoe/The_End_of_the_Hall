using UnityEngine;
using System.Collections;

public class StorageDoorGenerator {

	string DOOR_STRING = "door";
	string DOOR_TAG = "Door";
	float DOOR_UPRIGHT = -90f;

	public void generate(RoomObject room){
		GameObject storage = room.roomsAsGameObject;
		float zRotation = doorRotationBasedOnSide (room.onRightSideOfHall);
		GameObject storageDoorRight = GameObject.Instantiate(Resources.Load(DOOR_STRING, typeof(GameObject))) as GameObject;
		storageDoorRight.transform.position = new Vector3(storage.transform.position.x + room.rightDoorPositionX, storage.transform.position.y, storage.transform.position.z + room.doorPositionZ);
		storageDoorRight.transform.localEulerAngles = new Vector3(DOOR_UPRIGHT, 0, zRotation);
		setupDoorScripts (storageDoorRight, room);
		GameObject storageDoorLeft = GameObject.Instantiate(Resources.Load(DOOR_STRING, typeof(GameObject))) as GameObject;
		storageDoorLeft.transform.position = new Vector3(storage.transform.position.x + room.leftDoorPositionX, storage.transform.position.y, storage.transform.position.z + room.doorPositionZ);
		storageDoorLeft.transform.localEulerAngles = new Vector3(DOOR_UPRIGHT, 0, zRotation);
		setupDoorScripts (storageDoorLeft, room);
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
