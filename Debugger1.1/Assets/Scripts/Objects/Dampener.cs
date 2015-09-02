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

	public override void OnCollisionEnter(Collision col) {

		if (col.gameObject.tag == "Player Bullet") {

			if (currHealth <= 0) {
                SoundManager.instance.MiscSoundeffects[7].Play();
				Toggle = true;
			}

		}
	}

}