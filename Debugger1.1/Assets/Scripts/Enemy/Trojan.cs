using UnityEngine;
using System.Collections;

public class Trojan : Statistics {

	NavMeshAgent agent = null;
	Transform target = null;

	[SerializeField]
	Transform[] spawns = null;
	[SerializeField]
	int[] spawnChances = null;	// Must equal 100% if you want 100% chance to spawn something.
	[SerializeField]
	float spawnDelay = 1.5f;
	float spawnTimer = 1.5f;
	[SerializeField]
	float maxSpawnDistance = 1.5f;

	[SerializeField]
	int minSpawnOnDeath = 1;
	[SerializeField]
	int maxSpawnOnDeath = 4;

	// Use this for initialization
	void Start () {
		UpdateStats ();

		spawnTimer = spawnDelay;
		agent = GetComponent<NavMeshAgent> ();
		target = GameObject.FindGameObjectWithTag ("Player").transform;

		agent.destination = target.position;

	}
	
	// Update is called once per frame
	void Update () {
		agent.destination = target.position;

		if (spawnTimer <= 0.0f) {
			int random = Random.Range(0, 100);
			int rangeMin = 0, rangeMax = 0;

			for (int index = 0; index < spawns.Length; index++) {
				rangeMax += spawnChances[index];
				if(random >= rangeMin && random <= rangeMax) {
					SpawnEnemy(index);
					break;
				}

				rangeMin += spawnChances[index];
			}

			spawnTimer = spawnDelay;
		} else if (spawnTimer > 0.0f) {
			spawnTimer -= Time.deltaTime * GameManager.CTimeScale;

			if(spawnTimer < 0.0f)
				spawnTimer = 0.0f;
		}
	}

	void SpawnEnemy(int spawn) {
		if (spawn < spawns.Length && spawn >= 0) {
			LayerMask layer = (1 << (LayerMask.NameToLayer ("Enemy") | LayerMask.NameToLayer ("Player")));

			Vector3 pos = Vector3.zero;
			SphereCollider[] enemyCollider = spawns [spawn].GetComponentsInChildren<SphereCollider> (true);
			float minDistance = gameObject.GetComponent<SphereCollider> ().radius + enemyCollider [0].radius + 1.0f; 
		
			do {
				pos = Random.insideUnitSphere;
				pos.y = 0;
				pos.Normalize ();
				pos *= Random.Range (minDistance, minDistance + maxSpawnDistance);
				pos += transform.position;
				pos.y = GameObject.FindGameObjectWithTag ("Player Controller").transform.position.y;
			} while (Physics.OverlapSphere(pos, enemyCollider[0].radius, layer).Length > 1);
		
			Instantiate (spawns [spawn], pos, Quaternion.identity);
		}
	}

	public void OnDeath () {
		int numToSpawn = Random.Range(minSpawnOnDeath, maxSpawnOnDeath);
			
		for (int enemy = 0; enemy < numToSpawn; enemy++) {
			int random = Random.Range(0, 100);
			int rangeMin = 0, rangeMax = 0;
				
			for (int index = 0; index < spawns.Length; index++) {
				rangeMax += spawnChances[index];
		
				if(random >= rangeMin && random <= rangeMax) {
					SpawnEnemy(index);
					break;
				}
					
				rangeMin += spawnChances[index];
			}
		}
	}
}
