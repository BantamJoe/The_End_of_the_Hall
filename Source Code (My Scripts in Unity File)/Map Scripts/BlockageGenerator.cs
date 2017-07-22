using UnityEngine;
using System.Collections.Generic;

public class BlockageGenerator {

	string[] blockageStrings = { "CrateBlockage" };
	System.Random random = new System.Random();

	public void generate(List<RoomObject> rooms){
		for (int i = 0; i < rooms.Count; i++) {
			GameObject blockage = GameObject.Instantiate (Resources.Load(selectBlockage(), typeof(GameObject))) as GameObject;
			blockage.transform.position = new Vector3 (rooms [i].positionPointX, 0, 0);
			blockage.transform.localRotation = Quaternion.Euler (new Vector3 (0, 90f, 0));
		}
	}

	string selectBlockage(){
		return blockageStrings[random.Next(blockageStrings.Length)];
	}



}
