using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnTrigger : MonoBehaviour {

	Transform trigger; // grabs position of spawn trigger
	public List<int> numOfSpawns; // list of how many of each enemy to spawn
	public List<Transform> enemySpawns; // list of enemy types to spawn
	enum ColorSpawns {RANDOM, MANUAL}; // allow for enemies with DLL colors
	
	[SerializeField]
	bool spawnsCanBeColored = false;
	[SerializeField]
	ColorSpawns colorSpawnMethod = ColorSpawns.RANDOM;
	[SerializeField]
	DLLColor.Color manualColor = DLLColor.Color.NEUTRAL;

	void Start() {
		trigger = gameObject.GetComponent<Transform> ();
	}

	void OnTriggerEnter(Collider col) {
		if (col.gameObject.tag == "Player") {
			for(int i = 0; i < numOfSpawns.Count; i++) {
				for(int j = 0; j < numOfSpawns[i]; j++) {
					Vector3 spawnPoint = trigger.position + new Vector3(Random.Range(-3.0f, 3.0f), 0.0f, Random.Range(-3.0f, 3.0f));
					Transform enemy = Instantiate(enemySpawns[i], spawnPoint, Quaternion.Euler(Vector3.zero)) as Transform;

					// Can we spawn enemies with DLL colors?
					if(spawnsCanBeColored) {
						// Spawn enemies with randomly assigned DLL colors?
						if(colorSpawnMethod == ColorSpawns.RANDOM) {
							int randColor = Random.Range(0, 4);
							switch(randColor) {
							case 0:
								enemy.GetComponent<Statistics>().Color = DLLColor.Color.NEUTRAL;
								enemy.GetComponentInChildren<SpriteRenderer>().color = Color.black;
								break;
							case 1:
								enemy.GetComponent<Statistics>().Color = DLLColor.Color.RED;
								enemy.GetComponentInChildren<SpriteRenderer>().color = Color.red;
								break;
							case 2:
								enemy.GetComponent<Statistics>().Color = DLLColor.Color.GREEN;
								enemy.GetComponentInChildren<SpriteRenderer>().color = Color.green;
								break;
							case 3:
								enemy.GetComponent<Statistics>().Color = DLLColor.Color.BLUE;
								enemy.GetComponentInChildren<SpriteRenderer>().color = Color.blue;
								break;
							}
						}
						// Spawn enemies with a preselected DLL color?
						else if(colorSpawnMethod == ColorSpawns.MANUAL) {
							enemy.GetComponent<Statistics>().Color = manualColor;
							switch (manualColor) {
							case DLLColor.Color.NEUTRAL:
								enemy.GetComponentInChildren<SpriteRenderer>().color = Color.black;
								break;
							case DLLColor.Color.RED:
								enemy.GetComponentInChildren<SpriteRenderer>().color = Color.red;
								break;
							case DLLColor.Color.GREEN:
								enemy.GetComponentInChildren<SpriteRenderer>().color = Color.green;
								break;
							case DLLColor.Color.BLUE:
								enemy.GetComponentInChildren<SpriteRenderer>().color = Color.blue;
								break;
							}
						}
					}
				}
			}
		}

		Destroy (gameObject);
	}
}
