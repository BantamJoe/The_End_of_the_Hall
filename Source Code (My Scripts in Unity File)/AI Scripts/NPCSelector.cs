using UnityEngine;
using System.Collections;

public class NPCSelector {

	string SKELETON = "skeleton_animated";

	public NPC selectNPC(RoomObject roomNPCIsIN){
		//if there are more than one NPC by the end a random number generator would select which NPC for the given room
		NPC npc = new NPC();
		npc.name = SKELETON;
		AggressionState state = new AggressionState ();
		state.initialize ();
		npc.aggressionState = state;
		npc.boundary = createBoundary (roomNPCIsIN);
		return npc;
	}

	Vector3[,] createBoundary(RoomObject room){
		Vector3[,] boundaries = new Vector3[2, 2];
		float originX = room.roomOrigin.x;
		float originZ = room.roomOrigin.z;
		//Top Left of Room
		boundaries [0, 0] = new Vector3 (originX + room.roomDimensionX, 0, originZ + room.roomDimensionZ/2);
		//Top Right of Room
		boundaries [0, 1] = new Vector3 (originX + room.roomDimensionX, 0, originZ - room.roomDimensionZ/2);
		//Bottom Left of Room
		boundaries [1, 0] = new Vector3 (originX - room.roomDimensionX, 0, originZ + room.roomDimensionZ/2);
		//Bottom Right of Room
		boundaries [1, 1] = new Vector3 (originX - room.roomDimensionX, 0, originZ - room.roomDimensionZ/2);
		adjustZPositionsBasedOnRoom (boundaries, room);
		return boundaries;
	}

	void adjustZPositionsBasedOnRoom(Vector3[,] boundaries, RoomObject room){
		if (room.name.Equals ("storage")) {
			adjustStorage (boundaries);
		} else if (room.name.Equals ("classroom")) {
			adjustClassroom (boundaries);
		} else {
			adjustTemplates (boundaries);
		}
	}

	void adjustStorage(Vector3[,] boundaries){
		float leftPointAdjustments = 1.7f;
		float rightPointAdjustments = -1.7f;
		boundaries [0, 0].z += leftPointAdjustments;
		boundaries [0, 1].z += rightPointAdjustments;
		boundaries [1, 0].z += leftPointAdjustments;
		boundaries [1, 1].z += rightPointAdjustments;
	}

	void adjustClassroom(Vector3[,] boundaries){
		float leftPointAdjustments = 1.2f;
		float rightPointAdjustments = -5f;
		boundaries [0, 0].z += leftPointAdjustments;
		boundaries [0, 1].z += rightPointAdjustments;
		boundaries [1, 0].z += leftPointAdjustments;
		boundaries [1, 1].z += rightPointAdjustments;
	}
		
	void adjustTemplates(Vector3[,] boundaries){
		float leftPointAdjustments = 0.5f;
		float rightPointAdjustments = -0.6f;
		boundaries [0, 0].z += leftPointAdjustments;
		boundaries [0, 1].z += rightPointAdjustments;
		boundaries [1, 0].z += leftPointAdjustments;
		boundaries [1, 1].z += rightPointAdjustments;
	}

}
