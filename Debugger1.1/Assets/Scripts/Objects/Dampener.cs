using UnityEngine;
using System.Collections;

public class Dampener : Statistics {

	[SerializeField]
	Transform teleporter = null;



	void Update() {
		if (currHealth > 0) {
			teleporter.GetComponentInChildren<Teleporter>().IsActive = false;
		}
	}

	void OnCollisionEnter(Collision col) {
		if (col.gameObject.tag == "Player Bullet"
		    && (col.gameObject.transform.localScale.x > 2.1f
		    && col.gameObject.transform.localScale.y > 2.1f
		    && col.gameObject.transform.localScale.z > 2.1f)) {
			if(currHealth <= 0) {
				teleporter.GetComponentInChildren<Teleporter>().IsActive = true;
				DestroyObject();
			}
		}
	}
}
