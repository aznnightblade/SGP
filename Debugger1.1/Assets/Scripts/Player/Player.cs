using UnityEngine;
using System.Collections;

public class Player : Statistics {

	[SerializeField]
	Transform shotLocation = null;
	[SerializeField]
	Weapon currWeapon = null;
	[SerializeField]
	Weapon[] weapons = null;
	[SerializeField]
	Breakpoint breakpoint = null;
	[SerializeField]
	int multithreadLevel = 1;
	[SerializeField]
	float invulTimePerDamage = 0.1f;
	float invulTimer = 0.0f;

	// Use this for initialization
	void Start () {
		currHealth = maxHealth = initialHealth + healthPerEndurance * endurance;
		critChance = initialCrit + critPerLuck * luck;
		currWeapon = weapons [0];
	}
	
	// Update is called once per frame
	void Update () {
		if (invulTimer >= 0.0f) {
			invulTimer -= Time.deltaTime;

			if (invulTimer < 0.0f)
				invulTimer = 0.0f;
		}
	}

	public void DamagePlayer (int damageTaken) {
		// If the player is not in invulnerability state, deal damage to the player.
		if (invulTimer <= 0.0f) {
			currHealth -= damageTaken;

			if (currHealth <= 0) {

			}

			invulTimer = invulTimePerDamage * damageTaken;
		}
	}

	public Weapon CurrWeapon { get { return currWeapon; } }
	public Weapon[] Weapons { get { return weapons; } }
	public Breakpoint Breakpoint { get { return breakpoint; } }
	public int MultithreadLevel {
		get { return multithreadLevel; }
		set { multithreadLevel = value; }
	}
	public Transform ShotLocation { get { return shotLocation; } }
}
