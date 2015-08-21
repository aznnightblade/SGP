﻿using UnityEngine;
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
			if(currHealth <= 0) {
				teleporter.GetComponentInChildren<Teleporter>().IsActive = true;
				DestroyObject();
			}
		}
	}
}
