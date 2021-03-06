﻿using UnityEngine;
using System.Collections;

public class FriendShotBullet : Weapon {

	void OnCollisionEnter(Collision col) {
			if(col.gameObject.tag != "Wall" && col.gameObject.tag != "WorldObject") {
				Statistics colStats = col.gameObject.GetComponent<Statistics> ();
				
				if(colStats != null) {
					float damage = ((initialDamage + ownersStrength * damagePerStrength) * ChargeScale) - colStats.Defense;
					
					if(damage <= 0.0f)
						damage = 1;
					
					if(WeaknessCheck(color, colStats.CurrColor) > 0) {
						damage *= 1.5f;
					} else if (WeaknessCheck(color, colStats.CurrColor) < 0) {
						damage *= 0.5f;
					}
					
					if (col.gameObject.tag != "Player" && col.gameObject.tag != "Dampener")
					{
						SoundManager.instance.EnemySoundeffects[4].Play();
						colStats.Damage(Mathf.CeilToInt(damage), transform);
					}
					else if (col.gameObject.tag == "Dampener" && ChargeScale > 1.0f)
					{
						SoundManager.instance.MiscSoundeffects[5].Play();
						colStats.Damage(Mathf.CeilToInt(damage), transform);
					}
					else if (col.gameObject.tag == "Dampener" && ChargeScale <= 1.0f)
					{
						SoundManager.instance.MiscSoundeffects[6].Play();
						colStats.Damage(0, transform);
					}

					EnemyHealthbar healthbar = col.transform.parent.GetComponentInChildren<EnemyHealthbar> ();
						
					if (healthbar != null)
						healthbar.UpdateFillAmount();
				}
			}
			
			Destroy(gameObject);
	}
}
