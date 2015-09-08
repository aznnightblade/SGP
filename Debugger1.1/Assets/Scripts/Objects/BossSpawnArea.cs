using UnityEngine;
using System.Collections;

public class BossSpawnArea : MonoBehaviour {

	bool containsPlayer = false;

	void OnTriggerEnter (Collider col) {
		if (col.tag == "Player Controller" || col.tag == "Player") {
			containsPlayer = true;
		}
	}

	void OnTiggerExit (Collider col) {
		if (col.tag == "Player Controller" || col.tag == "Player") {
			containsPlayer = false;
		}
	}

	public bool ContainsPlayer { get { return containsPlayer; } }
}
