using UnityEngine;
using System.Collections;

public class SpawnLocationMaster : MonoBehaviour {

	[SerializeField]
	BossSpawnArea[] spawnPoints = null;
	int lastPoint = 0;
	float checkDelay = 0.25f;
	float checkTimer = 0.0f;

	// Use this for initialization
	void Start () {
		for (int index = 0; index < spawnPoints.Length; index++) {
			if (spawnPoints[index].ContainsPlayer) {
				lastPoint = index;
				break;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (checkTimer <= 0.0f) {
			for (int index = 0; index < spawnPoints.Length; index++) {
				if (index == lastPoint)
					continue;

				if (spawnPoints[index].ContainsPlayer) {
					spawnPoints[lastPoint].OnTiggerExit (GameObject.FindGameObjectWithTag("Player").GetComponent<Collider> ());
					lastPoint = index;
					break;
				}
			}

			checkTimer = checkDelay;
		} else {
			checkTimer -= Time.deltaTime;
		}
	}
}
