using UnityEngine;
using System.Collections;

public class GridCell {

	public Vector3[,] cell { get; set; }

	//The Vector3 returned is the bottom left corner, and will be used to
	//calculate the next cell
	//If the last possible cell for the given room is created then the method
	//returns a Vector3 with values (0,0,0);
	public Vector3 calculateAndSetCell(Vector3 bottomRightBoundary, RoomObject room){
		cell = new Vector3[2, 2];
		float topPointsX = getTopPointsXValue (bottomRightBoundary, room);
		float leftPointsZ = getLeftPointsZValue (bottomRightBoundary, room);
		//topLeft
		cell[0, 0] = new Vector3(topPointsX, 0, leftPointsZ);
		//topRight
		cell[0, 1] = new Vector3(topPointsX, 0, bottomRightBoundary.z);
		//bottomLeft
		Vector3 bottomLeft = new Vector3(bottomRightBoundary.x, 0, leftPointsZ);
		cell [1, 0] = bottomLeft;
		//bottomRight
		cell[1,1] = bottomRightBoundary;
		if(vectorsEqual(cell[0,0], room.npc.boundary[0,0])){
			bottomLeft = new Vector3 (0, 0, 0);
		}
		return bottomLeft;
	}

	float getTopPointsXValue(Vector3 bottomRightBoundary, RoomObject room){
		float result = bottomRightBoundary.x + 5;
		if (result > room.npc.boundary[0,0].x) {
			result = room.npc.boundary [0, 0].x;
		}
		return result;
	}

	float getLeftPointsZValue(Vector3 bottomRightBoundary, RoomObject room){
		float result = bottomRightBoundary.z + 5;
		if (result > room.npc.boundary[0,0].z) {
			result = room.npc.boundary [0, 0].z;
		}
		return result;
	}

	bool vectorsEqual(Vector3 topLeft, Vector3 topLeftBoundary){
		return Vector3.SqrMagnitude (topLeft - topLeftBoundary) < 0.0001;
	}

	public bool pathPointIsWithinGridCell(Vector3 pathPoint){
		bool result = false;
		if(withinXBoundary(pathPoint) && withinZBoundary(pathPoint)){
			result = true;
		}
		return result;
	}

	bool withinXBoundary(Vector3 pathPoint){
		bool result = false;
		float maxX = cell [0, 0].x;
		float minX = cell [1, 0].x;
		if (pathPoint.x <= maxX && pathPoint.x >= minX) {
			result = true;
		}
		return result;
	}

	bool withinZBoundary(Vector3 pathPoint){
		bool result = false;
		float maxZ = cell [0, 0].z;
		float minZ = cell [0, 1].z;
		if (pathPoint.z <= maxZ && pathPoint.z >= minZ) {
			result = true;
		}
		return result;
	}
}
