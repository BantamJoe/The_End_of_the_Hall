using UnityEngine;
using System.Collections.Generic;

public class ProcessPaths {

	List<List<Vector3>> playerPaths;
	List<RoomObject> roomReferences;
	List<Grid> cellsPerRoom;

	public List<RoomObject> processPaths(){
		roomReferences = MapGenerator.completedRooms;
		roomReferences = initializeRoomsPatrolReference ();
		playerPaths = PlayerPath.playerPaths;
		analyzeByRoom ();
		return roomReferences;
	}

	List<RoomObject> initializeRoomsPatrolReference (){
		List<RoomObject> roomsWithNewPatrolReference = new List<RoomObject> ();
		for (int room = 0; room < roomReferences.Count; room++) {
			RoomObject givenRoom = roomReferences [room];
			givenRoom.npcPatrolPoints = new List<GridCell> ();
			roomsWithNewPatrolReference.Add (givenRoom);
		}
		return roomsWithNewPatrolReference;
	}

	void analyzeByRoom(){
		setCellsPerRoom ();
		for(int room = 0; room < roomReferences.Count; room++){
			for (int cellIndex = 0; cellIndex < cellsPerRoom [room].grid.Count; cellIndex++) {
				float averagePointsInCellPerGame = getAveragePointsInCell (cellsPerRoom[room].grid[cellIndex]);
				//Debug.Log("Room: " + room + " Cell: " + cellIndex + " Average: " + averagePointsInCellPerGame);
				if (averagePointsInCellPerGame >= 50) {
					roomReferences [room].npcPatrolPoints.Add (cellsPerRoom [room].grid [cellIndex]);
				}
			}
		}
	}

	void setCellsPerRoom(){
		cellsPerRoom = new List<Grid> ();
		for (int room = 0; room < roomReferences.Count; room++) {
			Grid gridForRoom = new Grid ();
			gridForRoom.generateGridForRoom (roomReferences [room]);
			cellsPerRoom.Add (gridForRoom);
		}
	}

	float getAveragePointsInCell(GridCell cell){
		int totalCountForCell = getCountForCell (cell);
		return (float)totalCountForCell / (float)playerPaths.Count;
	}

	int getCountForCell(GridCell cell){
		int count = 0;
		for (int gameIndex = 0; gameIndex < playerPaths.Count; gameIndex++) {
			count += getCountForGame (playerPaths [gameIndex], cell);
		}
		return count;
	}

	int getCountForGame(List<Vector3> playerPath, GridCell cell){
		int count = 0;
		for (int pathIndex = 0; pathIndex < playerPath.Count; pathIndex++) {
			if(cell.pathPointIsWithinGridCell(playerPath[pathIndex])){
				count += 1;
			}
		}
		return count;
	}

}
