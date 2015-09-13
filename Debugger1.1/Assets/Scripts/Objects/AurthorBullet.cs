using UnityEngine;
using System.Collections;

public class AurthorBullet : Weapon {

	void OnCollisionEnter(Collision col) {
			if(col.gameObject.tag != "Wall" && col.gameObject.tag != "WorldObject") {
				Statistics colStats = col.gameObject.GetComponentInChildren<Statistics> ();
				
				if(colStats != null) {
					float damage = ((initialDamage + ownersStrength * damagePerStrength) * ChargeScale) - colStats.Defense;
					
					if(damage <= 0.0f)
						damage = 5;

					SoundManager.instance.PlayerSoundeffects[4].Play();
					col.gameObject.GetComponentInChildren<Player>().DamagePlayer(Mathf.CeilToInt(damage));

					GameObject Aurthor = GameObject.FindGameObjectWithTag("Aurthor");

					if (Aurthor != null)
						Aurthor.GetComponent<Aurthor> ().DebuffPlayer ();
				}
			}
			
			Destroy(gameObject);
		}
}
