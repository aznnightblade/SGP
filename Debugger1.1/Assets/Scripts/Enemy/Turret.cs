using UnityEngine;
using System.Collections;

public class Turret : Enemy {

	[SerializeField]
	Transform bullet = null;
	
	float shotDelayTimer = 0.0f;
	[SerializeField]
	float maximumShotDistance = 0.0f;

	// Use this for initialization
	void Start () {
		UpdateStats ();

		maximumShotDistance = initialShotDistance + ShotDistancePerDexerity * dexterity;
		detectRange = maximumShotDistance;
		maxDistance = maximumShotDistance;

		if (currMode != Mode.Friendly && currMode != Mode.Deactivated) {
			agent.updatePosition = false;
			agent.updateRotation = false;
		}
	}

	// Update is called once per frame
	void Update () {
		RechargeShields ();
		
		if (currMode != Mode.Friendly) {
			if (GameManager.CTimeScale == 0.0f) {
				agent.velocity = Vector3.zero;
				agent.updateRotation = false;
			}
			
			if (GameManager.CTimeScale > 0.0f && !agent.updateRotation) {
				agent.updateRotation = true;
			}
			
			if (currMode == Mode.Attack) {
				CheckForReset ();

				FacePlayer ();

				if (Vector3.Distance (transform.position, target.position) <= maximumShotDistance && shotDelayTimer <= 0.0f) {
					SoundManager.instance.EnemySoundeffects [1].Play ();
					FireBullet ();
					
					shotDelayTimer = shotDelay;
				}
			} else if (currMode != Mode.Deactivated) {
				CheckForPlayer ();
			}
		}

		if (shotDelayTimer > 0.0f) {
			shotDelayTimer -= Time.deltaTime * GameManager.CTimeScale;
			
			if (shotDelayTimer < 0.0f)
				shotDelayTimer = 0.0f;
		}
	}

	void FireBullet() {
		SoundManager.instance.EnemySoundeffects[1].Play();
		GameObject newBullet = (Instantiate (bullet, transform.position, transform.rotation) as Transform).gameObject;
		newBullet.tag = ("Enemy Bullet");
		newBullet.GetComponent<Weapon> ().Owner = this;
		newBullet.GetComponent<Weapon> ().OwnerMoveDirection = Vector3.zero;
	}
}
