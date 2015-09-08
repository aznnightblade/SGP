using UnityEngine;
using System.Collections;

public class BossEnemySpawning : MonoBehaviour {

	enum SpawnType { Interval, Random, Player };
	enum SpawnLocation { OnTarget, OnSpawnPoint };

	[SerializeField]
	Wave[] Waves = null;
	int waveCounter = 0;
	[SerializeField]
	SpawnType Spawn = SpawnType.Interval;
	[SerializeField]
	SpawnLocation Type = SpawnLocation.OnTarget;

	[SerializeField]
	Transform[] spawnPoints = null;
	[SerializeField]
	BossSpawnArea[] spawnPointScripts = null;

	[SerializeField]
	float timeBetweenWaves = 30.0f;
	[SerializeField]
	float spawnTimer = 0.0f;

	[SerializeField]
	float maxSpawnDistance = 2.0f;

	// Use this for initialization
	void Start () {
		spawnTimer = timeBetweenWaves;
	}
	
	// Update is called once per frame
	void Update () {
		if (spawnTimer <= 0.0f) {
			int spawn = 0;

			if (Spawn == SpawnType.Interval) {
				spawn = waveCounter;
				waveCounter++;

				if (waveCounter == Waves.Length)
					waveCounter = 0;
			} else {
				spawn = Random.Range (0, Waves.Length - 1);
			}

			LayerMask layer = (1 << (LayerMask.NameToLayer("Enemy") | LayerMask.NameToLayer("Player")));
            SoundManager.instance.BossSoundeffects[2].Play();
			if(Type == SpawnLocation.OnTarget) {
				for (int index = 0; index < Waves[spawn].EnemyTypes.Length; index++) {
					SphereCollider[] enemyCollider = Waves[spawn].EnemyTypes[index].GetComponentsInChildren<SphereCollider> (true);
					float minDistance = gameObject.GetComponent<SphereCollider> ().radius + enemyCollider[0].radius + 1.0f; 
				
					for (int enemy = 0; enemy < Waves[spawn].numToSpawn[index]; enemy++) {
						Vector3 pos = Vector3.zero;

						do {
							pos = Random.insideUnitSphere;
							pos.y = 0;
							pos.Normalize();
							pos *= Random.Range(minDistance, minDistance + maxSpawnDistance);
							pos += transform.position;
							pos.y = GameObject.FindGameObjectWithTag("Player Controller").transform.position.y;
						} while (Physics.OverlapSphere(pos, enemyCollider[0].radius, layer).Length > 1);

						Instantiate(Waves[spawn].EnemyTypes[index], pos, Quaternion.identity);
					}
				}
			} else {
				for (int index = 0; index < Waves[spawn].EnemyTypes.Length; index++) {
					SphereCollider[] enemyCollider = Waves[spawn].EnemyTypes[index].GetComponentsInChildren<SphereCollider> (true);
					
					for (int enemy = 0; enemy < Waves[spawn].numToSpawn[index]; enemy++) {
						Vector3 pos = Vector3.zero;
						int spawnerToUse = -1;

						if (Spawn == SpawnType.Player) {
							for (int spawnpoint = 0; spawnpoint < spawnPointScripts.Length - 1; index ++) {
								if (spawnPointScripts[spawnpoint].ContainsPlayer) {
									spawnerToUse = spawnpoint;
									break;
								}
							}
						}
						
						do {
							Vector3 collider = Vector3.zero;

							if (Spawn != SpawnType.Player || spawnerToUse == -1) {
								collider = spawnPoints[Random.Range(0, spawnPoints.Length - 1)].GetComponent<BoxCollider> ().size;
							} else {
								collider = spawnPoints[spawnerToUse].GetComponent<BoxCollider> ().size;
							}

							pos = Random.insideUnitSphere;
							pos = new Vector3 (pos.x * collider.x, 0, pos.z * collider.z);
							pos.y = GameObject.FindGameObjectWithTag("Player Controller").transform.position.y;
						} while (Physics.OverlapSphere(pos, enemyCollider[0].radius, layer).Length > 1);
						
						Instantiate(Waves[spawn].EnemyTypes[index], pos, Quaternion.identity);
					}
				}
			}

			spawnTimer = timeBetweenWaves;
		}

		if (spawnTimer > 0.0f) {
			spawnTimer -= Time.deltaTime * GameManager.CTimeScale;

			if (spawnTimer < 0.0f)
				spawnTimer = 0.0f;
		}
	}

	public Transform[] SpawnPoints { get { return spawnPoints; } }
}

[System.Serializable]
public struct Wave {
	public GameObject[] EnemyTypes;
	public int[] numToSpawn;
}