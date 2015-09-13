using UnityEngine;
using System.Collections;

public class Rob : Enemy {

	[SerializeField]
	BoxCollider[] movementZones = null;
	[SerializeField]
	Lasers[] lazers = null;
	[SerializeField]
	Walls[] shieldWalls = null;
	[SerializeField]
	Teleporter[] teleporters = null;
	[SerializeField]
	Transform turret = null;

	int currMoveZone = 0;
	int hallway = 0;

	[SerializeField]
	int chargeChance = 20;
	[SerializeField]
	float chargeSpeed = 50;
	float nonChargeSpeed = 0.0f;
	[SerializeField]
	float chargeDelay = 0.35f;
	float chargeTimer = 0.0f;
	[SerializeField]
	float chargeUpTime = 1.0f;
	float chargeUpTimer = 0.0f;
	[SerializeField]
	int minCharges = 2;
	int totalCharges = 0;
	bool isCharging = false;

	[SerializeField]
	int minTurrets = 4;
	int totalTurrets = 0;
	[SerializeField]
	int turretSpawnChance = 45;
	[SerializeField]
	float spawnTurretDelay = 0.85f;
	float spawnTurretTime = 0.0f;

	[SerializeField]
	int transformChance = 50;

	float enterRoomHealthPercent = 1.0f;
	[SerializeField]
	float healthLossTillMove = 0.1f;
	[SerializeField]
	float timeTillMove = 120.0f;
	float MoveTime = 0.0f;

	Player player = null;

	[SerializeField]
	bool onGround = true;
	bool movingToNextRoom = false;
	bool forceUpdateDestination = false;

    bool isdead = false;
    float deathtimer = 0;
	// Use this for initialization
	void Start () {
		UpdateStats ();

		anim = gameObject.GetComponentInChildren<Animator> ();
		nonChargeSpeed = agent.speed;
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ();
		target = GameObject.FindGameObjectWithTag ("Player").transform;
		chargeTimer = chargeDelay;
		MoveTime = timeTillMove;

		for (int hallway = 0; hallway < lazers.Length; hallway++) {
			for (int lazer = 0; lazer < lazers[hallway].Lazers.Length; lazer++) {
				lazers[hallway].Lazers[lazer].transform.parent.gameObject.SetActive(false);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
        Death();
		if (agent.remainingDistance <= 1.0f && Random.Range(1, 100) <= 50 && !movingToNextRoom || forceUpdateDestination) {
			NavMeshHit hit;
			float maxDistance = 0.0f;

			do {
				Vector3 newDestination = Random.insideUnitSphere;
				Vector3 movementBounds = movementZones[currMoveZone].bounds.size * 0.5f;
				newDestination = new Vector3(movementBounds.x * newDestination.x + movementZones[currMoveZone].transform.position.x,
											 transform.position.y,
				                             movementBounds.z * newDestination.z + movementZones[currMoveZone].transform.position.z);

				NavMesh.SamplePosition(newDestination, out hit, maxDistance, 1);

				maxDistance += 1.0f;
			} while (hit.hit == false);

			if (forceUpdateDestination)
				forceUpdateDestination = false;

			agent.SetDestination(hit.position);
		}
		if (!movingToNextRoom) {
			if (!onGround) {
				if (spawnTurretTime <= 0.0f) {
					if (Random.Range (1, 100) <= turretSpawnChance) {
						SphereCollider[] collider = turret.GetComponentsInChildren<SphereCollider> (true);
						float distanceFromBoss = collider[0].radius + gameObject.GetComponent<SphereCollider> ().radius;
						float rot = (transform.rotation.eulerAngles.y + 90.0f) * (Mathf.PI / 180.0f);
						Vector3 direction = new Vector3 (-Mathf.Cos (rot), 0, Mathf.Sin (rot));
						direction = -direction * distanceFromBoss + transform.position;
						direction.y = player.gameObject.transform.position.y;
						Collider[] collisions = Physics.OverlapSphere(direction, collider[0].radius);

						bool createTurret = true;
						if (collisions.Length > 0) {
							for (int index = 0; index < collisions.Length; index++) {
								if (collisions[index].tag == "Player" || collisions[index].tag == "Player Controller" ||
								    collisions[index].tag == "Enemy" || collisions[index].tag == "Wall" || collisions[index].tag == "ShieldWall") {
									createTurret = false;
									break;
								}
							}
						}

						if (createTurret) {
							GameObject newEnemy = (Instantiate(turret, direction, Quaternion.identity) as Transform).gameObject;
							newEnemy.GetComponentInChildren<Enemy> ().CurrColor = (DLLColor.Color)Random.Range(0, (int)DLLColor.Color.BLUE);
							totalTurrets++;
						}
					}
			
					if (totalTurrets >= minTurrets && Random.Range (1, 100) <= transformChance) {
						anim.SetBool ("Flying", false);
						gameObject.GetComponent<SphereCollider> ().isTrigger = false;
						onGround = true;
						totalTurrets = 0;
					}

					spawnTurretTime = spawnTurretDelay;
				}
			} else {
				if (chargeTimer <= 0.0f && !isCharging) {
					if (Random.Range(1, 100) <= chargeChance) {
						isCharging = true;

						agent.speed = 0.0f;
						agent.velocity = Vector3.zero;
						agent.updateRotation = false;
					}

					if (totalCharges >= minCharges && Random.Range (1, 100) <= transformChance) {
						anim.SetBool ("Flying", true);
						gameObject.GetComponent<SphereCollider> ().isTrigger = true;
						onGround = false;
						totalCharges = 0;
					}

					chargeTimer = chargeDelay;
				}

				if (isCharging) {
					if (chargeUpTimer < chargeUpTime) {
						FacePlayer ();

						chargeUpTimer += Time.deltaTime;
					} else {
						gameObject.GetComponent<SphereCollider> ().isTrigger = true;

						float rot = (transform.rotation.eulerAngles.y + 90.0f) * (Mathf.PI / 180.0f);
						Vector3 direction = new Vector3 (-Mathf.Cos (rot), 0, Mathf.Sin (rot));

						gameObject.GetComponent<Rigidbody> ().velocity = direction * chargeSpeed;

						chargeUpTimer = 0.0f;
						totalCharges++;
						isCharging = false;
					}
				}
			}
		}

		if (!isCharging) {
			if (timeTillMove <= 0.0f || ((float)currHealth / maxHealth) <= (enterRoomHealthPercent - healthLossTillMove)) {
				timeTillMove = MoveTime;
				enterRoomHealthPercent = (float)currHealth / maxHealth;
				anim.SetBool("Flying", true);
				gameObject.GetComponent<SphereCollider> ().enabled = false;
				gameObject.GetComponent<SphereCollider> ().isTrigger = true;
				onGround = false;
				movingToNextRoom = true;

				if (Random.Range(1, 100) <= 50) {
					currMoveZone++;

					if (currMoveZone >= movementZones.Length)
						currMoveZone = 0;

					hallway = currMoveZone;
				} else {
					hallway = currMoveZone;

					currMoveZone--;

					if (currMoveZone < 0)
						currMoveZone = movementZones.Length - 1;
				}

				for (int index = 0; index < shieldWalls[hallway].walls.Length; index++)
					shieldWalls[hallway].walls[index].SetActive(false);

				ActivateLasers (hallway);

				agent.SetDestination(movementZones[currMoveZone].transform.position);
			}
		}

		if (movingToNextRoom) {
			if (agent.remainingDistance <= 1.0f && movementZones[currMoveZone].GetComponent<BossSpawnArea> ().ContainsPlayer) {
				movingToNextRoom = false;
				forceUpdateDestination = true;
				gameObject.GetComponent<SphereCollider> ().enabled = true;

				for (int index = 0; index < shieldWalls[hallway].walls.Length; index++)
					shieldWalls[hallway].walls[index].SetActive(true);
			}
		}

		if (chargeTimer > 0.0f) {
			chargeTimer -= Time.deltaTime;
			
			if (chargeTimer < 0.0f)
				chargeTimer = 0.0f;
		}

		if (timeTillMove > 0.0f && !movingToNextRoom) {
			timeTillMove -= Time.deltaTime;

			if (timeTillMove < 0.0f)
				timeTillMove = 0.0f;
		}

		if (spawnTurretTime > 0.0f) {
			spawnTurretTime -= Time.deltaTime;
			
			if (spawnTurretTime < 0.0f)
				spawnTurretTime = 0.0f;
		}
	}

	public override void Damage (int damageTaken, Transform bullet) {
		currHealth -= damageTaken;

		if (currHealth <= 0) {
			for (int index = 0; index < teleporters.Length; index++)
				teleporters[index].gameObject.SetActive (true);

			for (int hallway = 0; hallway < lazers.Length; hallway++) {
				for (int lazer = 0; lazer < lazers[hallway].Lazers.Length; lazer++) {
					lazers[hallway].Lazers[lazer].transform.parent.gameObject.SetActive(false);
				}
			}

			SoundManager.instance.BossSoundeffects[3].Play();

            isdead = true;
		}
	}

	void OnTriggerEnter (Collider col) {
		if (col.tag == "Player" && onGround) {
			int damage = Mathf.CeilToInt((initialDamage + damagePerStrength * Strength) * 1.5f - player.Defense);

			if (damage < 5)
				damage = 5;

			player.DamagePlayer(damage);
		}

		if (col.tag == "Wall" || col.tag == "ShieldWall") {
			gameObject.GetComponent<Rigidbody> ().velocity = Vector3.zero;
			gameObject.GetComponent<SphereCollider> ().isTrigger = false;
			agent.speed = nonChargeSpeed;
			agent.updateRotation = true;
			forceUpdateDestination = true;
		}
	}

	void ActivateLasers (int currentHallway) {
		int lazersActive = 0;
		int lazersToActivate = Mathf.CeilToInt ((1 - (currHealth / maxHealth)) * lazers [currentHallway].Lazers.Length);

		if (lazersToActivate == 0)
			lazersToActivate = 1;
		else if (lazersToActivate >= lazers [currentHallway].Lazers.Length)
			lazersToActivate = lazers [currentHallway].Lazers.Length - 1;

		for (int lazer = 0; lazer < lazers [currentHallway].Lazers.Length; lazer++) {
			if (lazers [currentHallway].Lazers [lazer].transform.parent.gameObject.activeInHierarchy)
				lazersActive++;
		}

		lazersToActivate -= lazersActive;

		while (lazersToActivate > 0) {
			int random = Random.Range(0, lazers [currentHallway].Lazers.Length);

			if (!lazers [currentHallway].Lazers [random].transform.parent.gameObject.activeInHierarchy) {
				lazers [currentHallway].Lazers [random].transform.parent.gameObject.SetActive(true);
				lazersToActivate--;
			}
		}
	}
    public override void Death()
    {
        if (isdead == true)
        {
            anim.SetBool("Death", true);
            deathtimer += Time.deltaTime;
            if (deathtimer >= 1.0f)
            {
                DestroyObject();
                isdead = false;
                deathtimer = 0;
                anim.SetBool("Death", false);
            }
        }
    }
}

[System.Serializable]
public struct Lasers {
	public LazerSpawner[] Lazers;
}

[System.Serializable]
public struct Walls {
	public GameObject[] walls;
}