using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Aurthor : Enemy {

	[SerializeField]
	Transform bullet = null;
	[SerializeField]
	BoxCollider teleportZone = null;
	[SerializeField]
	BossEnemySpawning[] spawners = null;
	[SerializeField]
	Walls[] stageWalls = null;
	[SerializeField]
	WallZones[] stageWallZones = null;
	[SerializeField]
	GameObject companionCube = null;

	[SerializeField]
	int lives = 4;
	[SerializeField]
	int currStage = 0;

	List<GameObject> enemies = new List<GameObject> ();
	Player player = null;

	[SerializeField]
	float checkForPlayerDelay = 0.25f;
	float checkTimer = 0.0f;
	bool wallsChanged = true;

	[SerializeField]
	float[] spawnDelays = null;
	float[] spawnTimers = null;
	[SerializeField]
	float checkForEnemiesDelay = 10.0f;
	float checkEnemiesTimer = 0.0f;

	float shotTimer = 0.0f;

	[SerializeField]
	float teleportDelay = 2.0f;
	float teleportTimer = 0.0f;

	[SerializeField]
	float buffDebuffTime = 5.0f;
	float buffDebuffTimer = 0.0f;
	int playerInitStrength = 0;
	float playerInitVel = 0;

    bool isdead =false;
    float deathtimer = 0;
	// Use this for initialization
	void Start () {
		UpdateStats ();
        anim = gameObject.GetComponentInChildren<Animator>();
		target = GameObject.FindGameObjectWithTag ("Player").transform;
		player = target.GetComponent<Player> ();
		playerInitStrength = player.Strength;
		playerInitVel = player.MaxVelocity;
		teleportTimer = 2.0f;

		spawnTimers = new float[spawnDelays.Length];
		for (int timer = 0; timer < spawnTimers.Length; timer++)
			spawnTimers [timer] = 3.0f;
	}
	
	// Update is called once per frame
	void Update () {
        Death();
		UpdateTimers ();

		FacePlayer ();

		if (!wallsChanged && checkTimer <= 0.0f) {
			if(!PlayerCheck()) {
				UpdateWalls ();
			}

			checkTimer = checkForPlayerDelay;
		}

		if (spawnTimers [currStage] <= 0.0f) {
			spawners[currStage].SpawnWave ();
			spawnTimers [currStage] = spawnDelays[currStage];

			if ((spawners[currStage].WaveCounter >= spawners[currStage].NumberOfWaves - 1 && currStage < 3) ||
			    currStage == 0)
				checkEnemiesTimer = checkForEnemiesDelay;
		}

		if (currStage > 2 && shotTimer <= 0.0f) {
			float shotDistance = initialShotDistance + ShotDistancePerDexerity * dexterity;

			if (Vector3.Distance(transform.position, target.position) <= shotDistance) {
				float rot = transform.rotation.eulerAngles.y;

				for (int currBullet = 0; currBullet < 5; currBullet++) {
					CreateBullet (rot + (-15 + (7.5f * currBullet)));
				}

				shotTimer = shotDelay;
			}
		}
	}

	void UpdateTimers () {
		if (checkTimer > 0.0f) {
			checkTimer -= Time.deltaTime;
			
			if (checkTimer < 0.0f)
				checkTimer = 0.0f;
		}

		if (shotTimer > 0.0f) {
			shotTimer -= Time.deltaTime;
			
			if (shotTimer < 0.0f)
				shotTimer = 0.0f;
		}

		if (buffDebuffTimer > 0.0f) {
			buffDebuffTimer -= Time.deltaTime;
			
			if (buffDebuffTimer <= 0.0f) {
				buffDebuffTimer = 0.0f;

				player.Strength = playerInitStrength;
				player.MaxVelocity = playerInitVel;
			}
		}

		if (spawnTimers [currStage] > 0.0f) {
			spawnTimers [currStage] -= Time.deltaTime;
			
			if (spawnTimers [currStage] <= 0.0f) {
				spawnTimers [currStage] = 0.0f;
			}
		}

		if (checkEnemiesTimer > 0.0f) {
			checkEnemiesTimer -= Time.deltaTime;
			
			if (checkEnemiesTimer <= 0.0f) {
				checkEnemiesTimer = 0.0f;

				CheckForEnemies ();
			}
		}

		if (currStage > 0) {
			if (teleportTimer > 0.0f) {
				teleportTimer -= Time.deltaTime;
			
				if (teleportTimer < 0.0f) {
					teleportTimer = 0.0f;

					Teleport ();
				}
			}
		}
	}

	public override void Damage (int damageTaken, Transform bullet) {
		currHealth -= damageTaken;

		if (currHealth <= 0) {
			currStage++;

			if (lives > 0) {
				lives--;

				currHealth = maxHealth;
				wallsChanged = false;

				SoundManager.instance.BossSoundeffects[3].Play();
			} else {
				companionCube.SetActive(true);
				UpdateWalls ();

				SoundManager.instance.BossSoundeffects[3].Play();

                isdead = true;
			}
		}
	}

	void Teleport () {
		NavMeshHit hit;
		Vector3 teleportLocation = Random.insideUnitSphere;
		Vector3 teleportBounds = teleportZone.bounds.size * 0.5f;
		teleportLocation = new Vector3 (teleportLocation.x * teleportBounds.x + teleportZone.transform.position.x, transform.position.y,
		                                teleportLocation.z * teleportBounds.z + teleportZone.transform.position.z);

		for (float maxDistance = 0.0f;; maxDistance += 1.0f) {
			NavMesh.SamplePosition(teleportLocation, out hit, maxDistance, 1);

			if (hit.hit)
				break;
		}

		agent.Warp (hit.position);
		teleportTimer = teleportDelay;
	}

	bool PlayerCheck () {
		for (int wall = 0; wall < stageWallZones[currStage].zones.Length; wall++) {
			if (stageWallZones[currStage].zones[wall].ContainsPlayer)
				return true;
		}
		
		return false;
	}

	void CheckForEnemies () {
		GameObject[] spawnedEnemies = GameObject.FindGameObjectsWithTag("Enemy");
		enemies.Clear ();
		
		for (int index = 0; index < spawnedEnemies.Length; index++) {
			if (spawnedEnemies[index] != gameObject && spawnedEnemies[index] != transform.parent.gameObject)
				enemies.Add(spawnedEnemies[index]);
		}

		if (enemies.Count > 0) {
			DebuffPlayer ();
		} else {
			BuffPlayer ();
		}
	}

	void BuffPlayer () {
		if (currStage == 0 || currStage == 1) {
			player.Strength = Mathf.CeilToInt(player.Strength * 1.5f);
		} else if (currStage == 2 || currStage == 3) {
			player.MaxVelocity *= 1.5f;
		}

		buffDebuffTimer = buffDebuffTime;
	}

	public void DebuffPlayer () {
		player.Strength = Mathf.FloorToInt(playerInitStrength * 0.5f);

		if (currStage > 1) {
			player.MaxVelocity = playerInitVel * 0.5f;
		}

		buffDebuffTimer = buffDebuffTime;
	}

	void UpdateWalls () {
		for (int wall = 0; wall < stageWalls[currStage - 1].walls.Length; wall++) {
			stageWalls[currStage - 1].walls[wall].SetActive(false);
		}

		if (currStage < stageWalls.Length) {
			for (int wall = 0; wall < stageWalls[currStage].walls.Length; wall++) {
				stageWalls [currStage].walls [wall].SetActive (false);
			}
		}

		wallsChanged = true;
	}

	void CreateBullet (float rot) {
		GameObject newBullet = (Instantiate (bullet, transform.position, Quaternion.Euler(0, rot, 0)) as Transform).gameObject;
		SoundManager.instance.BossSoundeffects[5].Play();
		newBullet.tag = ("Enemy Bullet");
		newBullet.GetComponent<Weapon> ().Owner = this;
	}

    public override void Death()
    {
        if (isdead == true)
        {
            anim.SetBool("Death", true);
            deathtimer += Time.deltaTime;
            if (deathtimer >= 1.2f)
            {
                DestroyObject();
                isdead = false;
                deathtimer = 0;
                GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().EXP += EXP;
                GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().Money += Money;
                anim.SetBool("Death", false);
            }
        }
    }
}

[System.Serializable]
public struct WallZones {
	public BossSpawnArea[] zones;
}