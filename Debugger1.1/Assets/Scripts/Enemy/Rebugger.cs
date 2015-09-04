using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Rebugger : Enemy{


	Statistics targetStats = null;
	List<GameObject> allies = new List<GameObject>();

	[SerializeField]
	float healRange = 4.0f;
	[SerializeField]
	float initialHealAmount = 3.0f;
	[SerializeField]
	float increasePerIntelligence = 1.0f;
	[SerializeField]
	float healDelay = 1.0f;
	float delayTimer = 0.0f;

	[SerializeField]
	float playerFleeDistance = 3.0f;
	[SerializeField]
	float maxFleeDistance = 4.0f;
	bool isFleeing = false;

	// Use this for initialization
	void Start () {
		UpdateStats ();
	}
	
	// Update is called once per frame
	void Update () {
		if (currMode == Mode.Patrolling)
			UpdateWaypoints ();

		RechargeShields ();

		if (currMode != Mode.Friendly) {
			if (currHealth <= 0)
				DestroyObject ();

			if (GameManager.CTimeScale == 0.0f) {
				agent.velocity = Vector3.zero;
				agent.updateRotation = false;
			}
		
			if (GameManager.CTimeScale > 0.0f && !agent.updateRotation) {
				agent.updateRotation = true;
			}

			if (target != null && !isFleeing && currMode != Mode.Deactivated) {
				agent.destination = target.position;

				if (agent.stoppingDistance < healRange - 1.0f)
					agent.stoppingDistance = healRange - 1.0f;

				if (delayTimer <= 0.0f) {
					if (!SoundManager.instance.EnemySoundeffects [3].isPlaying) {
						SoundManager.instance.EnemySoundeffects [3].Play ();
					}
					targetStats.CurrHealth += Mathf.CeilToInt (initialHealAmount + 
						increasePerIntelligence * GetComponent<Statistics> ().Intelligence);

					if (targetStats.CurrHealth > targetStats.MaxHealth)
						targetStats.CurrHealth = targetStats.MaxHealth;

					target.parent.GetComponentInChildren<EnemyHealthbar> ().UpdateFillAmount ();

					delayTimer = healDelay;

					if (targetStats.CurrHealth == targetStats.MaxHealth) {
						target = null;
						targetStats = null;
					}
				}
			}
	
			if (currMode != Mode.Deactivated) {
				Transform player = GameObject.FindGameObjectWithTag ("Player").transform;

				if (isFleeing) {
					if (agent.remainingDistance == 0 || agent.destination == null)
						isFleeing = false;

					NavMeshHit hit;
					agent.FindClosestEdge (out hit);

					if (hit.distance < 0.5f)
						isFleeing = false;
				}

				float distanceFromPlayer = Vector3.Distance (transform.position, player.position);

				if (distanceFromPlayer < playerFleeDistance && !isFleeing) {
					if (target == null || distanceFromPlayer < playerFleeDistance - healRange) {
						isFleeing = true;
						agent.stoppingDistance = 0.0f;

						Vector3 direction = (transform.position - player.position).normalized * maxFleeDistance;
						direction += transform.position;
						NavMeshHit hit;
						NavMesh.SamplePosition (direction, out hit, maxFleeDistance, 1);
		
						if (hit.hit) {
							agent.destination = hit.position;
						} else {
							int attempts = 0;
		
							while (attempts < 10) {
								direction = Random.insideUnitSphere;
								direction.y = 0;
								direction.Normalize ();
								direction = direction * maxFleeDistance + transform.position;
								NavMesh.SamplePosition (direction, out hit, maxFleeDistance, 1);
		
								if (hit.hit || attempts == 9) {
									agent.destination = hit.position;
									break;
								}
		
								attempts++;
							}
						}
					}
				}
		

				if (!isFleeing) {
					LayerMask layer = 1 << LayerMask.NameToLayer ("Enemy");
					Collider[] objects = Physics.OverlapSphere (transform.position, detectRange, layer);

					for (int index = 0; index < objects.Length; index++) {
						if (objects [index].gameObject == gameObject)
							continue;

						Statistics objStats = objects [index].GetComponentInChildren<Statistics> ();

						if (target == null && objStats.CurrHealth < objStats.MaxHealth) {
							target = objects [index].transform;
							targetStats = objStats;
						} else if (target != null && ((float)objStats.CurrHealth / objStats.MaxHealth) < ((float)targetStats.CurrHealth / targetStats.MaxHealth)) {
							target = objects [index].transform;
							targetStats = objStats;
						}
					}
				}
			}
		} else {
			if (agent.enabled == true) {
				agent.enabled = false;
			}
			
			FaceMouse ();
			
			if (target != null && delayTimer <= 0.0f) {
				if (InputManager.instance.GetButtonDown("Fire1") || InputManager.instance.GetButtonDown("Fire2")) {
					Dampener dampener = target.GetComponent<Dampener> ();

					if (dampener.Toggle == true) {
						dampener.Repair ();

						delayTimer = healDelay;
					}
				}
			}
		}

		if (delayTimer > 0.0f) {
			delayTimer -= Time.deltaTime;
			
			if (delayTimer < 0.0f)
				delayTimer = 0.0f;
		}
	}

	void OnCollisionEnter (Collision col) {
		if (currMode == Mode.Friendly && col.gameObject.tag == "Dampener") {
			target = col.transform;
		}
	}

	void OnCollisionExit (Collision col) {
		if (currMode == Mode.Friendly && target == col.transform) {
			target = null;
		}
	}
}
