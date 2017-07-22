using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

	static public bool playerWasKilled;
	static public int roomPlayerDiedIn;

	DeathListener deathListener;

	float playerHealth = 100f;
	float maxHealth = 100f;
	int damage = 10;
	string ATTACKING = "isAttacking";
	string IDLE = "isIdle";
	string WALKING = "isWalking";
	string RUNNING = "isRunning";

	Rect healthBar;
	Texture2D healthBarTexture;
	TimeSpentInEachRoom timeSpentScript;

	void Start () {
		timeSpentScript = this.GetComponent<TimeSpentInEachRoom> ();
		playerWasKilled = false;
		deathListener = GameObject.Find ("DeathController").GetComponent<DeathListener> ();
		healthBar = new Rect (Screen.width/10, Screen.height * 8.75f/10, Screen.width/3, Screen.height/50);
		healthBarTexture = new Texture2D (1, 1);
		healthBarTexture.SetPixel (0, 0, Color.red);
		healthBarTexture.Apply ();
	}

	void OnCollisionEnter (Collision collider) {
		if(collider.gameObject.tag == "Enemy") {
			GameObject enemy = collider.gameObject;
			AttackScript script = enemy.GetComponent<AttackScript> ();
			if (script) {
				StartCoroutine(hit (enemy));
			}
		}
	}

	IEnumerator hit(GameObject enemy){
		yield return new WaitForSeconds (0.85f);
		float distance = Vector3.Distance (this.transform.position, enemy.transform.position);
		if (distance < 1.3) {
			playerHealth -= damage;
			if (playerHealth == 0) {
				roomPlayerDiedIn = timeSpentScript.roomThePlayerIsIn;
				playerWasKilled = true;
				deathListener.playerDied ();
			}
			AudioSource source = enemy.GetComponent<AudioSource> ();
			source.Play ();
			Debug.Log ("Playerhealth: " + playerHealth);
			enemy.transform.Translate (new Vector3 (0, 0, -1f));
			Animator anim = enemy.GetComponent<Animator> ();
			anim.SetBool (IDLE, false);
			anim.SetBool (RUNNING, true);
			anim.SetBool (WALKING, false);
			anim.SetBool (ATTACKING, false);
		}
	}

	void OnGUI(){
		float healthRatio = playerHealth / maxHealth;
		float healthBarWidth = healthRatio * Screen.width / 4;
		healthBar.width = healthBarWidth;
		GUI.DrawTexture (healthBar, healthBarTexture);
	}

}
