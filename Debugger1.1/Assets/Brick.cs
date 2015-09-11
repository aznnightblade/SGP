using UnityEngine;
using System.Collections;

public class Brick : MonoBehaviour {

	[SerializeField]
	int damage = 15;
	[SerializeField]
	Vector3 moveDirection = new Vector3(0, 0, -10);
	[SerializeField]
	float rotationPerSecond = 60.0f;

	Rigidbody rigid = null;

	// Use this for initialization
	void Start () {
		rigid = gameObject.GetComponent<Rigidbody> ();
		rigid.velocity = moveDirection;
	}
	
	// Update is called once per frame
	void Update () {
		if (GameManager.CTimeScale == 0.0f && rigid.velocity != Vector3.zero) {
			rigid.velocity = Vector3.zero;
		} else if (rigid.velocity != moveDirection) {
			rigid.velocity = moveDirection;
		}

		if (GameManager.CTimeScale > 0.0f) {
			float rot = transform.rotation.eulerAngles.y;

			rot += rotationPerSecond * Time.deltaTime * GameManager.CTimeScale;
			transform.rotation = Quaternion.Euler(0, rot, 0);
		}
	}

	void OnCollisionEnter (Collision col) {
		if (col.gameObject.tag == "Player" || col.gameObject.tag == "Player Controller") {
			Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player> ();
			int totalDamage = Mathf.CeilToInt(damage - player.Defense);

			if (totalDamage < 5) {
				totalDamage = 5;
			}

			player.DamagePlayer(totalDamage);

			Destroy(gameObject);
		}

		if (col.gameObject.tag == "Untagged")
			Destroy (gameObject);
	}

	public Vector3 MoveDirection {
		get { return moveDirection; }
		set { moveDirection = value; }
	}
}
