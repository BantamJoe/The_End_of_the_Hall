using UnityEngine;
using System.Collections.Generic;

public class WallGenerator {

	private string WALL_TEXTURE = "backgrounddetailed3";
		
	//Sets the position and scale of each wall before instantiating the given wall
	public void generateWalls(List<RoomObject> rooms){
		for (int i = 0; i < rooms.Count; i++) {
			if (indexNotOutOfBounds(i, rooms.Count)) {
				Vector3 wallStart = calculateWallStartPosition (rooms [i]);
				Vector3 wallEnd = calculateWallEndPosition (rooms [i + 2]);
				float zDisplacement = getZDisplacemnet (rooms [i]);
				Vector3 wallPosition = new Vector3 (wallStart.x + wallEnd.x, 0, zDisplacement) / 2f;
				Vector3 wallScale = calculateScaleOfWall (wallStart, wallEnd);
				instantiateWall (wallScale, wallPosition, rooms[i].onRightSideOfHall);
			}
		}
	}

	public Vector3 finishWalls(List<RoomObject> rooms, float floorScale, float floorPosition){
		//	We "reset" the position by adding 5 becuase we subtract 5 in FloorGenerator.getFloorPosition
		//	in order to have the floor start behind the point (0, 0, 0) where the Player Starts
		float resetFloorPosition = floorPosition + 5;
		Vector3 floorStartPoint = new Vector3 (floorPosition - resetFloorPosition, 0, 0);
		Vector3 floorEndPoint = new Vector3 (floorPosition + resetFloorPosition, 0, 0);
		generateBeginningWalls (rooms[0], rooms[1], floorStartPoint);
		generateEndWalls (rooms[rooms.Count - 1], rooms[rooms.Count - 2], floorEndPoint);
		return floorEndPoint;
	}

	void generateBeginningWalls(RoomObject closestRoom, RoomObject nextClosestRoom, Vector3 floorStart){
		Vector3 wallStart = floorStart;
		generateStartingWall (closestRoom, wallStart);
		generateStartingWall (nextClosestRoom, wallStart);
		generateBookendWall (floorStart, false);
	}

	void generateEndWalls(RoomObject closestRoom, RoomObject nextClosestRoom, Vector3 floorEnd){
		Vector3 wallEnd = floorEnd;
		generateEndingWall (closestRoom, wallEnd);
		generateEndingWall (nextClosestRoom, wallEnd);
		generateBookendWall (floorEnd, true);
	}

	void generateStartingWall(RoomObject room, Vector3 wallStart){
		float zDisplacement = getZDisplacemnet (room);
		Vector3 wallEnd = calculateWallEndPosition (room);
		Vector3 wallPosition = new Vector3 (wallStart.x + wallEnd.x, 0, zDisplacement) / 2f;
		Vector3 wallScale = calculateScaleOfWall (wallStart, wallEnd);
		instantiateWall (wallScale, wallPosition, room.onRightSideOfHall);
	}

	void generateEndingWall(RoomObject room, Vector3 wallEnd){
		float zDisplacement = getZDisplacemnet (room);
		Vector3 wallStart = calculateWallStartPosition (room);
		Vector3 wallPosition = new Vector3 (wallStart.x + wallEnd.x, 0, zDisplacement) / 2f;
		Vector3 wallScale = calculateScaleOfWall (wallStart, wallEnd);
		instantiateWall (wallScale, wallPosition, room.onRightSideOfHall);
	}

	void generateBookendWall(Vector3 floorStart, bool firstWall){
		Vector3 wallScale = new Vector3 (1, 0.92f, 1);
		GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Plane);
		wall.transform.localScale = wallScale;
		wall.transform.position = floorStart;
		float rotationAngle = getWallRotation(firstWall);
		wall.transform.localRotation = Quaternion.Euler (new Vector3 (0, 0, rotationAngle));
		Renderer wallRenderer = wall.GetComponent<Renderer> ();
		wallRenderer.material.mainTexture = Resources.Load (WALL_TEXTURE) as Texture;
	}

	Vector3 calculateWallStartPosition(RoomObject room){
		float roomsLength = (room.roomDimensionX);
		float wallPositionStart = roomsLength + room.positionPointX;
		return new Vector3 (wallPositionStart, 0, 0);
	}

	Vector3 calculateWallEndPosition(RoomObject room){
		float roomsLength = (room.roomDimensionX);
		float wallPositionEnd = room.positionPointX - roomsLength;
		return new Vector3 (wallPositionEnd, 0, 0);
	}

	Vector3 calculateScaleOfWall(Vector3 wallStart, Vector3 wallEnd){
		Vector3 scale = new Vector3 (1, 1, 0.92f);
		scale.x = Vector3.Distance (wallStart, wallEnd) / 10f;
		return scale;
	}

	void instantiateWall (Vector3 scale, Vector3 wallPosition, bool rightSide){
		GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Plane);
		wall.transform.localScale = scale;
		wall.transform.position = wallPosition;
		float rotationAngle = getWallRotation(rightSide);
		wall.transform.localRotation = Quaternion.Euler (new Vector3 (rotationAngle, 0, 0));
		Renderer wallRenderer = wall.GetComponent<Renderer> ();
		wallRenderer.material.mainTexture = Resources.Load (WALL_TEXTURE) as Texture;
	}

	float getZDisplacemnet(RoomObject room){
		float result;
		if (room.onRightSideOfHall) {
			result = -10f;
		} else {
			result = 10f;
		}
		return result;
	}

	float getWallRotation(bool rightSide){
		float result;
		if (rightSide) {
			result = 90f;
		} else {
			result = -90f;
		}
		return result;
	}

	bool indexNotOutOfBounds(int index, int roomsSize){
		return (index + 2) < roomsSize;
	}

}
