using UnityEngine;
using System.Collections;

public class Destructor : Enemy {
	
	float playTime = 1.0f;

	[SerializeField]
	float explosionRadius = 1.0f;

	// Use this for initialization
	void Start () {
		anim = gameObject.GetComponentInChildren<Animator> ();
		anim.enabled = false;

		UpdateStats ();
	}

//	void Awake () {
//		anim = gameObject.GetComponentInChildren<Animator> ();
//		anim.enabled = false;
//		
//		UpdateStats ();
//	}
	
	// Update is called once per frame
	void Update () {

		if (currMode == Mode.Patrolling)
			UpdateWaypoints ();

		if (target != null && (currMode == Mode.Attack || currMode == Mode.Patrolling || currMode == Mode.BossRoom))
			agent.destination = target.position;

		if (currMode != Mode.Friendly) {
			if (GameManager.CTimeScale == 0.0f || anim.GetBool ("IsExploding")) {
				agent.velocity = Vector3.zero;
				agent.updateRotation = false;

				if (GameManager.CTimeScale == 0.0f)
					anim.speed = 0.0f;
			}
		
			if (GameManager.CTimeScale > 0.0f && !agent.updateRotation) {
				agent.updateRotation = true;
				anim.speed = 1.0f;
			}

			if (currMode == Mode.Attack) {
				CheckForReset ();
			} else if (currMode != Mode.Deactivated) {
				CheckForPlayer ();
			}

			if (currMode == Mode.Patrolling)
				UpdateWaypoints ();

			if (currHealth <= 0.0f || anim.GetBool ("IsExploding")) {
				if (playTime <= 0.0f) {
					DestroyObject ();
				} else {
					playTime -= Time.deltaTime * GameManager.CTimeScale;
				}
			}
		} else {
			if (GameManager.CTimeScale2 > 0.0f) {
				if (agent.enabled == true) {
					agent.enabled = false;
				}

				FaceMouse ();
			
				if (currHealth <= 0.0f || anim.GetBool ("IsExploding")) {
					if (playTime <= 0.0f) {
						DestroyObject ();
					} else {
						playTime -= Time.deltaTime * GameManager.CTimeScale2;
					}
				}
			}
		}
	}

	void OnCollisionEnter (Collision col) {
		if (currMode != Mode.Friendly && currMode != Mode.Deactivated) {
			if (col.gameObject.tag == "Player Controller") {
				Detonate ();
			}
		} else if (currMode == Mode.Friendly) {
			if (col.gameObject.tag == "Enemy") {
				Detonate ();
			}
		}
	}

	public void OnTriggerEnter (Collider col) {
		if (currMode != Mode.Friendly) {
			if (col.gameObject.tag == "Player Controller" || col.gameObject.tag == "Player") {
				Detonate ();
			}
		} else if (currMode == Mode.Friendly) {
			if (col.gameObject.tag == "Enemy") {
				Detonate ();
			}
		}
	}

	public void Detonate () {
		if (anim == null) {
			anim = gameObject.GetComponentInChildren<Animator> ();
			agent = gameObject.GetComponentInChildren<NavMeshAgent> ();
		}

		LayerMask layer;

		if (currMode != Mode.Friendly)
			layer = (1 << LayerMask.NameToLayer("Player"));
		else
			layer = (1 << LayerMask.NameToLayer("Enemy"));

		if (!anim.GetBool ("IsExploding")) {
			anim.enabled = true;
			anim.SetBool ("IsExploding", true);
			gameObject.GetComponent<SphereCollider> ().isTrigger = true;
			agent.updateRotation = false;
			agent.updatePosition = false;
			gameObject.GetComponentInChildren<Rigidbody> ().constraints = RigidbodyConstraints.FreezeAll;
		}

		Collider[] collision = Physics.OverlapSphere (transform.position, explosionRadius, layer);

		if (currMode != Mode.Friendly) {
			if (collision.Length > 0) {
				collision [0].GetComponent<Player> ().DamagePlayer (Mathf.CeilToInt (initialDamage + damagePerStrength * strength) * 2);
			}
		} else {
			if (collision.Length > 0) {
				for (int index = 0; index < collision.Length; index++) {
					collision [0].GetComponent<Enemy> ().Damage (Mathf.CeilToInt (initialDamage + damagePerStrength * strength) * 2, transform);
				}
			}
		}
	}
}