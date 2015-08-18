using UnityEngine;
using System.Collections;

public class Teleporter : MonoBehaviour {

	[SerializeField]
	Transform destination = null;

	bool isActive = true;

	// Use this for initialization
	void Start () {
		gameObject.GetComponent<Renderer> ().material.color = Color.green;
	}

	void OnTriggerEnter (Collider col) {
		if (col.gameObject.tag == "Player") {
			//col.transform.parent.position = destination.position;

			ParticleSystem particles = gameObject.GetComponent<ParticleSystem> ();
			particles.Play();
			particles.startSpeed = Vector3.Distance(transform.position, destination.position) / particles.startLifetime;
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
