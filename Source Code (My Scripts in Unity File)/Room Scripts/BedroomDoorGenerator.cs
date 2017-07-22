using UnityEngine;
using System.Collections;

public class BedroomDoorGenerator {

	string DOOR_STRING = "door";
	string DOOR_TAG = "Door";
	float DOOR_UPRIGHT = -90f;

	public void generate(RoomObject room){
		GameObject bedroom = room.roomsAsGameObject;
		float zRotation = doorRotationBasedOnSide (room.onRightSideOfHall);
		GameObject bedroomDoorRight = GameObject.Instantiate(Resources.Load(DOOR_STRING, typeof(GameObject))) as GameObject;
		bedroomDoorRight.transform.position = new Vector3 (bedroom.transform.position.x + room.rightDoorPositionX, bedroom.transform.position.y, bedroom.transform.position.z + room.doorPositionZ);
		bedroomDoorRight.transform.localEulerAngles = new Vector3(DOOR_UPRIGHT, 0, zRotation);
		setupDoorScripts (bedroomDoorRight, room);
		GameObject bedroomDoorLeft = GameObject.Instantiate(Resources.Load(DOOR_STRING, typeof(GameObject))) as GameObject;
		bedroomDoorLeft.transform.position = new Vector3 (bedroom.transform.position.x + room.leftDoorPositionX, bedroom.transform.position.y, bedroom.transform.position.z + room.doorPositionZ);
		bedroomDoorLeft.transform.localEulerAngles = new Vector3(DOOR_UPRIGHT, 0, zRotation);
		setupDoorScripts (bedroomDoorLeft, room);
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
