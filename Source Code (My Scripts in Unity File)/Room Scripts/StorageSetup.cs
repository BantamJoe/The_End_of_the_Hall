using UnityEngine;
using System.Collections;

public class StorageSetup : StorageDoorGenerator {

	public void setup(RoomObject room){
		setupLight (room.roomsAsGameObject);
		base.generate(adjustPositionsBasedOnSide (room));
	}

	void setupLight(GameObject storage){
		GameObject storageLight = new GameObject ("Storage Light");
		Light lightComp = storageLight.AddComponent<Light> ();
		storageLight.transform.position = new Vector3 (storage.transform.position.x, 4.7f, storage.transform.position.z);
		lightComp.transform.localRotation = Quaternion.Euler(new Vector3(90f, 0, 0));
		lightComp.type = LightType.Point;
		lightComp.spotAngle = 130f;
		lightComp.intensity = 3;
		lightComp.bounceIntensity = 2;
		storageLight.AddComponent<LightFlickering> ();
	}

	RoomObject adjustPositionsBasedOnSide(RoomObject room){
		RoomObject roomWithAdjustments = new RoomObject ();
		roomWithAdjustments = room;
		if (room.onRightSideOfHall) {
			roomWithAdjustments.rightDoorPositionX = -room.rightDoorPositionX;
		} else {
			roomWithAdjustments.leftDoorPositionX = -room.leftDoorPositionX;
			roomWithAdjustments.doorPositionZ = -room.doorPositionZ;
		}
		return roomWithAdjustments;
	}

}
