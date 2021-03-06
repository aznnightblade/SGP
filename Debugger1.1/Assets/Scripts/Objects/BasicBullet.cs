﻿using UnityEngine;
using System.Collections;

public class BasicBullet : Weapon {

	void OnCollisionEnter(Collision col) {
		if(col.gameObject.tag != "Wall" && col.gameObject.tag != "WorldObject") {
			Statistics colStats = null;
			
			if (col.gameObject.tag != "Player Controller")
				colStats = col.gameObject.GetComponent<Statistics> ();
			else
				colStats = col.gameObject.GetComponentInChildren<Player> ();
			
			if(colStats != null) {
				float damage = ((initialDamage + ownersStrength * damagePerStrength) * ChargeScale) - colStats.Defense;
				
				if(damage <= 0.0f)
					damage = 1;
				
				if(WeaknessCheck(color, colStats.CurrColor) > 0) {
					damage *= 1.5f;
				} else if (WeaknessCheck(color, colStats.CurrColor) < 0) {
					damage *= 0.5f;
				}
				
				if (col.gameObject.tag != "Player Controller")
				{
					SoundManager.instance.EnemySoundeffects[4].Play();
					colStats.Damage(Mathf.CeilToInt(damage), transform);
				}
				else
				{
					SoundManager.instance.PlayerSoundeffects[4].Play();
					col.gameObject.GetComponentInChildren<Player>().DamagePlayer(Mathf.CeilToInt(damage));
				}
				if(col.gameObject.tag != "Player") {
					EnemyHealthbar healthbar = col.transform.parent.GetComponentInChildren<EnemyHealthbar> ();
					
					if (healthbar != null)
						healthbar.UpdateFillAmount();
				}
			}
		}
		
		Destroy(gameObject);
	}
}