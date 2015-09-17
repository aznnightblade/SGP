using UnityEngine;
using System.Collections;

public class Teleporter : MonoBehaviour {

	[SerializeField]
	Transform destination = null;
	Vector3 moveDir = Vector3.zero;
	float speed = 0.0f;
	public bool newScene = false;
	bool isActive = true;
	bool playerWarping = false;
	public Dampener[] dampeners;
	public string Next = "Level3Boss";
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
		if (playerWarping && !newScene) {
			Transform player = GameObject.FindGameObjectWithTag("Player").transform;
			Vector3 playerPos = player.position;
			Vector3 destPos = destination.position;
			playerPos.y = destPos.y = 0;
            
			if (!SoundManager.instance.MiscSoundeffects[1].isPlaying && !SoundManager.instance.MiscSoundeffects[2].isPlaying)
            {
				SoundManager.instance.MiscSoundeffects[2].Play();
            }
			if (Vector3.Distance(playerPos, destPos) > speed * Time.fixedDeltaTime) {
				Vector3 moveAmount = moveDir * speed * Time.fixedDeltaTime;
				player.GetComponentInParent<Rigidbody> ().MovePosition(player.GetComponentInParent<Rigidbody>().position + moveAmount);
			}

			if (!gameObject.GetComponent<ParticleSystem> ().IsAlive()) {
				player.GetComponentInChildren<SpriteRenderer> ().enabled = true;
				player.GetComponent<SphereCollider> ().enabled = true;
				player.GetComponentInParent<PlayerController> ().enabled = true;
				player.GetComponentInParent<Rigidbody> ().isKinematic = false;
				GameManager.CTimeScale = 1.0f;
				playerWarping = false;

				Player playerStats = player.GetComponent<Player> ();

				if (playerStats.IsCompanionActive) {
					Vector3 pos = destination.position;
					pos.z += 2.0f;

					playerStats.ActiveCompanion.SetActive(true);
					playerStats.ActiveCompanion.GetComponentInChildren<NavMeshAgent> ().Warp(pos);
				}

				SoundManager.instance.MiscSoundeffects[2].Stop();
				SoundManager.instance.MiscSoundeffects[3].Play();
			}
		}
	}

	void OnTriggerEnter (Collider col) {
		if (col.gameObject.tag == "Player" && col.gameObject.name == "Player Stats" && isActive && !newScene) {
			ParticleSystem particles = gameObject.GetComponent<ParticleSystem> ();
			particles.Play();

			//col.transform.parent.position = destination.position;
			col.gameObject.GetComponentInChildren<SpriteRenderer> ().enabled = false;
			col.gameObject.GetComponent<SphereCollider> ().enabled = false;
			col.gameObject.GetComponentInParent<PlayerController> ().enabled = false;
			col.gameObject.GetComponentInParent<Rigidbody> ().velocity = Vector3.zero;
			col.gameObject.GetComponentInParent<Rigidbody> ().isKinematic = true;
			GameManager.CTimeScale = 0.0f;
			moveDir = (destination.position - col.transform.position).normalized;
			speed = Vector3.Distance(col.transform.position, destination.position) / particles.startLifetime;

			Player player = col.gameObject.GetComponent<Player> ();

			if (player.IsCompanionActive) {
				player.ActiveCompanion.SetActive(false);
			}

			playerWarping = true;
			SoundManager.instance.MiscSoundeffects[1].Play();
		}
		if (col.tag== "Player" && isActive && newScene)
		{
			GameManager.instance.NextScene();
			PlayerPrefs.SetString ("Nextscene", Next);
			Application.LoadLevel ("Loadingscreen");
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
	void Update()
	{
		int count = 0;
		for (int i = 0; i < dampeners.Length; ++i) {
			if(dampeners[i].Toggle == false)
				count++;
		}
		if (count > 0)
			ChangeState (false);
		else
			ChangeState (true);
	}
}
