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

	public override void OnCollisionEnter(Collision col) {

		if (col.gameObject.tag == "Player Bullet") {

			if (currHealth <= 0) {
				teleporter.GetComponentInChildren<Teleporter> ().IsActive = true;
                SoundManager.instance.MiscSoundeffects[7].Play();
				DestroyObject ();
			}
		}
	}

}