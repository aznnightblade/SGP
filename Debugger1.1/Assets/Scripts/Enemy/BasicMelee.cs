using UnityEngine;
using System.Collections;

public class BasicMelee : Statistics {

	NavMeshAgent agent = null;
	Transform target = null;

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

		if (currHealth <= 0)
			DestroyObject ();
	}

	public override void OnCollisionEnter(Collision col) {
		gameObject.GetComponent<Rigidbody> ().velocity = Vector3.zero;
	}
}
