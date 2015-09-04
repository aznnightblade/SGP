using UnityEngine;
using System.Collections;

public class DeletedMemory : MonoBehaviour {

	Player player = null;
	bool playerContacting = false;

	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ();

	}

	void Update () {
		if (player.IsHovering == false && playerContacting) {
			player.CurrHealth = 0;
		}
	}

	void OnTriggerEnter(Collider col) {
		if (col.tag == "Player" ) {
			if (col.name == "Player")
				playerContacting = true;
			else
				col.gameObject.GetComponent<Enemy> ().Damage(9999, transform);
		}
	}

	void OnTriggerExit(Collider col) {
		if (col.tag == "Player" ) {
			playerContacting = false;
		}
	}
}
