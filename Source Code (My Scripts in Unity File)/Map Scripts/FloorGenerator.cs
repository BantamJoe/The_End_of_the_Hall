using UnityEngine;
using System.Collections.Generic;

public class FloorGenerator {

	WallGenerator wallGenerator = new WallGenerator();
	WinControllerGenerator winGenerator = new WinControllerGenerator();

	public void generateFloor (List<RoomObject> rooms) {
		float scaleXFloor = getScaleXForFloor (rooms);
		float floorPosition = getFloorPosition (scaleXFloor);
		GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
		plane.transform.localScale = new Vector3(scaleXFloor, 1, 1);
		plane.transform.position = new Vector3 (floorPosition, 0, 0);
		addTexture (plane.GetComponent<Renderer> ());
		Vector3 positioningForWinCollider = wallGenerator.finishWalls (rooms, scaleXFloor, floorPosition);
		winGenerator.generateController (positioningForWinCollider);
	}

	float getScaleXForFloor(List<RoomObject> rooms){
		float scale = 1;
		for (int i = 0; i < rooms.Count; i++) {
			scale += rooms [i].scaleOfRoomX;
		}
		return scale;
	}

	float getFloorPosition(float scale){
		return (scale * 5f) - 5;
	}

	void addTexture(Renderer planesRenderer){
		planesRenderer.material.mainTexture = Resources.Load ("hardwood") as Texture;
	}
}
