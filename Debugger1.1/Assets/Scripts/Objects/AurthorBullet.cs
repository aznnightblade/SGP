using UnityEngine;
using System.Collections;

public class AurthorBullet : Weapon {

	void OnCollisionEnter(Collision col) {
		if (col.gameObject.tag != owner.gameObject.tag && col.gameObject.tag != gameObject.tag) {
			if(col.gameObject.tag != "Wall" && col.gameObject.tag != "WorldObject") {
				Statistics colStats = col.gameObject.GetComponent<Statistics> ();
				
				if(colStats != null) {
					float damage = ((initialDamage + owner.Strength * damagePerStrength) * ChargeScale) - colStats.Defense;
					
					if(damage <= 0.0f)
						damage = 5;

					SoundManager.instance.PlayerSoundeffects[4].Play();
					col.gameObject.GetComponentInChildren<Player>().DamagePlayer(Mathf.CeilToInt(damage));

					owner.GetComponent<Aurthor> ().DebuffPlayer ();
				}
			}
			
			Destroy(gameObject);
		}
	}
}
