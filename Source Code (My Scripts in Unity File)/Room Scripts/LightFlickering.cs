using UnityEngine;
using System.Collections;

public class LightFlickering : MonoBehaviour {

    public float minTime = 0.05f;
    public float maxTime = 1.2f;

    float timer;
    Light theLight;
    //AudioSource sound;

	// Use this for initialization
	void Start () {
        theLight = GetComponent<Light>();
        timer = Random.Range(minTime, maxTime);
        //sound = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            theLight.enabled = !theLight.enabled;
            timer = Random.Range(minTime, maxTime);
            //sound.Play();
        }
    }
}
