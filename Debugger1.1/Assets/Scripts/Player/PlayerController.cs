using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour {

	[SerializeField]
	Transform PlayerSprite = null;
	Player player = null;
	Transform[] playerControlledObjects = new Transform[2];
	int controlCounter = 0;

	[SerializeField]
	Vector3 moveDir = Vector3.zero;
	[SerializeField]
	Vector3 rigidVel = Vector3.zero;
	Vector3 direction = Vector3.zero;
	Vector3 previousLookDir = Vector3.zero;
	
	bool bulletFired = false;
    bool chargebullet = false;
    public Image chargemeter;
    public Image Heatmeter;
	// Use this for initialization
	void Start () {

		player = GetComponentInChildren<Player> ();
		PlayerSprite = GameObject.FindGameObjectWithTag ("Player").transform;
		PlayerControlledObjects [0] = transform;
	}
	
	// Update is called once per frame
	void Update () {
		if (GameManager.CTimeScale2 > 0.0f) {
			if (controlCounter == 0) {
				if (Time.timeScale > 0.0f) {
					if (InputManager.instance.UsingController == false) {
						previousLookDir = direction;
						direction = Camera.main.WorldToScreenPoint (transform.position) - Input.mousePosition;
					} else {
						previousLookDir = direction;
						direction = new Vector3 (InputManager.instance.GetAxisRaw ("Horizontal2"), InputManager.instance.GetAxisRaw ("Vertical2"), 0);

						if (direction == Vector3.zero)
							direction = previousLookDir;
					}
				}

				direction.Normalize ();
				float rot = (Mathf.Atan2 (-direction.y, direction.x) * 180 / Mathf.PI) - 90;
				PlayerSprite.rotation = Quaternion.Euler (0, rot, 0);
					
				if (((InputManager.instance.GetButton ("Fire1") || InputManager.instance.GetButtonUp ("Fire2")) && 
				     (player.CurrWeapon.ShotDelay <= 0.0f && !player.CurrWeapon.OnCooldown))) {
					bulletFired = true;
	
					player.CurrWeapon.ShotDelay += player.CurrWeapon.InitialShotDelay - player.CurrWeapon.ShotDelayReductionPerAgility * player.Agility;
					player.CurrWeapon.HeatGenerated += player.CurrWeapon.HeatPerShot * player.CurrWeapon.ChargeScale;
				}

				if (InputManager.instance.GetButton ("Fire2") && !InputManager.instance.GetButton ("Fire1") &&
				    player.CurrWeapon.ChargeDelay <= 0.0f && !player.CurrWeapon.OnCooldown) {
					if (player.CurrWeapon.ChargeScale == 1.0f)
	                    SoundManager.instance.PlayerSoundeffects[1].Play();

        	        if (!SoundManager.instance.PlayerSoundeffects[1].isPlaying && !SoundManager.instance.PlayerSoundeffects[2].isPlaying)
        	            SoundManager.instance.PlayerSoundeffects[2].Play();

    	            if ((player.CurrWeapon.ChargeScale < player.CurrWeapon.MaxChargeScale) && player.HasChargeShot)
    	            {
						player.CurrWeapon.ChargeScale += player.CurrWeapon.ChargePerTick;
						player.CurrWeapon.ChargeDelay = player.CurrWeapon.DelayTime;
						chargebullet = true;
               
						if (player.CurrWeapon.ChargeScale > player.CurrWeapon.MaxChargeScale)
							player.CurrWeapon.ChargeScale = player.CurrWeapon.MaxChargeScale;	
					}
				}

				if (InputManager.instance.GetButtonDown ("Fire3")) {
					player.Breakpoint.FireBreakpoint ();
    	            SoundManager.instance.WeaponSoundeffects[1].Play();
				}

				if (player.HasDLLs) {
					if (InputManager.instance.GetButtonDown ("ColorSwap")) {
						if (InputManager.instance.GetAxisRaw ("ColorSwap") > 0)
							NextColor ();
						else
							PrevColor ();
					}
				}

				if (player.Weapons.Length > 1) {
					if (InputManager.instance.GetButtonDown ("WeaponSwap")) {
						if (InputManager.instance.GetAxisRaw ("WeaponSwap") > 0) {
							NextWeapon ();
						} else {
							PrevWeapon ();
						}
					}
				}

				if (player.HasNegationBoots) {
					if (InputManager.instance.GetButtonDown ("Hover")) {
						player.IsHovering = !player.IsHovering;
					}
				}
			}

			if (InputManager.instance.GetButtonDown ("FriendToggle")) {
				if (player.Friend != null) {
					if (playerControlledObjects[1] == null) {
						player.Friend.gameObject.SetActive(true);
						playerControlledObjects[1] = player.Friend.GetChild(0);
						Camera.main.GetComponent<CameraFollow> ().Target = PlayerControlledObjects[1];
						Enemy friend = playerControlledObjects[1].gameObject.GetComponent<Enemy> ();

						if (friend != null)
							friend.enabled = true;

						controlCounter = 1;
					} else {
						player.Friend.gameObject.SetActive(false);
						playerControlledObjects[1] = null;
						Camera.main.GetComponent<CameraFollow> ().Target = PlayerSprite;
						controlCounter = 0;
					}
				}
			}

			for (int index = 0; index < player.Weapons.Length; index++) {
				if (player.Weapons [index].ShotDelay > 0.0f) {
					player.Weapons [index].ShotDelay -= Time.deltaTime;
			
					if (player.Weapons [index].ShotDelay < 0.0f)
						player.Weapons [index].ShotDelay = 0.0f;
				}

				if (player.Weapons [index].ChargeDelay > 0.0f) {
					player.Weapons [index].ChargeDelay -= Time.deltaTime;

					if (player.Weapons [index].ShotDelay < 0.0f)
						player.Weapons [index].ShotDelay = 0.0f;
				}

				if (player.Weapons [index].HeatGenerated > 0.0f) {
					if (player.Weapons [index].HeatGenerated >= player.Weapons [index].OverheatLevel)
						player.Weapons [index].OnCooldown = true;
			
					player.Weapons [index].HeatGenerated -= player.Weapons [index].HeatLossPerSecond * Time.deltaTime;
			
					if (player.Weapons [index].HeatGenerated < 0.0f)
						player.Weapons [index].HeatGenerated = 0.0f;
				}
		
				if (player.Weapons [index].OnCooldown) {
					if (player.Weapons [index].HeatGenerated <= 0.0f)
						player.Weapons [index].OnCooldown = false;
				}
			}
			chargemeter.fillAmount = player.CurrWeapon.ChargeScale - 1;
			Heatmeter.fillAmount = player.CurrWeapon.HeatGenerated / player.CurrWeapon.OverheatLevel;
		} else {
			gameObject.GetComponent<Rigidbody> ().velocity = Vector3.zero;
		}
	}

	void FixedUpdate() {
		rigidVel = playerControlledObjects [controlCounter].GetComponent<Rigidbody> ().velocity;

		if (GameManager.CTimeScale2 > 0.0f) {
			moveDir.x = InputManager.instance.GetAxisRaw ("Horizontal");
			moveDir.z = InputManager.instance.GetAxisRaw ("Vertical");

			if (moveDir.magnitude > 0.0f) {
				if (player.Velocity < player.MaxVelocity) {
					player.Velocity += player.Acceleration * Time.deltaTime;

					if (player.Velocity > player.MaxVelocity)
						player.Velocity = player.MaxVelocity;
				}
			}
			if (moveDir.magnitude == 0.0f) {
				if (player.Velocity > 0.0f) {
					player.Velocity -= player.Acceleration * Time.deltaTime;

					if (player.Velocity < 0.0f)
						player.Velocity = 0.0f;
				}
			}

			if (moveDir.magnitude > 1.0f)
				moveDir.Normalize ();

			moveDir *= player.Velocity;

			playerControlledObjects[controlCounter].GetComponent<Rigidbody> ().velocity = moveDir;

			if (bulletFired)
				FireBullet ();
		}
	}

	void NextColor () {
		player.CurrWeapon.CurrColor = player.NextColor;

		if (player.NextColor == DLLColor.Color.BLUE)
			player.NextColor = DLLColor.Color.NEUTRAL;
		else
			player.NextColor++;
	}
	
	void PrevColor () {
		player.CurrWeapon.CurrColor = player.PrevColor;
		
		if (player.PrevColor == DLLColor.Color.NEUTRAL)
			player.PrevColor = DLLColor.Color.BLUE;
		else
			player.PrevColor--;
	}

	void PrevWeapon () {
		player.CurrWeaponCounter--;

		if (player.CurrWeaponCounter == -1)
			player.CurrWeaponCounter = player.Weapons.Length - 1;

		player.CurrWeapon = player.Weapons [player.CurrWeaponCounter];
	}

	void NextWeapon () {
		player.CurrWeaponCounter++;
		
		if (player.CurrWeaponCounter == player.Weapons.Length)
			player.CurrWeaponCounter = 0;

		player.CurrWeapon = player.Weapons [player.CurrWeaponCounter];
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

	public GameObject CreateBullet (Transform weapon, Vector3 pos, float rot) {
		GameObject newBullet = (Instantiate (weapon, pos, Quaternion.Euler (0, rot, 0)) as Transform).gameObject;
		newBullet.tag = ("Player Bullet");
		newBullet.GetComponent<Weapon> ().Owner = (Statistics)player;
		newBullet.GetComponent<Weapon> ().CurrColor = player.CurrWeapon.CurrColor;
		newBullet.GetComponent<Weapon> ().OwnerMoveDirection = moveDir;
		newBullet.GetComponent<Weapon> ().ChargeScale = player.CurrWeapon.ChargeScale;
		newBullet.transform.localScale = newBullet.transform.localScale * player.CurrWeapon.ChargeScale;
        if (chargebullet==true)
        {
            chargebullet = false;
            SoundManager.instance.PlayerSoundeffects[1].Stop();
            SoundManager.instance.PlayerSoundeffects[2].Stop();
            SoundManager.instance.PlayerSoundeffects[3].Play();
        }
        else if(chargebullet==false)
        {
            SoundManager.instance.PlayerSoundeffects[0].Play();
        }

		return newBullet;
	}

	public Transform[] PlayerControlledObjects { get { return playerControlledObjects; } }
	public int ControlCounter {
		get { return controlCounter; }
		set { controlCounter = value; }
	}
}
