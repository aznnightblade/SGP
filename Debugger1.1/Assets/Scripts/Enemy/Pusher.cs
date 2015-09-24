using UnityEngine;
using System.Collections;

public class Pusher : Enemy {
	
	[SerializeField]
	float damageDelay = 0.5f;
	float delayTimer = 0.0f;
	bool attackingPlayer = false;

	GameObject[] flanks = null;
	int currFlank = 0;
	[SerializeField]
	float resetFlankDistance = 7.0f;

	// Use this for initialization
	void Start () {
		UpdateStats ();

		if (currMode != Mode.Friendly) {
			flanks = GameObject.FindGameObjectsWithTag ("Melee Flank");
			target = flanks [Random.Range (0, 1)].transform;
		}
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

			if (currMode == Mode.Attack) {
				if ((currFlank == 0 || currFlank == 1) && agent.remainingDistance < 1.0f) {
					int random = Mathf.CeilToInt (Random.Range (0, 100) * currHealth / MaxHealth);

					if (random <= 25) {
						target = GameObject.FindGameObjectWithTag ("Player").transform;
					} else {
						target = flanks [2].transform;
					}
				} else if (currFlank == 2) {
					if (agent.remainingDistance < 1.0f) {
						target = GameObject.FindGameObjectWithTag ("Player").transform;
					} else {
						int random = Mathf.CeilToInt (Random.Range (0, 100) * currHealth / MaxHealth);
				
						if (random <= 10) {
							target = GameObject.FindGameObjectWithTag ("Player").transform;
						}
					}
				}

				if (currFlank != 0 || currFlank != 1) {
					if (agent.remainingDistance > resetFlankDistance)
						target = flanks [Random.Range (0, 1)].transform;
				}

				if (attackingPlayer && delayTimer <= 0.0f) {
					SoundManager.instance.EnemySoundeffects [0].Play ();
					Player player = GameObject.FindGameObjectWithTag ("Player").GetComponentInChildren<Player> ();
				
					float damage = (initialDamage + damagePerStrength * strength) - player.Defense;
				
					if (damage < 0.0f)
						damage = 0;
				
					player.DamagePlayer (Mathf.CeilToInt (damage));
				
					delayTimer = damageDelay;
				}
			} else if (currMode == Mode.Patrolling) {
				UpdateWaypoints ();
			}
		} else if (currMode == Mode.Friendly) {
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
		} else if (target == null && (col.gameObject.tag != "Player Controller" && col.gameObject.tag != "Untagged")) {
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
}
