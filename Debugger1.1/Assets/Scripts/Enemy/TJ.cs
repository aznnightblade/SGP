using UnityEngine;
using System.Collections;

public class TJ : Enemy {

	[SerializeField]
	Transform bullet = null;
	Weapon bulletScript = null;
	[SerializeField]
	GameObject teleporter = null;

	[SerializeField]
	float percentTillTeleport = 0.2f;
	float lastTeleportHealth = 1.0f;
	BossEnemySpawning spawner = null;
	[SerializeField]
	Transform[] rooms = null;
	int currentRoom = 4;

	[SerializeField]
	int DeathBlossomChance = 20;
	[SerializeField]
	int bulletsInBlossom = 24;

	bool isActive = false;
	bool playerNear = false;
    bool isdead = false;
    float deathtimer = 0;
	// Use this for initialization
	void Start () {
		UpdateStats ();
        anim = gameObject.GetComponentInChildren<Animator>();
		spawner = gameObject.GetComponent<BossEnemySpawning> ();
		bulletScript = bullet.GetComponent<Weapon> ();
		shotDelay = (bulletScript.ShotDelay - bulletScript.ShotDelayReductionPerAgility * Agility) * 3;
		target = GameObject.FindGameObjectWithTag ("Player").transform;
		agent.updatePosition = false;
		agent.updateRotation = false;
	}
	
	// Update is called once per frame
	void Update () {
        Death();
		if (isActive) {
			if ((float)currHealth / maxHealth <= lastTeleportHealth - percentTillTeleport) {
				do {
					int room = Random.Range (0, rooms.Length - 1);

					if (room != currentRoom) {
						currentRoom = room;

						Vector3 newPosition = rooms[currentRoom].position;
						newPosition.y = transform.position.y;
						transform.parent.transform.position = new Vector3 (newPosition.x, transform.position.y, newPosition.z);
						shotDelay = (bulletScript.ShotDelay - bulletScript.ShotDelayReductionPerAgility * Agility) * 3;
						transform.rotation = Quaternion.identity;
						break;
					}
				} while (true);

				lastTeleportHealth -= percentTillTeleport;
			}

			if (playerNear) {
				FacePlayer ();

				if (shotDelay <= 0.0f) {
					int random = Random.Range (1, 100);

					if (random <= DeathBlossomChance && (float)currHealth / maxHealth <= 0.4f) {
						float rot = transform.rotation.eulerAngles.y;
						float angleIncease = 360 / bulletsInBlossom;

						for (int bullet = 0; bullet < bulletsInBlossom; bullet++) {
							CreateBullet (transform.position, Quaternion.Euler(0, rot + (angleIncease * bullet), 0));
						}
					} else {
						FireBullets ();
					}

					shotDelay = bulletScript.InitialShotDelay - bulletScript.ShotDelayReductionPerAgility * Agility;
				}
			}

			if (spawner.SpawnPointScripts[currentRoom].ContainsPlayer && !playerNear) {
				playerNear = true;
				spawner.enabled = false;
			} else if (playerNear) {
				playerNear = false;
				spawner.enabled = true;
			}

			if (shotDelay > 0.0f) {
				shotDelay -= Time.deltaTime * GameManager.CTimeScale;

				if (shotDelay <= 0.0f)
					shotDelay = 0.0f;
			}
		}
	}

	void FireBullets () {
		float rot = transform.rotation.eulerAngles.y;
		float healthPercent = 1 - (float)currHealth / maxHealth;
		int bulletsToFire = 0;
        SoundManager.instance.EnemySoundeffects[1].Play();
		int random = Random.Range (1, 100);

		if (random <= 10 + (20 * healthPercent)) {
			bulletsToFire = 4;
		} else if (random <= 20 + (30 * healthPercent)) {
			bulletsToFire = 3;
		} else if (random <= 30 + (40 * healthPercent)) {
			bulletsToFire = 2;
		} else {
			bulletsToFire = 1;
		}

		switch (bulletsToFire) {
		case 1:
			CreateBullet(transform.position, Quaternion.Euler(0, rot, 0));
			break;
		case 2:
			CreateBullet(transform.position, Quaternion.Euler(0, rot + 3, 0));
			CreateBullet(transform.position, Quaternion.Euler(0, rot - 3, 0));
			break;
		case 3:
			CreateBullet(transform.position, Quaternion.Euler(0, rot, 0));
			CreateBullet(transform.position, Quaternion.Euler(0, rot + 4, 0));
			CreateBullet(transform.position, Quaternion.Euler(0, rot - 4, 0));
			break;
		case 4:
			CreateBullet(transform.position, Quaternion.Euler(0, rot + 6, 0));
			CreateBullet(transform.position, Quaternion.Euler(0, rot + 3, 0));
			CreateBullet(transform.position, Quaternion.Euler(0, rot - 3, 0));
			CreateBullet(transform.position, Quaternion.Euler(0, rot - 6, 0));
			break;
		}
	}

	void CreateBullet (Vector3 pos, Quaternion rot) {
		GameObject newBullet = (Instantiate (bullet, pos, rot) as Transform).gameObject;
		SoundManager.instance.BossSoundeffects[5].Play();
		newBullet.tag = ("Enemy Bullet");
		newBullet.GetComponent<Weapon> ().Owner = this;
	}

	public override void Damage (int damage, Transform bullet) {
		currHealth -= damage;
		
		if (currHealth <= 0.0f) {
			teleporter.SetActive (true);
			SoundManager.instance.BossSoundeffects[3].Play();
			GameObject.FindGameObjectWithTag("Player").GetComponent<Player> ().HasDLLs = true;

            isdead = true;
		}
	}

	void OnTriggerEnter (Collider col) {
		if (col.tag == "Player" || col.tag == "Player Controller") {
			if (!isActive) {
				isActive = true;
			}
		}

		if (col.tag == "Dampener") {
			Dampener dampener = col.gameObject.GetComponent<Dampener> ();

			if (dampener.Toggle == true) {
				dampener.Repair();
			}
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
            }
        }
    }
}
