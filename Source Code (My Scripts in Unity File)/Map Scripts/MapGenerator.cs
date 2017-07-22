using UnityEngine;
using System.Collections.Generic;
using System;

public class MapGenerator : MonoBehaviour {

	private RoomSelector roomSelector = new RoomSelector ();
	private FloorGenerator floorGenerator = new FloorGenerator ();
	private WallGenerator wallGenerator = new WallGenerator ();
	private RoofGenerator roofGenerator = new RoofGenerator();
	private RoomSetupController roomSetup = new RoomSetupController ();
	private NPCGenerator npcGenerator = new NPCGenerator();
	private BlockageGenerator blockageGenerator = new BlockageGenerator ();

	public static List<RoomObject> completedRooms;

	void Start () {
		if (completedRooms == null) {
			List<RoomObject> rooms = roomSelector.selectRooms ();
			completedRooms = instaniateRooms (rooms);
			completedRooms = npcGenerator.generateNPCs (completedRooms);
		} else {
			completedRooms = instaniateRooms (completedRooms);
			npcGenerator.regenerateNPCs (completedRooms);
		}
		floorGenerator.generateFloor (completedRooms);
		roofGenerator.generateRoof (completedRooms);
		wallGenerator.generateWalls (completedRooms);
		roomSetup.setupEachRoom (completedRooms);
		blockageGenerator.generate (completedRooms);
	}

	List<RoomObject> instaniateRooms(List<RoomObject> rooms){
		List<RoomObject> instaniatedRooms = new List<RoomObject> ();
		for (int i = 0; i < rooms.Count; i++) {
			GameObject room = Instantiate(Resources.Load(rooms[i].name, typeof(GameObject))) as GameObject;
			room.transform.position = rooms [i].roomOrigin;
			room.transform.localRotation = Quaternion.Euler (new Vector3 (0, rooms[i].roomRotation, 0));
			RoomObject roomObject = rooms [i];
			roomObject.roomsAsGameObject = room;
			instaniatedRooms.Add (roomObject);
		}
		return instaniatedRooms;
	}

	public static void clearRoomMemory(){
		completedRooms = null;
	}

	public NPC getNPCReferenceFromRoom(int roomIndex){
		return completedRooms [roomIndex].npc;
	}

}
