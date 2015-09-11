using UnityEngine;
using System.Collections;

public class BasicMelee : Enemy {

	[SerializeField]
	float damageDelay = 0.5f;
	float delayTimer = 0.0f;
	bool attacking = false;

	// Use this for initialization
	void Start () {
		UpdateStats ();
	}
	
	// Update is called once per frame
	void Update () {
		if (currMode == Mode.Patrolling)
			UpdateWaypoints ();

		if (currMode == Mode.Attack || currMode == Mode.Patrolling || currMode == Mode.BossRoom)
			agent.destination = target.position;

		RechargeShields ();

		if (currMode != Mode.Friendly && currMode != Mode.Deactivated) {
			if (GameManager.CTimeScale == 0.0f) {
				agent.velocity = Vector3.zero;
				agent.updateRotation = false;
			}
		
			if (GameManager.CTimeScale > 0.0f && !agent.updateRotation) {
				agent.updateRotation = true;
			}

			if (currMode == Mode.Attack) {
				CheckForReset ();
			} else if (currMode != Mode.Deactivated) {
				CheckForPlayer ();
			}

			if (attacking && delayTimer <= 0.0f) {
				Player player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ();
				SoundManager.instance.EnemySoundeffects [0].Play ();
				float damage = (initialDamage + damagePerStrength * strength) - player.Defense;

				if (damage < 0.0f)
					damage = 0;
				SoundManager.instance.EnemySoundeffects [0].Play ();
				player.DamagePlayer (Mathf.CeilToInt (damage));

				delayTimer = damageDelay;
			}
		} else {
			if (agent.enabled == true) {
				agent.enabled = false;
			}

			if (target != null) {
				if ((InputManager.instance.GetButton("Fire1") || InputManager.instance.GetButton("Fire2")) && delayTimer <= 0.0f) {
					float damage = (initialDamage + damagePerStrength * strength) - target.GetComponent<Statistics> ().Defense;
					
					if (damage < 0.0f)
						damage = 0;

					target.GetComponent<Statistics> ().Damage(Mathf.CeilToInt (damage), transform);
					SoundManager.instance.EnemySoundeffects [0].Play ();
					delayTimer = damageDelay;
				}
			}
		}

		if (delayTimer > 0.0f) {
			delayTimer -= Time.deltaTime * GameManager.CTimeScale;
			
			if (delayTimer <= 0.0f)
				delayTimer = 0.0f;
		}
	}

	void OnCollisionEnter(Collision col) {
		if (currMode != Mode.Friendly) {
			gameObject.GetComponent<Rigidbody> ().velocity = Vector3.zero;

			if (col.gameObject.tag == "Player Controller") {
				attacking = true;
			}
		} else if (target == null && col.gameObject.tag != "Player Controller") {
			target = col.transform;
		}
	}

	void OnCollisionExit(Collision col) {
		if (currMode != Mode.Friendly) {
			if (col.gameObject.tag == "Player Controller") {
				attacking = false;
			}
		} else if (col.transform == target) {
			target = null;
		}
	}
}
