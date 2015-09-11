using UnityEngine;
using System.Collections;

public class TeleportZoneScript : MonoBehaviour {

	bool containsPlayer = false;

	void OnTriggerEnter (Collider col) {
		if (col.gameObject.tag == "Player") {
			containsPlayer = true;
		}
	}

	void OnTriggerExit (Collider col) {
		if (col.gameObject.tag == "Player") {
			containsPlayer = false;
		}
	}

	public bool ContainsPlayer { get { return containsPlayer; } }
}
