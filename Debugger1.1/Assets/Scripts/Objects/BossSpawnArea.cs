using UnityEngine;
using System.Collections;

public class BossSpawnArea : MonoBehaviour {

	[SerializeField]
	bool containsPlayer = false;

	void Update () {

	}

	void OnTriggerEnter (Collider col) {
		if (col.tag == "Player Controller" || col.tag == "Player") {
			containsPlayer = true;
		}
	}

	void OnTriggerExit (Collider col) {
		if (col.tag == "Player Controller" || col.tag == "Player") {
			containsPlayer = false;
		}
	}

	public void OnTiggerExit (Collider col) {
		if (col.tag == "Player Controller" || col.tag == "Player") {
			containsPlayer = false;
		}
	}

	public bool ContainsPlayer { get { return containsPlayer; } }
}
