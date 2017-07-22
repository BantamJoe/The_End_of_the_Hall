using UnityEngine;
using System.Collections;

public class FlashlightMovement : MonoBehaviour {

    public float mouseSensitivity = 6.0f;
    public float pitchRange = 60.0f;
    public AudioClip soundOn;
    public AudioClip soundOff;
    Light flashlight;
    float verticalRot = 0;
    AudioSource sound;

    // Use this for initialization
    void Start () {
        flashlight = GetComponent<Light>();
        sound = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        /*float rotLR = Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.Rotate(0, rotLR, 0);*/

        verticalRot -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        verticalRot = Mathf.Clamp(verticalRot, -pitchRange, pitchRange);
        flashlight.transform.localRotation = Quaternion.Euler(verticalRot, 0, 0);

        if(Input.GetButtonDown("Flashlight Toggle"))
        {
            if (flashlight.enabled)
            {
                flashlight.enabled = false;
                sound.clip = soundOn;
                sound.Play();
            }
            else
            {
                flashlight.enabled = true;
                sound.clip = soundOff;
                sound.Play();
            }
        }
    }
}
