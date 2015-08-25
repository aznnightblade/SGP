using UnityEngine;
using System.Collections;

public class ModerateRanged : Statistics {

	NavMeshAgent agent = null;
	Transform target = null;
	Transform player = null;
	
	[SerializeField]
	Transform bullet = null;
	
	float delayTimer = 0.0f;
	[SerializeField]
	float maximumShotDistance = 7.0f;

	GameObject[] flanks = null;
	bool clockwise = false;
	int currFlank = 0;
	int prevFlank = -1;

	// Use this for initialization
	void Start () {
		currHealth = maxHealth = initialHealth + healthPerEndurance * endurance;
		critChance = initialCrit + critPerLuck * luck;
		
		agent = gameObject.GetComponent<NavMeshAgent> ();
		player = GameObject.FindGameObjectWithTag ("Player").transform;

		flanks = GameObject.FindGameObjectsWithTag ("Ranged Flank");

		int random = Random.Range (0, 1);

		if (random == 0) {
			target = flanks[0].transform;
			prevFlank = 3;
			currFlank = 0;
		} else {
			target = flanks[2].transform;
			clockwise = true;
			prevFlank = 3;
			currFlank = 2;
		}

		agent.destination = target.position;
	}
	
	// Update is called once per frame
	void Update () {
		agent.destination = target.position;

		RechargeShields ();

		if (GameManager.CTimeScale == 0.0f) {
			agent.velocity = Vector3.zero;
			agent.updateRotation = false;
		}
		
		if (GameManager.CTimeScale > 0.0f && !agent.updateRotation) {
			agent.updateRotation = true;
		}

		if (agent.remainingDistance < 1.0f) {
			if (clockwise) {
				if (currFlank == flanks.Length - 1) {
					prevFlank = currFlank;
					currFlank = 0;
				} else {
					prevFlank = currFlank;
					currFlank++;
				}

				target = flanks[currFlank].transform;
			} else {
				if (currFlank == 0) {
					prevFlank = currFlank;
					currFlank = flanks.Length - 1;
				} else {
					prevFlank = currFlank;
					currFlank--;
				}

				target = flanks[currFlank].transform;
			}
		}

		for (int index = 0; index < flanks.Length; index++) {
			if(index == currFlank || index == prevFlank)
				continue;

			if (Vector3.Distance(transform.position, flanks[index].transform.position) < agent.remainingDistance) {
				target = flanks[index].transform;
				break;
			}
		}

		if (Vector3.Distance (transform.position, target.position) <= maximumShotDistance && delayTimer <= 0.0f) {
			FireBullet();
			
			delayTimer = shotDelay;
		}
		
		if (delayTimer > 0.0f) {
			delayTimer -= Time.deltaTime * GameManager.CTimeScale;
			
			if( delayTimer < 0.0f)
				delayTimer = 0.0f;
		}

        if (currHealth <= 0.0f)
        {
            sounds.EnemySoundeffects[6].Play();
            DestroyObject();
        }
	}

	void FireBullet() {
		Vector3 pos = transform.position;
		Vector3 direction = pos - player.position;
		float rot = -((Mathf.Atan2(direction.z, direction.x) * 180 / Mathf.PI) + 90.0f);
        sounds.EnemySoundeffects[1].Play();
		GameObject newBullet = (Instantiate (bullet, pos, Quaternion.Euler(0, rot, 0)) as Transform).gameObject;
		newBullet.tag = ("Enemy Bullet");
		newBullet.GetComponent<Weapon> ().Owner = this;
		newBullet.GetComponent<Weapon> ().OwnerMoveDirection = Vector3.zero;
	}
}
