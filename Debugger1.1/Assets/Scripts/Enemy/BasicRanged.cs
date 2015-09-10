using UnityEngine;
using System.Collections;

public class BasicRanged : Enemy {
	
	[SerializeField]
	Transform bullet = null;

	float delayTimer = 0.0f;
	[SerializeField]
	float maximumShotDistance = 5.0f;

	// Use this for initialization
	void Start () {
		UpdateStats ();
	}
	
	// Update is called once per frame
	void Update () {
		if (currMode == Mode.Patrolling)
			UpdateWaypoints ();

		if (currMode == Mode.Attack || currMode == Mode.Patrolling || currMode == Mode.BossRoom)
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

				if (Vector3.Distance (transform.position, target.position) <= maximumShotDistance && delayTimer <= 0.0f) {
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
		
		if (currHealth <= 0.0f)
			DestroyObject ();
	}

	void FireBullet () {
		GameObject newBullet = null;

		if (currMode != Mode.Friendly) {
			Vector3 pos = transform.position;
			Vector3 direction = pos - target.position;
			float rot = -((Mathf.Atan2 (direction.z, direction.x) * 180 / Mathf.PI) + 90.0f);

			newBullet = (Instantiate (bullet, pos, Quaternion.Euler (0, rot, 0)) as Transform).gameObject;
			newBullet.tag = ("Enemy Bullet");
		} else {
			newBullet = (Instantiate (bullet, transform.position, Quaternion.Euler (0, transform.rotation.y, 0)) as Transform).gameObject;
			newBullet.tag = ("Player Bullet");
		}

		newBullet.GetComponent<Weapon> ().Owner = this;
		newBullet.GetComponent<Weapon> ().OwnerMoveDirection = agent.velocity;
	}
}
