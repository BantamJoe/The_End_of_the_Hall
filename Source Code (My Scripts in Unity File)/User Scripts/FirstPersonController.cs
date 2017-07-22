using UnityEngine;
using System.Collections;

[RequireComponent (typeof(CharacterController))]
public class FirstPersonController : MonoBehaviour {

	public float mouseSensitivity = 6.0f;
	public float jumpSpeed = 5;

	CharacterController cc;

	float verticalRot = 0;
	float verticalVelocity = 0;
	float pitchRange = 60.0f;

	float movementSpeed;
	float sprintSpeed = 6.0f;
	float walkSpeed = 3.0f;
	bool isRunning;
	float stamina = 5f;
	float maxStamina = 5f;

	Rect staminaBar;
	Texture2D staminaBarTexture;

	// Use this for initialization
	void Start () {
        Cursor.visible = false;
        cc = GetComponent<CharacterController>();
		setSprinting (false);
		staminaBar = new Rect (Screen.width/10, Screen.height * 9/10, Screen.width/3, Screen.height/50);
		staminaBarTexture = new Texture2D (1, 1);
		staminaBarTexture.SetPixel (0, 0, Color.green);
		staminaBarTexture.Apply ();
    }

    // Update is called once per frame
    void Update () {
        handleRotation ();
		Vector3 speed = getMovementVector ();
        cc.Move(speed * Time.deltaTime);
    }

	void handleRotation(){
		float rotLR = Input.GetAxis("Mouse X") * mouseSensitivity;
		transform.Rotate(0, rotLR, 0);
		verticalRot -= Input.GetAxis("Mouse Y") * mouseSensitivity;
		verticalRot = Mathf.Clamp(verticalRot, -pitchRange, pitchRange);
		Camera.main.transform.localRotation = Quaternion.Euler(verticalRot, 0, 0);
	}

	Vector3 getMovementVector(){
		float forwardSpeed;
		float sideSpeed;
		handleSprintButton ();
		forwardSpeed = Input.GetAxis("Vertical") * movementSpeed;
		sideSpeed = Input.GetAxis("Horizontal") * movementSpeed;
		handleSprintValue ();
		handleJumps ();
		//Movement vector
		Vector3 speed = new Vector3(sideSpeed, verticalVelocity, forwardSpeed);
		speed = transform.rotation * speed;
		return speed;
	}

	void handleSprintButton(){
		if (Input.GetButtonDown ("Sprint")) {
			setSprinting (true);
		}
		if (Input.GetButtonUp ("Sprint")) {
			setSprinting (false);
		}
	}

	void handleSprintValue(){
		if (isRunning) {
			//Decrease Stamina until there is none left
			stamina -= Time.deltaTime;
			if (stamina < 0) {
				stamina = 0;
				setSprinting (false);
			}
		} else if(stamina < maxStamina) {
			//Replenish Stamina
			stamina += Time.deltaTime/2;
		}
	}

	void handleJumps(){
		verticalVelocity += Physics.gravity.y * Time.deltaTime;
		if (Input.GetButtonDown("Jump") && cc.isGrounded) {
			verticalVelocity = jumpSpeed;
		}
	}

	void OnGUI(){
		float staminaRatio = stamina / maxStamina;
		float staminaBarWidth = staminaRatio * Screen.width / 4;
		staminaBar.width = staminaBarWidth;
		GUI.DrawTexture (staminaBar, staminaBarTexture);
	}

	void setSprinting(bool isRunning){
		this.isRunning = isRunning;
		movementSpeed = isRunning ? sprintSpeed : walkSpeed;
	}

}
