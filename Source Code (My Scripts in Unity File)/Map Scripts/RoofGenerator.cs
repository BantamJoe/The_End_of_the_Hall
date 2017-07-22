using UnityEngine;
using System.Collections.Generic;

public class RoofGenerator {

	public void generateRoof (List<RoomObject> rooms) {
		float scaleXRoof = getScaleXForRoof (rooms);
		float roofPosition = getRoofPosition (scaleXRoof);
		GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
		plane.transform.localScale = new Vector3(scaleXRoof, 1, 1);
		plane.transform.position = new Vector3 (roofPosition, 4.6f, 0);
		plane.transform.localRotation = Quaternion.Euler (new Vector3 (180f, 0, 0));
		addTexture (plane.GetComponent<Renderer> ());
		List<float> lightPositions = getLightPositions (rooms);
		generateLights (lightPositions);
		generateFixtures (lightPositions);
	}

	float getScaleXForRoof(List<RoomObject> rooms){
		float scale = 1;
		for (int i = 0; i < rooms.Count; i++) {
			scale += rooms [i].scaleOfRoomX;
		}
		return scale;
	}

	float getRoofPosition(float scale){
		return (scale * 5f) - 5;
	}

	void addTexture(Renderer planesRenderer){
		planesRenderer.material.mainTextureScale = new Vector2 (60f, 10f);
		planesRenderer.material.mainTexture = Resources.Load ("ceiling_tile") as Texture;
	}

	List<float> getLightPositions(List<RoomObject> rooms){
		List<float> lightPositions = new List<float> ();
		for (int i = 0; i < rooms.Count; i++) {
			if ((i + 1) < rooms.Count) {
				float position = (rooms [i].positionPointX + rooms [i + 1].positionPointX) / 2;
				lightPositions.Add (position);
			}
		}
		return lightPositions;
	}

	void generateLights(List<float> lightPositions){
		for (int i = 0; i < lightPositions.Count; i++) {
			GameObject hallLight = new GameObject ("Hall Light " + (i+1));
			Light lightComp = hallLight.AddComponent<Light> ();
			lightComp.transform.position = new Vector3 (lightPositions[i], 4.4f, 0);
			lightComp.transform.localRotation = Quaternion.Euler(new Vector3(90f, 0, 0));
			lightComp.type = LightType.Point;
			lightComp.spotAngle = 130f;
			lightComp.intensity = 3;
			lightComp.bounceIntensity = 2;
			hallLight.AddComponent<LightFlickering> ();
		}
	}

	void generateFixtures(List<float> lightPositions){
		for (int i = 0; i < lightPositions.Count; i++) {
			GameObject fixture = GameObject.Instantiate(Resources.Load("lightFixture", typeof(GameObject))) as GameObject;
			fixture.transform.position = new Vector3 (lightPositions[i], 4.6f, 0);
			fixture.gameObject.name = "fixture" + i;
		}
	}

}
