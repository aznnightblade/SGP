using UnityEngine;
using System.Collections;

public class Justin : Statistics {
	enum UnoCards { Reverse, Skip, DrawTwo, Wild, WildDrawFour }

	[SerializeField]
	CardSpawner cardBase = null;
	[SerializeField]
	ConveyorBeltSpawner[] conveyorBelts = null;
    [SerializeField]
    Transform teleportZone = null;
	[SerializeField]
	Transform[] teleportLocations = null;
	[SerializeField]
	GameObject Teleporter = null;
	[SerializeField]
	GameObject Dampener = null;
	[SerializeField]
	Transform[] shotLocations = null;
	[SerializeField]
	Transform bullet = null;

	[SerializeField]
	float cardRespawnTimer = 20.0f;
	float respawnTimer = 0.0f;
	[SerializeField]
	int cardCountForRespawn = 5;
	bool tickedRespawntimer = false;

	[SerializeField]
	float pullCardDelay = 1.0f;
	float pullCardTimer = 0.0f;
	[SerializeField]
	int chanceForCard = 30;

	[SerializeField]
	float freezePlayerDuration = 1.5f;
	float freezeTimer = 0.0f;


	Transform target = null;


	// Use this for initialization
	void Start () {
		UpdateStats ();

		target = GameObject.FindGameObjectWithTag ("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 direction = transform.position - target.position;
		float rot = -((Mathf.Atan2(direction.z, direction.x) * 180 / Mathf.PI) + 90.0f);
		transform.rotation = Quaternion.Euler (0, rot, 0);

		if (cardBase.Cards.Count <= cardCountForRespawn && tickedRespawntimer == false) {
			respawnTimer = cardRespawnTimer;
			tickedRespawntimer = true;
		} else if (tickedRespawntimer == true && respawnTimer <= 0.0f) {
			cardBase.SpawnCards();
			tickedRespawntimer = false;
		} else if (respawnTimer > 0.0f) {
			respawnTimer -= Time.deltaTime * GameManager.CTimeScale;

			if (respawnTimer < 0.0f)
				respawnTimer = 0.0f;
		}

		if (pullCardTimer <= 0.0f) {
			int random = Random.Range(0, 100);

			if(random <= chanceForCard)
				PullCard ();

			pullCardTimer = pullCardDelay;
		} else {
			pullCardTimer -= Time.deltaTime * GameManager.CTimeScale;
			
			if (pullCardTimer < 0.0f)
				pullCardTimer = 0.0f;
		}

		if (freezeTimer <= 0.0f && GameManager.CTimeScale2 == 0.0f) {
			GameManager.CTimeScale2 = 1.0f;
		} else if (freezeTimer > 0.0f) {
			freezeTimer -= Time.deltaTime;
			
			if (freezeTimer < 0.0f)
				freezeTimer = 0.0f;
		}
	}

	void PullCard () {
		int random = Random.Range (0, (int)UnoCards.WildDrawFour);

		switch (random) {
            case 0:
                {
                    SoundManager.instance.BossSoundeffects[6].Play();
                    for (int index = 0; index < conveyorBelts.Length; index++)
                    {
                        conveyorBelts[index].MoveDirection = -conveyorBelts[index].MoveDirection;
                    }
                }
                break;
		case 1:
			if (freezeTimer <= 0.0f) {
                SoundManager.instance.BossSoundeffects[4].Play();
				freezeTimer = freezePlayerDuration;
				GameManager.CTimeScale2 = 0.0f;
			}
			break;
		case 2:
			FireBullets(2);
			break;
		case 3:
			TeleportZoneScript teleport = teleportZone.GetComponent<TeleportZoneScript> ();
                SoundManager.instance.MiscSoundeffects[3].Play();
			if(teleport.ContainsPlayer) {
				target.parent.position = teleportLocations[Random.Range(0, teleportLocations.Length)].position;
			}
			break;
		case 4:
			FireBullets(4);
			break;
		}
	}

	void FireBullets (int count) {
		Vector3 direction = Vector3.zero;
		float rot = 0;
		switch (count) {
		case 4:
			for(int index = 0; index < shotLocations.Length; index++) {
				direction = transform.position - shotLocations[index].position;
				rot = -((Mathf.Atan2(direction.z, direction.x) * 180 / Mathf.PI) + 90.0f);
				CreateBullet(transform.position, Quaternion.Euler(0, rot, 0));
			}
			break;
		case 2:
			for(int index = 1; index < shotLocations.Length - 1; index++) {
				direction = transform.position - shotLocations[index].position;
				rot = -((Mathf.Atan2(direction.z, direction.x) * 180 / Mathf.PI) + 90.0f);
				CreateBullet(transform.position, Quaternion.Euler(0, rot, 0));
			}
			break;
		}
	}

	void CreateBullet (Vector3 pos, Quaternion rot) {
		GameObject newBullet = (Instantiate (bullet, pos, rot) as Transform).gameObject;
        SoundManager.instance.BossSoundeffects[5].Play();
		//GameObject newBullet = Bullet.gameObject;
		newBullet.tag = ("Enemy Bullet");
		newBullet.GetComponent<Weapon> ().Owner = this;
	}

	public override void Damage (int damage, Transform bullet) {
		currHealth -= damage;

		if (currHealth <= 0.0f) {
			Teleporter.SetActive(true);
			Dampener.SetActive(true);
			GameObject.FindGameObjectWithTag("Player").GetComponent<Player> ().HasChargeShot = true;
			SoundManager.instance.BossSoundeffects[7].Play();
			
			DestroyObject();
		}
	}
}
