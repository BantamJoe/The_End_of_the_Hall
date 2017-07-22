using UnityEngine;
using System.Collections;

public class InteractScript : MonoBehaviour {

    public float interactDistance = 5f;
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire1"))
        {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, interactDistance))
            {
                if (hit.collider.gameObject.CompareTag("Door"))
                {
                    DoorOpen door = hit.collider.gameObject.GetComponent<DoorOpen>();
                    door.ChangeDoorState();
                }
            }
        }
	}
}
