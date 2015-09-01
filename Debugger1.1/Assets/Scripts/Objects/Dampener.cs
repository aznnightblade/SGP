using UnityEngine;
using System.Collections;

public class Dampener : Statistics {
	public bool Toggle = false;
	[SerializeField]
	void Update() {
		if (currHealth > 0) {
			Toggle = false;
		}
	}

	void OnCollisionEnter(Collision col) {

		if (col.gameObject.tag == "Player Bullet") {

			if (currHealth <= 0) {
                sounds.MiscSoundeffects[7].Play();
				Toggle = true;
			}

		}
	}

}