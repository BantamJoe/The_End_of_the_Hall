using UnityEngine;
using System.Collections;

public class OfficeSetup : OfficeDoorGenerator {

	public void setup(RoomObject room){
		setupLight (room);
		base.generate(adjustPositionsBasedOnSide(room));
	}

	void setupLight(RoomObject room){

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
