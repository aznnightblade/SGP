using UnityEngine;
using System.Collections;

public class BossEnemySpawning : MonoBehaviour {

	[SerializeField]
	Wave[] Waves = null;
	int waveCounter = 0;

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
			LayerMask layer = (1 << LayerMask.NameToLayer("Enemy"));

			for (int index = 0; index < Waves[waveCounter].EnemyTypes.Length; index++) {
				SphereCollider[] enemyCollider = Waves[waveCounter].EnemyTypes[index].GetComponentsInChildren<SphereCollider> (true);
				float minDistance = gameObject.GetComponent<SphereCollider> ().radius + enemyCollider[0].radius + 1.0f; 
				
				for (int enemy = 0; enemy < Waves[waveCounter].numToSpawn[index]; enemy++) {
					Vector3 pos = Vector3.zero;

					do {
						pos = Random.insideUnitSphere;
						pos.y = 0;
						pos.Normalize();
						pos *= Random.Range(minDistance, minDistance + maxSpawnDistance);
						pos += transform.position;
						pos.y = GameObject.FindGameObjectWithTag("Player Controller").transform.position.y;
					} while (Physics.OverlapSphere(pos, enemyCollider[0].radius, layer).Length > 1);

					Instantiate(Waves[waveCounter].EnemyTypes[index], pos, transform.rotation);
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
}

[System.Serializable]
struct Wave {
	public GameObject[] EnemyTypes;
	public int[] numToSpawn;
}