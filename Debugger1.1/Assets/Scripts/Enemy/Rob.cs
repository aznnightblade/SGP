using UnityEngine;
using System.Collections;

public class Rob : Enemy {

	[SerializeField]
	BoxCollider[] movementZones = null;
	[SerializeField]
	Lasers[] lazers = null;
	[SerializeField]
	Walls[] shieldWalls;
	[SerializeField]
	Teleporter[] teleporters = null;
	[SerializeField]
	Transform turret = null;

	int currMoveZone = 0;

	[SerializeField]
	int maxTurrets = 10;
	[SerializeField]
	int turretSpawnChance = 30;

	Animator anim = null;
	Player player = null;

	bool onGround = true;
	bool movingToNextRoom = false;

	// Use this for initialization
	void Start () {
		UpdateStats ();

		anim = gameObject.GetComponentInChildren<Animator> ();
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (agent.remainingDistance <= 1.0f && Random.Range(1, 100) <= 50 && !movingToNextRoom) {
			NavMeshHit hit;
			float maxDistance = 0.0f;

			do {
				Vector3 newDestination = Random.insideUnitSphere;
				newDestination = new Vector3(movementZones[currMoveZone].bounds.size.x * newDestination.x + movementZones[currMoveZone].transform.position.x,
											 transform.position.y,
				                             movementZones[currMoveZone].bounds.size.z * newDestination.z + movementZones[currMoveZone].transform.position.z);

				NavMesh.SamplePosition(newDestination, out hit, maxDistance, 1);

				maxDistance += 1.0f;
			} while (hit.hit == false); 

			agent.SetDestination(hit.position);
		}
	}

	public override void Damage (int damageTaken, Transform bullet) {
		currHealth -= damageTaken;

		if (currHealth <= 0) {
			for (int index = 0; index < teleporters.Length; index++)
				teleporters[index].gameObject.SetActive (true);

			SoundManager.instance.BossSoundeffects[3].Play();
			
			DestroyObject();
		}
	}

	void OnTriggerEnter (Collider col) {
		if (col.tag == "Player" && onGround) {
			int damage = Mathf.CeilToInt((initialDamage + damagePerStrength * Strength) * 1.5f - player.Defense);

			if (damage < 5)
				damage = 5;

			player.DamagePlayer(damage);
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