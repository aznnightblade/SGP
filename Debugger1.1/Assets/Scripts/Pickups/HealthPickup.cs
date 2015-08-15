using UnityEngine;
using System.Collections;

public class HealthPickup : MonoBehaviour {

	void OnTriggerEnter(Collider col) {
		if (col.gameObject.tag == "Player") {
			// Restores 15% of player's max health
			int healthRestored = (int)(col.gameObject.GetComponent<Statistics>().MaxHealth * 0.15f);
			col.gameObject.GetComponent<Statistics>().CurrHealth += healthRestored;
			// Avoid overhealing the player
			if(col.gameObject.GetComponent<Statistics>().CurrHealth >= col.gameObject.GetComponent<Statistics>().MaxHealth)
				col.gameObject.GetComponent<Statistics>().CurrHealth = col.gameObject.GetComponent<Statistics>().MaxHealth;
		}

		Destroy (gameObject);
	}
}
