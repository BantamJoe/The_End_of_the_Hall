using UnityEngine;
using System.Collections.Generic;

public class Grid {

	public List<GridCell> grid { get; set; }

	Vector3 finishedVector = new Vector3 (0, 0, 0);

	public void generateGridForRoom(RoomObject room){
		grid = new List<GridCell> ();
		bool cellGenerationFinished = false;
		bool firstIteration = true;
		bool newRow = false;
		Vector3 nextBottomRight = new Vector3 ();
		int counter = 2; 
		Vector3 initialBottomRight = room.npc.boundary [1, 1];
		while (!cellGenerationFinished) {
			GridCell cell = new GridCell ();
			if (firstIteration) {
				nextBottomRight = cell.calculateAndSetCell (initialBottomRight, room);
				firstIteration = false;
			} else {
				if (newRow) {
					float x = initialBottomRight.x + (5 * counter);
					Vector3 startOfNewRowCellBottomRight = new Vector3(x, 0, initialBottomRight.z);
					nextBottomRight = cell.calculateAndSetCell (startOfNewRowCellBottomRight, room);
					counter++;
					newRow = false;
				} else {
					nextBottomRight = cell.calculateAndSetCell (nextBottomRight, room);
					if (lastCellInRow (nextBottomRight, room)) {
						newRow = true;
					}
				}
			}
			if (vectorsEqualFinished (nextBottomRight)) {
				cellGenerationFinished = true;
			}
			grid.Add (cell);
		}
	}

	bool vectorsEqualFinished(Vector3 nextBottomRight){
		return Vector3.SqrMagnitude (nextBottomRight - finishedVector) < 0.0001;
	}

	bool lastCellInRow(Vector3 nextBottomRight, RoomObject room){
		return nextBottomRight.z == room.npc.boundary [0, 0].z;
	}

}
