using UnityEngine;
using System.Collections.Generic;

public struct RoomObject {

	public int ID { get; set; }
	public string name { get; set; }
	public float roomDimensionX { get; set; }
	public float roomDimensionZ { get; set; }
	public float leftDoorPositionX { get; set; }
	public float rightDoorPositionX { get; set; }
	public float doorPositionZ { get; set; }
	public float rotation { get; set; }
	public float positionPointX { get; set; }
	public float scaleOfRoomX { get; set; }
	public bool onRightSideOfHall { get; set; }
	public int numberOfPossibleEvents { get; set; }
	public GameObject roomsAsGameObject { get; set; }
	public NPC npc { get; set; }
	public List<GridCell> npcPatrolPoints { get; set; }
	public Vector3 roomOrigin { get; set; }
	public float roomRotation { get; set; }

}
