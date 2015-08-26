﻿using UnityEngine;
using System.Collections;

public class BasicMelee : Statistics {

	NavMeshAgent agent = null;
	Transform target = null;

	[SerializeField]
	float damageDelay = 0.5f;
	float delayTimer = 0.0f;
	bool attackingPlayer = false;

	// Use this for initialization
	void Start () {
		currHealth = maxHealth = initialHealth + healthPerEndurance * endurance;
		critChance = initialCrit + critPerLuck * luck;

		agent = gameObject.GetComponent<NavMeshAgent> ();
		target = GameObject.FindGameObjectWithTag ("Player").transform;
		agent.destination = target.position;
	}
	
	// Update is called once per frame
	void Update () {
		agent.destination = target.position;

		RechargeShields ();

		if (GameManager.CTimeScale == 0.0f) {
			agent.velocity = Vector3.zero;
			agent.updateRotation = false;
		}
		
		if (GameManager.CTimeScale > 0.0f && !agent.updateRotation) {
			agent.updateRotation = true;
		}

        if (currHealth <= 0)
        {
            DestroyObject();
        }

		if (attackingPlayer && delayTimer <= 0.0f) {
			Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            sounds.EnemySoundeffects[0].Play();
			float damage = (initialDamage + damagePerStrength * strength) - player.Defense;

			if(damage < 0.0f)
				damage = 0;
            sounds.EnemySoundeffects[0].Play();
			player.DamagePlayer(Mathf.CeilToInt(damage));

			delayTimer = damageDelay;
		}

		if (delayTimer > 0.0f) {
			delayTimer -= Time.deltaTime * GameManager.CTimeScale;

			if (delayTimer <= 0.0f)
				delayTimer = 0.0f;
		}
	}

	public override void OnCollisionEnter(Collision col) {
		gameObject.GetComponent<Rigidbody> ().velocity = Vector3.zero;

		if (col.gameObject.tag == "Player Controller") {
			attackingPlayer = true;
		}
	}

	void OnCollisionExit(Collision col) {
		if (col.gameObject.tag == "Player Controller") {
			attackingPlayer = false;
		}
	}
}
