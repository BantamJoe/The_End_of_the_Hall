using UnityEngine;
using System.Collections.Generic;

public class RoomSetupController {

	private OfficeSetup officeSetup = new OfficeSetup();
	private ClassroomSetup classroomSetup = new ClassroomSetup();
	private LoungeSetup loungeSetup = new LoungeSetup();
	private BedroomSetup bedroomSetup = new BedroomSetup();
	private StorageSetup storageSetup = new StorageSetup();

	public void setupEachRoom(List<RoomObject> rooms) {
		for (int i = 0; i < rooms.Count; i++) {
			string roomName = rooms [i].name;
			switch (roomName) {
				case "office":
					officeSetup.setup (rooms[i]);
					break;
				case "classroom":
					classroomSetup.setup (rooms[i]);
					break;
				case "loungeAndKitchen":
					loungeSetup.setup (rooms[i]);
					break;
				case "bedroom":
					bedroomSetup.setup (rooms[i]);
					break;
				case "storage":
					storageSetup.setup (rooms[i]);
					break;
				default:
					Debug.Log ("Room Name Not Found!");
					break;
			}
		}
	}

}
