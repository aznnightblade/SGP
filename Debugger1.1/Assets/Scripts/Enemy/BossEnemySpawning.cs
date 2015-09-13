using UnityEngine;
using System.Collections;

public class BossEnemySpawning : MonoBehaviour {

	enum SpawnType { Interval, Random, OnCallInterval, OnCallRandom };
	enum SpawnLocation { OnTarget, OnSpawnPoint, Player };

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
		if (spawnTimer <= 0.0f && !(Spawn == SpawnType.OnCallInterval || Spawn == SpawnType.OnCallRandom)) {
			SpawnWave ();

			spawnTimer = timeBetweenWaves;
		}

		if (spawnTimer > 0.0f) {
			spawnTimer -= Time.deltaTime * GameManager.CTimeScale;

			if (spawnTimer < 0.0f)
				spawnTimer = 0.0f;
		}
	}

	public void SpawnWave (int waveToSpawn = -1) {
		int spawn = 0;

		if (waveToSpawn < 0 || waveToSpawn >= Waves.Length) {
			if (Spawn == SpawnType.Interval || Spawn == SpawnType.OnCallInterval) {
				if (waveCounter == Waves.Length)
					waveCounter = 0;

				spawn = waveCounter;
				waveCounter++;
			} else {
				spawn = Random.Range (0, Waves.Length - 1);
			}
		} else {
			spawn = waveCounter = waveToSpawn;
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
					
					GameObject newEnemy = (GameObject)Instantiate(Waves[spawn].EnemyTypes[index], pos, Quaternion.identity);
					newEnemy.GetComponentInChildren<Enemy> ().CurrColor = (DLLColor.Color)Random.Range(0, (int)DLLColor.Color.BLUE);
					newEnemy.GetComponentInChildren<Enemy> ().CurrMode = Enemy.Mode.BossRoom;
				}
			}
		} else {
			int spawnerToUse = -1;
			
			if (Type == SpawnLocation.Player) {
				for (int spawnpoint = 0; spawnpoint < spawnPointScripts.Length - 1; spawnpoint++) {
					if (spawnPointScripts[spawnpoint].ContainsPlayer) {
						spawnerToUse = spawnpoint;
						break;
					}
				}
			}
			
			for (int index = 0; index < Waves[spawn].EnemyTypes.Length; index++) {
				SphereCollider[] enemyCollider = Waves[spawn].EnemyTypes[index].GetComponentsInChildren<SphereCollider> (true);
				bool random = false;
				
				for (int enemy = 0; enemy < Waves[spawn].numToSpawn[index]; enemy++) {
					Vector3 pos = Vector3.zero;
					if (spawnerToUse == -1)
						random = true;

					if (random)
						spawnerToUse = Random.Range(0, spawnPoints.Length - 1);
					
					Vector3 collider = spawnPoints[spawnerToUse].GetComponent<BoxCollider> ().bounds.size * 0.5f;
					
					do {
						pos = Random.insideUnitSphere;
						pos = new Vector3 (pos.x * collider.x + spawnPoints[spawnerToUse].position.x, GameObject.FindGameObjectWithTag("Player Controller").transform.position.y,
						                   pos.z * collider.z + spawnPoints[spawnerToUse].position.z);
					} while (Physics.OverlapSphere(pos, enemyCollider[0].radius, layer).Length > 1);
					
					GameObject newEnemy = (GameObject)Instantiate(Waves[spawn].EnemyTypes[index], pos, Quaternion.identity);
					newEnemy.GetComponentInChildren<Enemy> ().CurrColor = (DLLColor.Color)Random.Range(0, (int)DLLColor.Color.BLUE);
					newEnemy.GetComponentInChildren<Enemy> ().CurrMode = Enemy.Mode.BossRoom;
				}
			}
		}
	}

	public Transform[] SpawnPoints { get { return spawnPoints; } }
	public BossSpawnArea[] SpawnPointScripts { get { return spawnPointScripts; } }
	public int NumberOfWaves { get { return Waves.Length; } }
	public int WaveCounter { get { return waveCounter; } }
}

[System.Serializable]
public struct Wave {
	public GameObject[] EnemyTypes;
	public int[] numToSpawn;
}