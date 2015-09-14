using UnityEngine;
using System.Collections;

public class Frosty : Enemy {

	[SerializeField]
	Derek derek = null;
	[SerializeField]
	Transform Bullet = null;
	[SerializeField]
	GameObject companionCube = null;
	[SerializeField]
	BoxCollider movementZone = null;

	[SerializeField]
	float delayTillBulletHell = 2.0f;
	float bulletHellDelayTimer = 0.0f;
	[SerializeField]
	float bulletHellBulletSpeed = 4.0f;
	[SerializeField]
	float bulletsShotPerWave = 30;
	[SerializeField]
	float rotIncrease = 3.0f;
	int shotCount = 0;
	float shotTimer = 0.0f;

	bool deletedBullets = false;
    bool isdead = false;
    float deathtimer = 0;
	// Use this for initialization
	void Start () {
		UpdateStats ();
        anim = gameObject.GetComponentInChildren<Animator>();
		agent = gameObject.GetComponent<NavMeshAgent> ();
		target = GameObject.FindGameObjectWithTag ("Player").transform;
		BulletHellDelayTimer = delayTillBulletHell;
	}
	
	// Update is called once per frame
	void Update () {
        Death();
		UpdateTimers ();

		if (derek.IsDead) {
			agent.updateRotation = false;
			agent.SetDestination(transform.position);

			if (bulletHellDelayTimer <= 0.0f) {
				if (shotTimer <= 0.0f)
					BulletHell ();
			}
		} else {
			if (derek.IsReviving) {
				agent.updatePosition = true;
				agent.updateRotation = true;
				shotCount = 0;
			}

			if (!derek.IsReviving && !deletedBullets) {
				GameObject[] bullets = GameObject.FindGameObjectsWithTag("Enemy Bullet");

				for (int bullet = 0; bullet < bullets.Length; bullet++) {
					Destroy(bullets[bullet]);
				}

				shotCount = 0;
			}

			if (agent.remainingDistance < 1.0f) {
				NavMeshHit hit;
				float maxDistance = 0.0f;
				
				do {
					Vector3 newDestination = Random.insideUnitSphere;
					newDestination = new Vector3(movementZone.bounds.size.x * newDestination.x + movementZone.transform.position.x,
					                             transform.position.y,
					                             movementZone.bounds.size.z * newDestination.z + movementZone.transform.position.z);
					
					NavMesh.SamplePosition(newDestination, out hit, maxDistance, 1);
					
					maxDistance += 1.0f;
				} while (hit.hit == false);

				agent.SetDestination(hit.position);
			}
		}
	}

	void BulletHell () {
		float rotationPerBullet = 360 / bulletsShotPerWave;
		Vector3 pos = new Vector3 (transform.position.x, target.position.y, transform.position.z);

		for (int bullet = 0; bullet < bulletsShotPerWave; bullet++) {
			GameObject newBullet = (Instantiate(Bullet, pos,
			                        Quaternion.Euler(0, rotIncrease * shotCount + rotationPerBullet * bullet, 0)) as Transform).gameObject;
			newBullet.GetComponent<Weapon> ().Velocity = bulletHellBulletSpeed;
			newBullet.tag = ("Enemy Bullet");
			newBullet.GetComponent<Weapon> ().Owner = this;
		}

		SoundManager.instance.BossSoundeffects[5].Play();
		shotCount++;
		shotTimer = shotDelay;
	}

	void UpdateTimers () {
		if (bulletHellDelayTimer > 0.0f) {
			bulletHellDelayTimer -= Time.deltaTime;

			if (bulletHellDelayTimer < 0.0f)
				bulletHellDelayTimer = 0.0f;
		}

		if (shotTimer > 0.0f) {
			shotTimer -= Time.deltaTime;
			
			if (shotTimer < 0.0f)
				shotTimer = 0.0f;
		}
	}

	public override void Damage (int damageTaken, Transform bullet) {
		if (!derek.IsDead && bulletHellDelayTimer <= 1.0f) {
			damageTaken = 0;
			SoundManager.instance.MiscSoundeffects [6].Play ();
		}

		currHealth -= damageTaken;
		
		if (currHealth <= 0) {
			companionCube.SetActive(true);
			
			SoundManager.instance.BossSoundeffects[3].Play();

			GameObject[] bullets = GameObject.FindGameObjectsWithTag("Enemy Bullet");

			for (int currBullet = 0; currBullet < bullets.Length; currBullet++) {
				Destroy(bullets[currBullet]);
			}

			derek.DestroyDerek ();
            isdead = true;
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
                GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().EXP += 800;
                GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().Money += 1000;
            }
        }
    }
	public float BulletHellDelayTimer {
		get { return bulletHellDelayTimer; }
		set { bulletHellDelayTimer = value; }
	}
	public float DelayTillBulletHell {
		get { return delayTillBulletHell; }
		set { delayTillBulletHell = value; }
	}
}
