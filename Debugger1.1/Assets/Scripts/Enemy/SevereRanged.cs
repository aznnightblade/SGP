using UnityEngine;
using System.Collections;

public class SevereRanged : Enemy {

	Transform player = null;
	
	[SerializeField]
	Transform bullet = null;
	
	float shotDelayTimer = 0.0f;
	[SerializeField]
	float maximumShotDistance = 7.0f;

	[SerializeField]
	float teleportDelay = 0.75f;
	float teleportDelayTimer = 0.0f;
	[SerializeField]
	float minDistanceFromPlayer = 2.0f;
	[SerializeField]
	float maxTeleportDistance = 9.0f;

	// Use this for initialization
	void Start () {
        UpdateStats();

		player = GameObject.FindGameObjectWithTag ("Player").transform;

		if (currMode != Mode.Friendly && currMode != Mode.Deactivated) {
			agent.updatePosition = false;
			agent.updateRotation = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (currMode == Mode.Patrolling)
			UpdateWaypoints ();
		
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

				if (teleportDelayTimer <= 0.0f) {
					Teleport ();
				}

				FacePlayer ();

				if (Vector3.Distance (transform.position, player.position) <= maximumShotDistance && shotDelayTimer <= 0.0f) {
					SoundManager.instance.EnemySoundeffects [1].Play ();
					FireBullet ();
					
					shotDelayTimer = shotDelay;
				}
			} else if (currMode != Mode.Deactivated) {
				CheckForPlayer ();
			}
		} else {
			if (GameManager.CTimeScale2 > 0.0f) {
				FaceMouse ();
				
				if (InputManager.instance.GetButton ("Fire1") || InputManager.instance.GetButtonUp ("Fire2")) {
					if (shotDelayTimer <= 0.0f) {
						shotDelayTimer = shotDelay;
						GameObject newBullet = GameObject.FindGameObjectWithTag("Player Controller").GetComponent<PlayerController> ().CreateBullet(bullet, transform.position, transform.rotation.eulerAngles.y);
						newBullet.GetComponent<Weapon> ().Owner = gameObject.GetComponent<Enemy> ();
						newBullet.GetComponent<Weapon> ().CurrColor = color;
						newBullet.layer = LayerMask.NameToLayer("Player Bullet");
					}
				}
				
				if (InputManager.instance.GetButton ("Fire2") && !InputManager.instance.GetButton ("Fire1")) {
					Weapon enemyWeapon = bullet.GetComponent<Weapon> ();
					
					if (enemyWeapon.ChargeDelay <= 0.0f) {
						if (enemyWeapon.ChargeScale == 1.0f)
							SoundManager.instance.PlayerSoundeffects[1].Play();
						
						if (!SoundManager.instance.PlayerSoundeffects[1].isPlaying && !SoundManager.instance.PlayerSoundeffects[2].isPlaying)
							SoundManager.instance.PlayerSoundeffects[2].Play();
						
						if ((enemyWeapon.ChargeScale < enemyWeapon.MaxChargeScale))
						{
							enemyWeapon.ChargeScale += enemyWeapon.ChargePerTick;
							enemyWeapon.ChargeDelay = enemyWeapon.DelayTime;
							
							if (enemyWeapon.ChargeScale > enemyWeapon.MaxChargeScale)
								enemyWeapon.ChargeScale = enemyWeapon.MaxChargeScale;	
						}
					}
				}
			}
		}
		
		if (shotDelayTimer > 0.0f) {
			shotDelayTimer -= Time.deltaTime * GameManager.CTimeScale;
			
			if (shotDelayTimer < 0.0f)
				shotDelayTimer = 0.0f;
		}

		if (teleportDelayTimer > 0.0f) {
			teleportDelayTimer -= Time.deltaTime * GameManager.CTimeScale;
			
			if (teleportDelayTimer < 0.0f)
				teleportDelayTimer = 0.0f;
		}
	}

	void Teleport () {
		int tries = 0;

		if (Vector3.Distance (transform.position, player.position) <= maximumShotDistance) {
			Vector3 position = Random.insideUnitSphere;
			position *= maximumShotDistance;
			position += target.position;
			position.y = transform.position.y;

			if (Vector3.Distance(position, player.position) < minDistanceFromPlayer) {
				position = (player.position - position).normalized * minDistanceFromPlayer + player.position;
			}
			
			NavMeshHit hit;
			float distanceFromPoint = 0.0f;
			do {
				NavMesh.SamplePosition(position, out hit, distanceFromPoint, 1);

				if (hit.hit == true) {
					Vector3 teleportPosition = hit.position;
					teleportPosition.y = transform.position.y;
					transform.position = teleportPosition;
				} else {
					distanceFromPoint += 1.0f;
					tries++;
				}
			} while (hit.hit == false && tries < 20);
		} else {
			Vector3 direction = ((target.position - transform.position).normalized * maxTeleportDistance) + transform.position;

			if (Vector3.Distance(direction, player.position) < minDistanceFromPlayer) {
				direction = (player.position - direction).normalized * minDistanceFromPlayer + player.position;
			}

			NavMeshHit hit;

			float distanceFromPoint = 0.0f;
			do {
				NavMesh.SamplePosition(direction, out hit, distanceFromPoint, 1);
				
				if (hit.hit == true) {
					Vector3 teleportPosition = hit.position;
					teleportPosition.y = transform.position.y;
					transform.position = teleportPosition;
				} else {
					distanceFromPoint += 1.0f;
					tries++;
				}
			} while (hit.hit == false && tries < 20);
		}

		teleportDelayTimer = teleportDelay;
	}

	void FacePlayer () {
		Vector3 direction = (transform.position - player.position).normalized;
		float rot = (Mathf.Atan2 (-direction.y, direction.x) * 180 / Mathf.PI) - 90;
		transform.rotation = Quaternion.Euler (0, rot, 0);
	}

	void FireBullet() {
		Vector3 pos = transform.position;
		Vector3 direction = pos - player.position;
		float rot = -((Mathf.Atan2(direction.z, direction.x) * 180 / Mathf.PI) + 90.0f);
		SoundManager.instance.EnemySoundeffects[1].Play();
		GameObject newBullet = (Instantiate (bullet, pos, Quaternion.Euler(0, rot, 0)) as Transform).gameObject;
		newBullet.tag = ("Enemy Bullet");
		newBullet.GetComponent<Weapon> ().Owner = this;
		newBullet.GetComponent<Weapon> ().OwnerMoveDirection = Vector3.zero;
	}
}
