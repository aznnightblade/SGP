﻿using UnityEngine;
using System.Collections;

public class HealthPickup : MonoBehaviour {

	public SoundManager sound;

	void Start() {
		sound = GameObject.FindGameObjectWithTag ("SoundManager").GetComponent<SoundManager> ();
	}

	void OnTriggerEnter(Collider col) {
		if (col.gameObject.tag == "Player") {
			sound.MiscSoundeffects[0].Play();
			// Restores 15% of player's max health
			int healthRestored = (int)(col.gameObject.GetComponent<Statistics>().MaxHealth * 0.15f);
			col.gameObject.GetComponent<Statistics>().CurrHealth += healthRestored;
			// Avoid overhealing the player
			if(col.gameObject.GetComponent<Statistics>().CurrHealth >= col.gameObject.GetComponent<Statistics>().MaxHealth)
				col.gameObject.GetComponent<Statistics>().CurrHealth = col.gameObject.GetComponent<Statistics>().MaxHealth;
			
			Destroy (gameObject);
		}
	}
}
