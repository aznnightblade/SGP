using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	[SerializeField]
	Transform PlayerSprite = null;
	Statistics Player = null;

	[SerializeField]
	Vector3 moveDir = Vector3.zero;
	[SerializeField]
	float playerVelocity = 0.0f;

	// Use this for initialization
	void Start () {
		Player = GetComponentInChildren<Statistics> ();
		PlayerSprite = GameObject.FindGameObjectWithTag ("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 direction = Camera.main.WorldToScreenPoint (transform.position) - Input.mousePosition;
		direction.Normalize();
		float rot = (Mathf.Atan2(-direction.y, direction.x) * 180 / Mathf.PI) - 90;
		PlayerSprite.rotation = Quaternion.Euler (0, rot, 0);
	}

	void FixedUpdate() {
		//moveDir = Vector3.zero;
		playerVelocity = Player.Velocity;
		moveDir.x = Input.GetAxisRaw ("Horizontal");
		moveDir.z = Input.GetAxisRaw ("Vertical");

		if (moveDir.magnitude > 0.0f) {
			if (Player.Velocity < Player.MaxVelocity) {
				Player.Velocity += Player.Acceleration * Time.deltaTime;

				if (Player.Velocity > Player.MaxVelocity)
					Player.Velocity = Player.MaxVelocity;
			}
		}
		if (moveDir.magnitude == 0.0f) {
			if(Player.Velocity > 0.0f){
				Player.Velocity -= Player.Acceleration * Time.deltaTime;

				if(Player.Velocity < 0.0f)
					Player.Velocity = 0.0f;
			}
		}

		if (moveDir.magnitude > 1.0f)
			moveDir.Normalize ();

		moveDir *= Player.Velocity;

		gameObject.GetComponent<Rigidbody> ().velocity = moveDir;
	}
}
