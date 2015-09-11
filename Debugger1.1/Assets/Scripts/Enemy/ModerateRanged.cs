using UnityEngine;
using System.Collections;

public class ModerateRanged : Enemy {

	Transform player = null;
	
	[SerializeField]
	Transform bullet = null;
	
	float delayTimer = 0.0f;
	[SerializeField]
	float maximumShotDistance = 7.0f;

	GameObject[] flanks = null;
	bool clockwise = false;
	int currFlank = 0;
	int prevFlank = -1;

	// Use this for initialization
	void Start () {
		UpdateStats ();

		if (currMode != Mode.Friendly && currMode != Mode.Deactivated) {
			player = GameObject.FindGameObjectWithTag ("Player").transform;

			flanks = GameObject.FindGameObjectsWithTag ("Ranged Flank");

			int random = Random.Range (0, 1);

			if (random == 0) {
				target = flanks [0].transform;
				prevFlank = 3;
				currFlank = 0;
			} else {
				target = flanks [2].transform;
				clockwise = true;
				prevFlank = 3;
				currFlank = 2;
			}

			agent.destination = target.position;
		}
	}
	
	// Update is called once per frame
	void Update () {
        Death();
		if (currMode == Mode.Patrolling)
			UpdateWaypoints ();
		
		if (currMode == Mode.Attack || currMode == Mode.Patrolling)
			agent.SetDestination (target.position);

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
				
				if (agent.remainingDistance < 1.0f) {
					if (clockwise) {
						if (currFlank == flanks.Length - 1) {
							prevFlank = currFlank;
							currFlank = 0;
						} else {
							prevFlank = currFlank;
							currFlank++;
						}
						
						target = flanks [currFlank].transform;
					} else {
						if (currFlank == 0) {
							prevFlank = currFlank;
							currFlank = flanks.Length - 1;
						} else {
							prevFlank = currFlank;
							currFlank--;
						}
						
						target = flanks [currFlank].transform;
					}
				}
				
				for (int index = 0; index < flanks.Length; index++) {
					if (index == currFlank || index == prevFlank)
						continue;
					
					if (Vector3.Distance (transform.position, flanks [index].transform.position) < agent.remainingDistance) {
						target = flanks [index].transform;
						break;
					}
				}
				
				if (Vector3.Distance (transform.position, player.position) <= maximumShotDistance && delayTimer <= 0.0f) {
					SoundManager.instance.EnemySoundeffects [1].Play ();
					FireBullet ();
					
					delayTimer = shotDelay;
				}
			} else if (currMode != Mode.Deactivated) {
				CheckForPlayer ();
			}

		} else {
			if (GameManager.CTimeScale2 > 0.0f) {
				FaceMouse ();
				
				if (InputManager.instance.GetButton ("Fire1") || InputManager.instance.GetButtonUp ("Fire2")) {
					if (delayTimer <= 0.0f) {
						delayTimer = shotDelay;
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

		if (delayTimer > 0.0f) {
			delayTimer -= Time.deltaTime * GameManager.CTimeScale;
			
			if (delayTimer < 0.0f)
				delayTimer = 0.0f;
		}
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
