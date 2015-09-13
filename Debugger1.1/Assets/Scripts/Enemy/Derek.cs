using UnityEngine;
using System.Collections;

public class Derek : Enemy {

	[SerializeField]
	Frosty frosty = null;
	Player player = null;

	[SerializeField]
	float timeDead = 10.0f;
	float deadTimer = 0.0f;
	[SerializeField]
	float reviveDelay = 1.5f;
	float reviveTimer = 0.0f;

	[SerializeField]
	float attackDelay = 0.25f;
	[SerializeField]
	float attackTimer = 0.0f;

	[SerializeField]
	bool isDead = false;
	[SerializeField]
	bool isReviving = false;
	[SerializeField]
	bool attacking = false;

	// Use this for initialization
	void Start () {
		UpdateStats ();

		sprite = gameObject.GetComponentInChildren<SpriteRenderer> ();
		target = GameObject.FindGameObjectWithTag ("Player").transform;
		player = target.GetComponent<Player> ();
	}
	
	// Update is called once per frame
	void Update () {
		UpdateTimers ();

		if (!isDead && !isReviving) {
			agent.destination = target.position;

			if (attacking && attackTimer <= 0.0f) {
				int damage = Mathf.CeilToInt((initialDamage + damagePerStrength * strength) - player.Defense);

				if (damage < 2) {
					damage = 2;
				}

				player.DamagePlayer(damage);
				attackTimer = attackDelay;
			}
		}

		if (isDead && deadTimer <= 0.0f) {
			isDead = false;
			reviveTimer = reviveDelay;
			isReviving = true;
		}

		if (isReviving && reviveTimer > 0.0f) {
			Color color = sprite.color;
			color.a = 0.2f + 0.8f * (1 - reviveTimer / reviveDelay);
			sprite.color = color;
		} else if (isReviving && reviveTimer <= 0.0f) {
			Color color = sprite.color;
			color.a = 1.0f;
			sprite.color = color;
			isReviving = false;

			agent.updatePosition = true;
			agent.updateRotation = true;

			currHealth = maxHealth;
		}
	}

	public void DestroyDerek () {
		DestroyObject ();
	}

	void UpdateTimers () {
		if (deadTimer > 0.0f) {
			deadTimer -= Time.deltaTime;

			if (deadTimer < 0.0f)
				deadTimer = 0.0f;
		}

		if (reviveTimer > 0.0f) {
			reviveTimer -= Time.deltaTime;
			
			if (reviveTimer < 0.0f)
				reviveTimer = 0.0f;
		}

		if (attackTimer > 0.0f) {
			attackTimer -= Time.deltaTime;
			
			if (attackTimer < 0.0f)
				attackTimer = 0.0f;
		}
	}

	public override void Damage (int damageTaken, Transform bullet) {
		currHealth -= damageTaken;

		if (!isDead) {
			SoundManager.instance.BossSoundeffects [3].Play ();
		} else {
			SoundManager.instance.MiscSoundeffects [6].Play ();
		}

		if (currHealth <= 0 && !isDead) {
			agent.updatePosition = false;
			agent.updateRotation = false;

			isDead = true;
			frosty.BulletHellDelayTimer = frosty.DelayTillBulletHell;
			deadTimer = timeDead;

			Color color = sprite.color;
			color.a = 0.2f;
			sprite.color = color;
		}
	}

	void OnCollisionEnter(Collision col) {
		if (col.gameObject.tag == "Player" || col.gameObject.tag == "Player Controller") {
			attacking = true;
		}
	}

	void OnCollisionExit(Collision col) {
		if (col.gameObject.tag == "Player" || col.gameObject.tag == "Player Controller") {
			attacking = false;
		}
	}

	public bool IsDead { get { return isDead; } }
	public bool IsReviving { get { return isReviving; } }
}
