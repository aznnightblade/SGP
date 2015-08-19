using UnityEngine;
using System.Collections;

public class Teleporter : MonoBehaviour {

	[SerializeField]
	Transform destination = null;
	Vector3 moveDir = Vector3.zero;
	float speed = 0.0f;

	bool isActive = true;
	bool playerWarping = false;

	// Use this for initialization
	void Start () {
		gameObject.GetComponent<Renderer> ().material.color = Color.green;
		Vector3 direction = (transform.position - destination.position).normalized;
		float rot = -((Mathf.Atan2(direction.z, direction.x) * 180 / Mathf.PI) + 90.0f);
		transform.rotation = Quaternion.Euler (0, rot, 0);

		ParticleSystem particles = gameObject.GetComponent<ParticleSystem> ();
		particles.startSpeed = Vector3.Distance(transform.position, destination.position) / particles.startLifetime;
	}

	void FixedUpdate() {
		if (playerWarping) {
			Transform player = GameObject.FindGameObjectWithTag("Player").transform;

			if (Vector3.Distance(player.position, destination.position) > speed * Time.fixedDeltaTime) {
				player.GetComponentInParent<Rigidbody> ().MovePosition(player.position + moveDir * speed * Time.fixedDeltaTime);
			}

			if (!gameObject.GetComponent<ParticleSystem> ().IsAlive()) {
				player.GetComponentInChildren<SpriteRenderer> ().enabled = true;
				player.GetComponent<SphereCollider> ().enabled = true;
				player.GetComponentInParent<PlayerController> ().enabled = true;
				playerWarping = false;
			}
		}
	}

	void OnTriggerEnter (Collider col) {
		if (col.gameObject.tag == "Player") {
			ParticleSystem particles = gameObject.GetComponent<ParticleSystem> ();
			particles.Play();

			//col.transform.parent.position = destination.position;
			col.gameObject.GetComponentInChildren<SpriteRenderer> ().enabled = false;
			col.gameObject.GetComponent<SphereCollider> ().enabled = false;
			col.gameObject.GetComponentInParent<PlayerController> ().enabled = false;
			col.gameObject.GetComponentInParent<Rigidbody> ().velocity = Vector3.zero;
			moveDir = (destination.position - col.transform.position).normalized;
			speed = Vector3.Distance(col.transform.position, destination.position) / particles.startLifetime;
			playerWarping = true;
		}
	}

	void ChangeState (bool active) {
		isActive = active;

		if (isActive) {
			gameObject.GetComponent<Renderer> ().material.color = Color.green;
		} else {
			gameObject.GetComponent<Renderer> ().material.color = Color.red;
		}
	}

	void OnDrawGizmos () {
		Gizmos.color = Color.green;
		Gizmos.DrawWireCube(destination.position, transform.localScale);
	}

	public bool IsActive {
		get { return isActive; }
		set { ChangeState(value); }
	}
}
