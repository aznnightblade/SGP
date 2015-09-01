using UnityEngine;
using System.Collections;

public class Destructor : Statistics {

	NavMeshAgent agent = null;
	Transform target = null;
	Animator anim = null;

	float playTime = 1.0f;

	[SerializeField]
	float explosionRadius = 1.0f;

	// Use this for initialization
	void Start () {
		UpdateStats ();

		agent = GetComponent<NavMeshAgent> ();
		target = GameObject.FindGameObjectWithTag ("Player").transform;
		anim = gameObject.GetComponentInChildren<Animator> ();
		anim.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (target != null)
			agent.destination = target.position;

		if (GameManager.CTimeScale == 0.0f || anim.GetBool("IsExploding")) {
			agent.velocity = Vector3.zero;
			agent.updateRotation = false;

			if (GameManager.CTimeScale == 0.0f)
				anim.speed = 0.0f;
		}
		
		if (GameManager.CTimeScale > 0.0f && !agent.updateRotation) {
			agent.updateRotation = true;
			anim.speed = 1.0f;
		}

		if (currHealth <= 0.0f || anim.GetBool("IsExploding")) {
			if (playTime <= 0.0f) {
				DestroyObject();
			} else {
				playTime -= Time.deltaTime * GameManager.CTimeScale;
			}
		}
	}

	public override void OnCollisionEnter (Collision col) {
		if (col.gameObject.tag == "Player Controller") {
			Detonate ();
		}
	}

	public void OnTriggerEnter (Collider col) {
		if (col.gameObject.tag == "Player Controller" || col.gameObject.tag == "Player") {
			Detonate ();
		}
	}

	public void Detonate () {
		LayerMask layer = (1 << LayerMask.NameToLayer("Player"));

		if (!anim.GetBool ("IsExploding")) {
			anim.enabled = true;
			anim.SetBool ("IsExploding", true);
			gameObject.GetComponent<SphereCollider> ().isTrigger = true;
			agent.updateRotation = false;
			agent.updatePosition = false;
		}

		Collider[] collision = Physics.OverlapSphere (transform.position, explosionRadius, layer);

		if (collision.Length > 0) {
			collision[0].GetComponent<Player> ().DamagePlayer (Mathf.CeilToInt(initialDamage + damagePerStrength * strength) * 2);
		}
	}
}