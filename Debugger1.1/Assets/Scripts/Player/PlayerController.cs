﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	[SerializeField]
	Transform PlayerSprite = null;
	Player player = null;

	[SerializeField]
	Vector3 moveDir = Vector3.zero;
	[SerializeField]
	float playerVelocity = 0.0f;
	
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
		//moveDir = Vector3.zero;
		playerVelocity = player.Velocity;
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
		Transform Bullet = Instantiate (player.CurrWeapon.transform, player.ShotLocation.position, PlayerSprite.rotation) as Transform;
		GameObject newBullet = Bullet.gameObject;
		newBullet.tag = ("Player Bullet");
		newBullet.GetComponent<Weapon> ().Owner = player;
		newBullet.transform.localScale = newBullet.transform.localScale * player.CurrWeapon.ChargeScale;
		//newBullet.GetComponent<Weapon> ().ChargeScale = player.CurrWeapon.ChargeScale;

		player.CurrWeapon.ChargeScale = 1.0f;
		bulletFired = false;
	}
}
