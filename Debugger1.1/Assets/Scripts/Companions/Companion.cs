using UnityEngine;
using System.Collections;

public class Companion : Statistics {

	enum StatToBuff {NONE, STRENGTH, ENDURANCE, AGILITY, DEXTERITY, INTELLIGENCE, LUCK};
	bool buffApplied = false;
	Player player = null;
	Transform target = null;
	float findTargetDelay = 0.25f;
	float findTargetTimer = 0.0f;
	NavMeshAgent agent = null;

	[SerializeField]
	StatToBuff statToBuff = StatToBuff.NONE; // which stat should be buffed
	[SerializeField]
	int buffAmount = 0; // amount to buff that stat by
	[SerializeField]
	int initShieldBuff = 0; // amount to increase player shield level by
	[SerializeField]
	int shieldIncreasePerLevel = 50;
	[SerializeField]
	Transform bullet = null;
	Weapon weapon = null;
	float shotDistance = 0.0f;
	[SerializeField]
	int currLevel = 1;

	// Use this for initialization
	void Start () {
		agent = gameObject.GetComponent<NavMeshAgent> ();
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ();
		agent.destination = player.transform.position;

		currHealth = maxHealth = initialHealth + healthPerEndurance * endurance;
		critChance = initialCrit + critPerLuck * luck;
		player.Shield = player.MaxShield = initShieldBuff + shieldIncreasePerLevel * currLevel;

		shotDistance = initialShotDistance + ShotDistancePerDexerity * dexterity;
		weapon = bullet.GetComponent<Weapon> ();

		AddBuffs();
	}
	
	// Update is called once per frame
	void Update () {
		agent.destination = player.transform.position;

		// apply stat buff if companion is alive, remove it when companion dies
		if (currHealth <= 0) {
			RemoveBuffs ();
            SoundManager.instance.CompanionSFX[3].Play();
			Destroy (gameObject);
		}

		if (findTargetTimer <= 0.0f && target == null) {
			LayerMask layer = (1 << LayerMask.NameToLayer ("Enemy"));
			Collider[] enemies = Physics.OverlapSphere (transform.position, shotDistance, layer);

			if (enemies.Length > 0) {
				for (int index = 0; index < enemies.Length; index++) {
					if (target == null) {
						target = enemies [index].transform;
					} else if (Vector3.Distance (transform.position, target.position) > Vector3.Distance (transform.position, enemies [index].transform.position)) {
						target = enemies [index].transform;
					}
				}
			}

			findTargetTimer = findTargetDelay;
		} else if (target != null && shotDelay <= 0.0f) {
			FireBullet ();

			shotDelay = weapon.InitialShotDelay + weapon.ShotDelayReductionPerAgility * agility;
		}

		if (findTargetTimer > 0.0f) {
			findTargetTimer -= Time.deltaTime * GameManager.CTimeScale2;

			if (findTargetTimer < 0.0f)
				findTargetTimer = 0.0f;
		}

		if (shotDelay > 0.0f) {
			shotDelay -= Time.deltaTime * GameManager.CTimeScale2;
			
			if (shotDelay < 0.0f)
				shotDelay = 0.0f;
		}
	}

	void AddBuffs() {
		// Only apply stat buff once
		if(!buffApplied){
			switch (statToBuff) {
			case StatToBuff.STRENGTH:
				player.Strength += buffAmount * currLevel;
				break;
			case StatToBuff.ENDURANCE:
				player.Endurance += buffAmount * currLevel;
				break;
			case StatToBuff.AGILITY:
				player.Agility += buffAmount * currLevel;
				break;
			case StatToBuff.DEXTERITY:
				player.Dexterity += buffAmount * currLevel;
				break;
			case StatToBuff.INTELLIGENCE:
				player.Intelligence += buffAmount * currLevel;
				break;
			case StatToBuff.LUCK:
				player.Luck += buffAmount * currLevel;
				break;
			case StatToBuff.NONE:
				break;
			}
		}

		buffApplied = true;
	}

	void RemoveBuffs() {
		// Only do this if player has a stat buff
		if(buffApplied) {
			switch (statToBuff) {
			case StatToBuff.STRENGTH:
				player.Strength -= buffAmount * currLevel;
				break;
			case StatToBuff.ENDURANCE:
				player.Endurance -= buffAmount * currLevel;
				break;
			case StatToBuff.AGILITY:
				player.Agility -= buffAmount * currLevel;
				break;
			case StatToBuff.DEXTERITY:
				player.Dexterity -= buffAmount * currLevel;
				break;
			case StatToBuff.INTELLIGENCE:
				player.Intelligence -= buffAmount * currLevel;
				break;
			case StatToBuff.LUCK:
				player.Luck -= buffAmount * currLevel;
				break;
			case StatToBuff.NONE:
				break;
			}

			player.Shield = player.MaxShield = 0;
		}

		buffApplied = false;
	}

	public void RemoveAllie () {
		RemoveBuffs ();
		Destroy (gameObject);
	}

	void FireBullet () {
        SoundManager.instance.CompanionSFX[2].Play();
		Vector3 pos = transform.position;
		Vector3 direction = pos - target.position;
		float rot = -((Mathf.Atan2(direction.z, direction.x) * 180 / Mathf.PI) + 90.0f);
		
		GameObject newBullet = (Instantiate (bullet, pos, Quaternion.Euler(0, rot, 0)) as Transform).gameObject;
		//GameObject newBullet = Bullet.gameObject;
		newBullet.tag = ("Player Bullet");
		newBullet.GetComponent<Weapon> ().Owner = this;
		newBullet.GetComponent<Weapon> ().OwnerMoveDirection = agent.velocity;
	}
}
