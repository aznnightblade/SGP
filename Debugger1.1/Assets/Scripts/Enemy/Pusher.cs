using UnityEngine;
using System.Collections;

public class Pusher : Statistics {

	NavMeshAgent agent = null;
	Transform target = null;
	
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
		currHealth = maxHealth = initialHealth + healthPerEndurance * endurance;
		critChance = initialCrit + critPerLuck * luck;
		
		agent = gameObject.GetComponent<NavMeshAgent> ();

		flanks = GameObject.FindGameObjectsWithTag ("Melee Flank");
		target = flanks [Random.Range (0, 1)].transform;

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

		if (currHealth <= 0)
			DestroyObject ();

		if ((currFlank == 0 || currFlank == 1) && agent.remainingDistance < 1.0f) {
			int random = Mathf.CeilToInt(Random.Range(0, 100) * currHealth / MaxHealth);

			if(random <= 25) {
				target = GameObject.FindGameObjectWithTag("Player").transform;
			} else {
				target = flanks[2].transform;
			}
		} else if (currFlank == 2) {
			if(agent.remainingDistance < 1.0f) {
				target = GameObject.FindGameObjectWithTag("Player").transform;
			} else {
				int random = Mathf.CeilToInt(Random.Range(0, 100) * currHealth / MaxHealth);
			
				if(random <= 10) {
					target = GameObject.FindGameObjectWithTag("Player").transform;
				}
			}
		}

		if (currFlank != 0 || currFlank != 1) {
			if (agent.remainingDistance > resetFlankDistance)
				target = flanks [Random.Range (0, 1)].transform;
		}

		if (attackingPlayer && delayTimer <= 0.0f) {
            SoundManager.instance.EnemySoundeffects[0].Play();
			Player player = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Player>();
			
			float damage = (initialDamage + damagePerStrength * strength) - player.Defense;
			
			if(damage < 0.0f)
				damage = 0;
			
			player.DamagePlayer(Mathf.CeilToInt(damage));
			
			delayTimer = damageDelay;
		}
		
		if (delayTimer > 0.0f) {
			delayTimer -= Time.deltaTime * GameManager.CTimeScale;
			
			if (delayTimer <= 0.0f)
				delayTimer = 0.0f;
		}
	}

	public override void OnCollisionEnter(Collision col) {
		gameObject.GetComponent<Rigidbody> ().velocity = Vector3.zero;
		
		if (col.gameObject.tag == "Player Controller") {
			attackingPlayer = true;
		}
	}
	
	void OnCollisionExit(Collision col) {
		if (col.gameObject.tag == "Player Controller") {
			attackingPlayer = false;
		}
	}
}
