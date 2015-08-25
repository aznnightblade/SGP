using UnityEngine;
using System.Collections;

public class DeletedMemory : MonoBehaviour {

	void OnTriggerEnter(Collider col) {
		if (col.tag == "Player" && col.GetComponentInChildren<Player>().IsHovering == false) {
			col.GetComponentInChildren<Player>().CurrHealth = 0;
		}
	}
}
