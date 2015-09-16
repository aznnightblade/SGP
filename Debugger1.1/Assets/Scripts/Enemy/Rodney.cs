using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Rodney : Enemy {

	enum RodneyMode { Numbers, Bricks, Enemies, Intermission };

	[SerializeField]
	BossEnemySpawning spawner = null;
	[SerializeField]
	GameObject[] ones = null;
	[SerializeField]
	GameObject[] zeros = null;
	[SerializeField]
	LazerSpawner[] lazers = null;
	int currWaypoint = -1;
	[SerializeField]
	Transform brick = null;

	[SerializeField]
	RodneyMode currRodneyMode = RodneyMode.Numbers;

	[SerializeField]
	float delayBeforeRepeat = 2.0f;
	float delayRepeatTimer = 0.0f;
	[SerializeField]
	float timeTillNextNumber = 0.25f;
	float nextNumberTimer = 0.0f;
	[SerializeField]
	float numberChangeDelay = 0.1f;
	float numberChangeTimer = 0.0f;
	int currNumber = 0;

	bool bricksSpawned = false;
	List<GameObject> bricks = new List<GameObject> ();

	bool waveSpawned = false;
	int waveCounter = 0;
	List<GameObject> spanwedEnemies = new List<GameObject> ();

	[SerializeField]
	float checkDelay = 0.25f;
	float checkTimer = 0.0f;

    bool isdead = false;
    float deathtimer = 0;
	// Use this for initialization
	void Start () {
		UpdateStats ();
        anim = gameObject.GetComponentInChildren<Animator>(); 
		target = GameObject.FindGameObjectWithTag ("Player").transform;
		delayRepeatTimer = delayBeforeRepeat;
		nextNumberTimer = timeTillNextNumber;
		waveCounter = spawner.NumberOfWaves - 1;
	}
	
	// Update is called once per frame
	void Update () {
        Death();
		if (delayRepeatTimer <= 0.0f && CheckForDestructor ()) {
			if (currRodneyMode == RodneyMode.Numbers) {
				if (nextNumberTimer <= 0.0f) {
					if (currNumber == ones.Length) {
						currRodneyMode = RodneyMode.Bricks;
					} else {
						ones[currNumber].SetActive(false);
						zeros[currNumber].SetActive(false);

						int random = Random.Range(1, 100);

						if (random <= 50 + (30 * (1.0f - (float)currHealth / maxHealth))) {
							ones[currNumber].SetActive(true);
						} else {
							zeros[currNumber].SetActive(true);
						}

						currNumber++;

						if (currNumber == ones.Length) {
							nextNumberTimer = timeTillNextNumber * 8.0f;
						} else {
							nextNumberTimer = timeTillNextNumber;
						}
					}
				} else {
					nextNumberTimer -= Time.deltaTime;
					
					if (nextNumberTimer <= 0.0f)
						nextNumberTimer = 0.0f;
				}

				if (numberChangeTimer <= 0.0f && currNumber < ones.Length) {
					for (int index = currNumber; index < ones.Length; index++) {
						ones[index].SetActive(false);
						zeros[index].SetActive(false);

						int random = Random.Range(1, 100);

						if (random <= 50) {
							ones[currNumber].SetActive(true);
						} else {
							zeros[currNumber].SetActive(true);
						}

						numberChangeTimer = numberChangeDelay;
					}
				} else if (numberChangeTimer > 0.0f) {
					numberChangeTimer -= Time.deltaTime;
					
					if (numberChangeTimer <= 0.0f)
						numberChangeTimer = 0.0f;
				}
			} else if (currRodneyMode == RodneyMode.Bricks) {
				if (!bricksSpawned) {
					for (int index = 0; index < ones.Length; index++) {
						if (ones[index].activeInHierarchy) {
                            SoundManager.instance.BossSoundeffects[9].Play();
							Vector3 pos = new Vector3(ones[index].transform.position.x, target.position.y + 1.5f, ones[index].transform.position.z); 
							SpawnBrick(pos);

							ones[index].SetActive(false);
						} else {
							zeros[index].SetActive(false);
						}

						bricksSpawned = true;
					}
				} else {
					if (checkTimer <= 0.0f) {
						for (int index = 0; index < bricks.Count; index++) {
							if (bricks[index] == null) {
								bricks.RemoveAt(index);
								index--;
							}
						}

						if (bricks.Count == 0) {
							currRodneyMode = RodneyMode.Enemies;
							bricksSpawned = false;
							currNumber = 0;
						} else {
							checkTimer = checkDelay;
						}
					} else {
						checkTimer -= Time.deltaTime;
						
						if (checkTimer <= 0.0f)
							checkTimer = 0.0f;
					}
				}
			} else if (currRodneyMode == RodneyMode.Enemies) {
				if (!waveSpawned) {
					if (waveCounter == spawner.NumberOfWaves)
						waveCounter = Mathf.FloorToInt((spawner.NumberOfWaves - 1) * ((float)currHealth / maxHealth));

					spawner.SpawnWave (waveCounter);

					GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

					for (int index = 0; index < enemies.Length; index++) {
						if (enemies[index] != gameObject && enemies[index] != transform.parent.gameObject)
							spanwedEnemies.Add(enemies[index]);
					}

					waveCounter++;
					waveSpawned = true;
				} else {
					if (checkTimer <= 0.0f) {
						for (int index = 0; index < spanwedEnemies.Count; index++) {
							if (spanwedEnemies[index] == null) {
								spanwedEnemies.RemoveAt(index);
								index--;
							}
						}

						if (spanwedEnemies.Count == 0) {
							waveSpawned = false;

							if (waveCounter < spawner.NumberOfWaves) {
								checkTimer = checkDelay;
							} else {
								currRodneyMode = RodneyMode.Intermission;
							}
						} else {
							checkTimer = checkDelay;
						}
					} else {
						checkTimer -= Time.deltaTime;
						
						if (checkTimer <= 0.0f)
							checkTimer = 0.0f;
					}
				}
			} else {
				int random = 0;

				do {
					random = Random.Range(0, Waypoints.Length - 1);
				} while (random == currWaypoint);

				agent.SetDestination(Waypoints[random].position);
				currWaypoint = random;

				delayRepeatTimer = delayBeforeRepeat;
				currRodneyMode = RodneyMode.Numbers;
			}
		} else {
			delayRepeatTimer -= Time.deltaTime;

			if (delayRepeatTimer <= 0.0f)
				delayRepeatTimer = 0.0f;
		}
	}

	void SpawnBrick (Vector3 Position) {
		bricks.Add((Instantiate (brick, Position, Quaternion.Euler (0, Random.Range (0, 360), 0)) as Transform).gameObject);
	}

	bool CheckForDestructor () {
		Player player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ();

		if (player.Friend != null && player.Friend.gameObject.name == "Destructor(Clone)(Clone)")
			return false;

		return true;
	}

	public override void Damage (int damage, Transform bullet) {
		currHealth -= damage;
		
		if (currHealth <= 0.0f) {
			SoundManager.instance.BossSoundeffects[3].Play();
			GameObject.FindGameObjectWithTag("Player").GetComponent<Player> ().HasNegationBoots = true;

			for (int index = 0; index < lazers.Length; index++)
				lazers[index].IsEnabled = false;

            isdead = true ;
		}
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
                anim.SetBool("Death", false);
                GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().EXP += EXP;
                GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().Money += Money;
            }
        }
    }
}
