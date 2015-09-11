using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FireWaller : Enemy {

	List<GameObject> allies = new List<GameObject>();

	[SerializeField]
	float initialShieldAmount = 20.0f;
	[SerializeField]
	float increasePerIntelligence = 2.0f;

	[SerializeField]
	float damageDelay = 0.5f;
	float delayTimer = 0.0f;
	bool attackingPlayer = false;

	// Use this for initialization
	void Start () {
		UpdateStats ();
	}
	
	// Update is called once per frame
	void Update () {
        Death();
		if (currMode == Mode.Patrolling)
			UpdateWaypoints ();
		
		if (currMode == Mode.Attack || currMode == Mode.Patrolling || currMode == Mode.BossRoom)
			agent.destination = target.position;

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
			} else if (currMode != Mode.Deactivated) {
				CheckForPlayer ();
			}

			if (currMode == Mode.Patrolling)
				UpdateWaypoints ();
		
			if (attackingPlayer && delayTimer <= 0.0f) {
				Player player = GameObject.FindGameObjectWithTag ("Player").GetComponentInChildren<Player> ();
			
				float damage = (initialDamage + damagePerStrength * strength) - player.Defense;
			
				if (damage < 0.0f)
					damage = 0;
			
				player.DamagePlayer (Mathf.CeilToInt (damage));
			
				delayTimer = damageDelay;
			}
		} else {
			if (agent.enabled == true) {
				agent.enabled = false;
			}

			FaceMouse ();
			
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
				attackingPlayer = true;
			}
		} else if (target == null && col.gameObject.tag != "Player Controller") {
			target = col.transform;
		}
	}
	
	void OnCollisionExit(Collision col) {
		if (currMode != Mode.Friendly) {
			if (col.gameObject.tag == "Player Controller") {
				attackingPlayer = false;
			}
		} else if (col.transform == target) {
			target = null;
		}
	}

	void OnTriggerEnter (Collider col) {
		if (currMode != Mode.Friendly || currMode != Mode.Deactivated) {
			if (col.gameObject.tag == "Enemy") {
				if (!SoundManager.instance.EnemySoundeffects [2].isPlaying) {
					SoundManager.instance.EnemySoundeffects [2].Play ();
				}
				col.GetComponent<Statistics> ().MaxShield = Mathf.CeilToInt (initialShieldAmount + increasePerIntelligence * gameObject.GetComponent<Statistics> ().Intelligence);
				allies.Add (col.gameObject);
			}
		}
	}

	void OnTriggerExit (Collider col) {
		if (currMode != Mode.Friendly || currMode != Mode.Deactivated) {
			if (col.gameObject.tag == "Enemy") {
				if (col.gameObject.name != "FireWaller" && col.gameObject.name != "Shielded Pusher")
					col.GetComponent<Statistics> ().MaxShield = col.GetComponent<Statistics> ().Shield = 0;
				else {
					col.GetComponent<Statistics> ().MaxShield -= Mathf.CeilToInt (initialShieldAmount + increasePerIntelligence * gameObject.GetComponent<Statistics> ().Intelligence);

					if (col.GetComponent<Statistics> ().Shield > col.GetComponent<Statistics> ().MaxShield)
						col.GetComponent<Statistics> ().Shield = col.GetComponent<Statistics> ().MaxShield;
				}
				allies.Remove (col.gameObject);
			}
		}
	}

	public void RemoveShields () {
		if (currMode != Mode.Friendly || currMode != Mode.Deactivated) {
			for (int index = 0; index < allies.Count; index ++) {
				if (allies [index] == null)
					continue;

				if (allies [index].name != "FireWaller" && allies [index].name != "Shielded Pusher")
					allies [index].GetComponent<Statistics> ().MaxShield = allies [index].GetComponent<Statistics> ().Shield = 0;
				else {
					allies [index].GetComponent<Statistics> ().MaxShield -= Mathf.CeilToInt (initialShieldAmount + increasePerIntelligence * gameObject.GetComponent<Statistics> ().Intelligence);
			
					if (allies [index].GetComponent<Statistics> ().Shield > allies [index].GetComponent<Statistics> ().MaxShield)
						allies [index].GetComponent<Statistics> ().Shield = allies [index].GetComponent<Statistics> ().MaxShield;
				}

				allies [index].transform.parent.GetComponentInChildren<EnemyShieldbar> ().UpdateFillAmount ();
			}
		}
	}
}
