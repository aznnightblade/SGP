using UnityEngine;
using System.Collections;

public class Teleporter : MonoBehaviour {

	[SerializeField]
	Transform destination = null;
	Vector3 moveDir = Vector3.zero;
	float speed = 0.0f;
    public SoundManager sounds;
	bool isActive = true;
	bool playerWarping = false;

	// Use this for initialization
	void Start () {
        sounds = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
		gameObject.GetComponent<Renderer> ().material.color = Color.green;
		Vector3 direction = (transform.position - destination.position).normalized;
		float rot = -((Mathf.Atan2(direction.z, direction.x) * 180 / Mathf.PI) + 90.0f);
		transform.rotation = Quaternion.Euler (0, rot, 0);
        sounds.MiscSoundeffects[2].loop = true;
		ParticleSystem particles = gameObject.GetComponent<ParticleSystem> ();
		particles.startSpeed = Vector3.Distance(transform.position, destination.position) / particles.startLifetime;
	}

	void FixedUpdate() {
		if (playerWarping) {
			Transform player = GameObject.FindGameObjectWithTag("Player").transform;
			Vector3 playerPos = player.position;
			Vector3 destPos = destination.position;
			playerPos.y = destPos.y = 0;
            
            if (!sounds.MiscSoundeffects[1].isPlaying && !sounds.MiscSoundeffects[2].isPlaying)
            {
                sounds.MiscSoundeffects[2].Play();
            }
			if (Vector3.Distance(playerPos, destPos) > speed * Time.fixedDeltaTime) {
				player.GetComponentInParent<Rigidbody> ().MovePosition(player.position + moveDir * speed * Time.fixedDeltaTime);
			}

			if (!gameObject.GetComponent<ParticleSystem> ().IsAlive()) {
				player.GetComponentInChildren<SpriteRenderer> ().enabled = true;
				player.GetComponentInChildren<SphereCollider> ().enabled = true;
                player.GetComponentInParent<PlayerController>().enabled = true;
				playerWarping = false;

                sounds.MiscSoundeffects[2].Stop();
                sounds.MiscSoundeffects[3].Play();
			}
		}
	}

	void OnTriggerEnter (Collider col) {
		if (col.gameObject.tag == "Player" && isActive) {
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
            sounds.MiscSoundeffects[1].Play();
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
