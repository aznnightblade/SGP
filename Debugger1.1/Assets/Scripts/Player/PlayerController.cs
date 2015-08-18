using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	[SerializeField]
	Transform PlayerSprite = null;
	Player player = null;

	Vector3 moveDir = Vector3.zero;
	
	bool bulletFired = false;

	// Use this for initialization
	void Start () {
		player = GetComponentInChildren<Player> ();
		PlayerSprite = GameObject.FindGameObjectWithTag ("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 direction = Camera.main.WorldToScreenPoint (transform.position) - Input.mousePosition;
		direction.Normalize();
		float rot = (Mathf.Atan2(-direction.y, direction.x) * 180 / Mathf.PI) - 90;
		PlayerSprite.rotation = Quaternion.Euler (0, rot, 0);

		if ((Input.GetButton ("Fire1") || Input.GetButtonUp("Fire2")) && player.CurrWeapon.ShotDelay <= 0.0f) {
			bulletFired = true;

			player.CurrWeapon.ShotDelay += player.CurrWeapon.InitialShotDelay - player.CurrWeapon.ShotDelayReductionPerAgility * player.Agility;
			player.CurrWeapon.HeatGenerated += player.CurrWeapon.HeatPerShot * player.CurrWeapon.ChargeScale;
		}

		if (Input.GetButton ("Fire2") && !Input.GetButton ("Fire1") && player.CurrWeapon.ChargeDelay <= 0.0f) {
			if (player.CurrWeapon.ChargeScale < player.CurrWeapon.MaxChargeScale) {
				player.CurrWeapon.ChargeScale += player.CurrWeapon.ChargePerTick;
				player.CurrWeapon.ChargeDelay = player.CurrWeapon.DelayTime;

				if(player.CurrWeapon.ChargeScale > player.CurrWeapon.MaxChargeScale)
					player.CurrWeapon.ChargeScale = player.CurrWeapon.MaxChargeScale;
			}
		}

		for (int index = 0; index < player.Weapons.Length; index++) {
			if (player.Weapons[index].ShotDelay > 0.0f) {
				player.Weapons[index].ShotDelay -= Time.deltaTime;
			
				if (player.Weapons[index].ShotDelay < 0.0f)
					player.Weapons[index].ShotDelay = 0.0f;
			}

			if (player.Weapons[index].ChargeDelay > 0.0f) {
				player.Weapons[index].ChargeDelay -= Time.deltaTime;

				if (player.Weapons[index].ShotDelay < 0.0f)
					player.Weapons[index].ShotDelay = 0.0f;
			}

			if (player.Weapons[index].HeatGenerated > 0.0f) {
				if (player.Weapons[index].HeatGenerated >= player.Weapons[index].OverheatLevel)
					player.Weapons[index].OnCooldown = true;
			
				player.Weapons[index].HeatGenerated -= player.Weapons[index].HeatLossPerSecond * Time.deltaTime;
			
				if (player.Weapons[index].HeatGenerated < 0.0f)
					player.Weapons[index].HeatGenerated = 0.0f;
			}
		
			if (player.Weapons[index].OnCooldown) {
				if (player.Weapons[index].HeatGenerated <= 0.0f)
					player.Weapons[index].OnCooldown = false;
			}
		}
	}

	void FixedUpdate() {
		moveDir.x = Input.GetAxisRaw ("Horizontal");
		moveDir.z = Input.GetAxisRaw ("Vertical");

		if (moveDir.magnitude > 0.0f) {
			if (player.Velocity < player.MaxVelocity) {
				player.Velocity += player.Acceleration * Time.deltaTime;

				if (player.Velocity > player.MaxVelocity)
					player.Velocity = player.MaxVelocity;
			}
		}
		if (moveDir.magnitude == 0.0f) {
			if(player.Velocity > 0.0f){
				player.Velocity -= player.Acceleration * Time.deltaTime;

				if(player.Velocity < 0.0f)
					player.Velocity = 0.0f;
			}
		}

		if (moveDir.magnitude > 1.0f)
			moveDir.Normalize ();

		moveDir *= player.Velocity;

		gameObject.GetComponent<Rigidbody> ().velocity = moveDir;

		if (bulletFired)
			FireBullet ();
	}

	void FireBullet(){
		float rot = PlayerSprite.rotation.eulerAngles.y;
		Vector3 pos = player.ShotLocation.position;

		switch (player.MultithreadLevel) {
		case 1:
			CreateBullet(player.CurrWeapon.transform, pos, rot);
			break;
		case 2:
			CreateBullet(player.CurrWeapon.transform, pos, rot + 2);
			CreateBullet(player.CurrWeapon.transform, pos, rot - 2);
			break;
		case 3:
			CreateBullet(player.CurrWeapon.transform, pos, rot + 3);
			CreateBullet(player.CurrWeapon.transform, pos, rot);
			CreateBullet(player.CurrWeapon.transform, pos, rot - 3);
			break;
		case 4:
			CreateBullet(player.CurrWeapon.transform, pos, rot + 8);
			CreateBullet(player.CurrWeapon.transform, pos, rot + 3);
			CreateBullet(player.CurrWeapon.transform, pos, rot - 3);
			CreateBullet(player.CurrWeapon.transform, pos, rot - 8);
			break;
		default:
			CreateBullet(player.CurrWeapon.transform, pos, rot + 8);
			CreateBullet(player.CurrWeapon.transform, pos, rot + 3);
			CreateBullet(player.CurrWeapon.transform, pos, rot - 3);
			CreateBullet(player.CurrWeapon.transform, pos, rot - 8);
			break;
		}

		player.CurrWeapon.ChargeScale = 1.0f;
		bulletFired = false;
	}

	void CreateBullet (Transform weapon, Vector3 pos, float rot) {
		GameObject newBullet = (Instantiate (weapon, pos, Quaternion.Euler(0, rot, 0)) as Transform).gameObject;
		//GameObject newBullet = Bullet.gameObject;
		newBullet.tag = ("Player Bullet");
		newBullet.GetComponent<Weapon> ().Owner = (Statistics)player;
		newBullet.GetComponent<Weapon> ().OwnerMoveDirection = moveDir;
		newBullet.transform.localScale = newBullet.transform.localScale * player.CurrWeapon.ChargeScale;
	}
}
