using UnityEngine;
using System.Collections;

public class DoorOpen : MonoBehaviour {

    public bool open = false;
    public float doorOpenAngle = 90f;
    public float doorCloseAngle = 0;
    public float smooth = 2f;

	// Use this for initialization
	void Start () {
	
	}
	
    public void ChangeDoorState()
    {
        open = !open;
    }

	// Update is called once per frame
	void Update () {
        if (open)
        {
            Quaternion targetRotation = Quaternion.Euler(-90, doorOpenAngle, 0);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smooth * Time.deltaTime);
        }
        else
        {
            Quaternion targetRotation = Quaternion.Euler(-90, doorCloseAngle, 0);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smooth * Time.deltaTime);
        }
    }

	public void setDoorSide(bool onRightSide){
		if (onRightSide) {
			doorCloseAngle = 180f;
		} else {
			doorOpenAngle = -90f;
		}
	}

}
