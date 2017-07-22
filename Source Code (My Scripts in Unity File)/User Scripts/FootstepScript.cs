using UnityEngine;
using System.Collections;

public class FootstepScript : MonoBehaviour {

	CharacterController cc;
	AudioSource source;
	public AudioClip footstep;
	public AudioClip shortFootstep;

	void Start () {
		cc = GetComponent<CharacterController> ();
		source = GetComponent<AudioSource> ();
	}
	
	void Update () {
		if(source.enabled){
			if (cc.isGrounded && isSprinting () && !source.isPlaying) {
				heavyFootstep ();
			} 
			if (cc.isGrounded && isWalking() && !source.isPlaying) {
				softFootstep ();
			}
		}
	}

	bool isSprinting(){
		bool result;
		if (cc.velocity.magnitude > 5f) {
			result = true;
		} else {
			result = false;
		}
		return result;
	}

	bool isWalking(){
		bool result;
		if (cc.velocity.magnitude > 2f && cc.velocity.magnitude < 6f) {
			result = true;
		} else {
			result = false;
		}
		return result;
	}

	void softFootstep(){
		source.clip = footstep;
		source.volume = Random.Range (0.6f, 0.8f);
		source.pitch = Random.Range (0.8f, 1.1f);
		source.Play ();
	}

	void heavyFootstep(){
		source.clip = shortFootstep;
		source.volume = Random.Range (0.8f, 1f);
		source.pitch = Random.Range (0.8f, 1.1f);
		source.Play ();
	}

}
