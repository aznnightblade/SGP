using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FireWaller : Statistics {

	NavMeshAgent agent = null;
	Transform target = null;
	List<GameObject> allies = new List<GameObject>();

	[SerializeField]
	float initialShieldAmount = 20.0f;
	[SerializeField]
	float increasePerIntelligence = 2.0f;

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
			DestroyObject ();
		
		if (attackingPlayer && delayTimer <= 0.0f) {
			Player player = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Player>();
			
			float damage = (initialDamage + damagePerStrength * strength) - player.Defense;
			
			if(damage < 0.0f)
				damage = 0;
			
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
		
		if (col.gameObject.tag == "Player") {
			attackingPlayer = true;
		}
	}
	
	void OnCollisionExit(Collision col) {
		if (col.gameObject.tag == "Player") {
			attackingPlayer = false;
		}
	}

	void OnTriggerEnter (Collider col) {
		if (col.gameObject.tag == "Enemy") {
			col.GetComponent<Statistics> ().MaxShield = Mathf.CeilToInt(initialShieldAmount + increasePerIntelligence * gameObject.GetComponent<Statistics> ().Intelligence);
			allies.Add(col.gameObject);
		}
	}

	void OnTriggerExit (Collider col) {
		if (col.gameObject.tag == "Enemy") {
			if (col.gameObject.name != "FireWaller")
				col.GetComponent<Statistics> ().MaxShield = col.GetComponent<Statistics> ().Shield = 0;
			else {
				col.GetComponent<Statistics> ().MaxShield -= Mathf.CeilToInt(initialShieldAmount + increasePerIntelligence * gameObject.GetComponent<Statistics> ().Intelligence);

				if (col.GetComponent<Statistics> ().Shield > col.GetComponent<Statistics> ().MaxShield)
					col.GetComponent<Statistics> ().Shield = col.GetComponent<Statistics> ().MaxShield;
			}
			allies.Remove(col.gameObject);
		}
	}

	public void RemoveShields () {
		for (int index = 0; index < allies.Count; index ++) {
			if (allies[index].name != "FireWaller")
				allies[index].GetComponent<Statistics> ().MaxShield = allies[index].GetComponent<Statistics> ().Shield = 0;
			else {
				allies[index].GetComponent<Statistics> ().MaxShield -= Mathf.CeilToInt (initialShieldAmount + increasePerIntelligence * gameObject.GetComponent<Statistics> ().Intelligence);
			
				if (allies[index].GetComponent<Statistics> ().Shield > allies[index].GetComponent<Statistics> ().MaxShield)
					allies[index].GetComponent<Statistics> ().Shield = allies[index].GetComponent<Statistics> ().MaxShield;
			}

			allies[index].transform.parent.GetComponentInChildren<EnemyShieldbar> ().UpdateFillAmount();
		}
	}
}
