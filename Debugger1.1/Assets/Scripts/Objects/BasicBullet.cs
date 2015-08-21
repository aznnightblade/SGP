using UnityEngine;
using System.Collections;

public class BasicBullet : Weapon {

	void OnCollisionEnter(Collision col) {
		if (col.gameObject.tag != owner.gameObject.tag && col.gameObject.tag != gameObject.tag) {
			if(col.gameObject.tag != "Wall" && col.gameObject.tag != "WorldObject") {
				Statistics colStats = null;

				if (col.gameObject.tag != "Player")
					colStats = col.gameObject.GetComponent<Statistics> ();
				else
					colStats = col.gameObject.GetComponentInChildren<Player> ();

				if(colStats != null) {
					float damage = (initialDamage + owner.Strength * damagePerStrength) - colStats.Defense;
						
					if(damage <= 0.0f)
						damage = 1;
	
					if(WeaknessCheck(color, colStats.Color) > 0) {
						damage *= 1.5f;
					} else if (WeaknessCheck(color, colStats.Color) < 0) {
						damage *= 0.5f;
					}

					colStats.CurrHealth -= Mathf.CeilToInt(damage);

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
}