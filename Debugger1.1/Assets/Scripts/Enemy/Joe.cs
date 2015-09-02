using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Joe : Statistics {

	[SerializeField]
    GameObject[] BossSwitches = null;
	[SerializeField]
	GameObject Teleporter = null;
	[SerializeField]
	Transform Explosion = null;

	[SerializeField]
	float vulnerableTime = 2.0f;
	float vulnerableTimer = 0.0f;
	bool vulnerable = false;

	[SerializeField]
	float teleportTime = 4.0f;
	float teleportTimer = 4.0f;

	float checkForWinDelay = 0.25f;
	float checkTimer = 0.0f;

	// Use this for initialization
	void Start () {
		BossSwitches [4].GetComponent<BossSwitches> ().JoesChoice ();
		Teleporter.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (vulnerable) {
			if (vulnerableTimer > 0.0f) {
				vulnerableTimer -= Time.deltaTime * GameManager.CTimeScale;

				if (vulnerableTimer < 0.0f)
					vulnerableTimer = 0.0f;
			} else {
				vulnerable = false;

				ResetSwitches ();
			}
		} else {
			if (teleportTimer > 0.0f) {
				teleportTimer -= Time.deltaTime * GameManager.CTimeScale;

				if (teleportTimer < 0.0f)
					teleportTimer = 0.0f;
			}

			if (checkTimer > 0.0f) {
				checkTimer -= Time.deltaTime * GameManager.CTimeScale;
				
				if (checkTimer < 0.0f)
					checkTimer = 0.0f;
			}

			if (checkTimer <= 0.0f) {
				checkTimer = checkForWinDelay;
				CheckForWin ();
				CheckActive ();
			}

			if (teleportTimer <= 0.0f) {
				teleportTimer = teleportTime;
				Teleport ();
			}
		}
	}

	public override void Damage (int damage, Transform bullet) {
		if (!vulnerable) {
			damage = Mathf.CeilToInt(damage * 0.2f);
		}

		currHealth -= damage;

		if (currHealth <= 0.0f) {
			Teleporter.SetActive (true);
            SoundManager.instance.BossSoundeffects[3].Play();
			GameObject.FindGameObjectWithTag("Player").GetComponent<Player> ().HasDLLs = true;

			DestroyObject();
		}
	}

	void Teleport () {
		List<int> teleportLocations = new List<int> ();

		for (int index = 0; index < BossSwitches.Length; index++) {
			if (BossSwitches[index].GetComponent<BossSwitches> ().Health != 0)
				teleportLocations.Add(index);
		}

		if (teleportLocations.Count > 0) {
			int teleportTo = Random.Range (0, teleportLocations.Count);
			transform.position = new Vector3 (BossSwitches [teleportLocations [teleportTo]].transform.position.x,
		                                  transform.position.y, BossSwitches [teleportLocations [teleportTo]].transform.position.z);

			FlipSwitch (teleportLocations [teleportTo]);
		}

		CheckForWin ();
	}

	void ResetSwitches () {
		for (int index = 0; index < BossSwitches.Length; index++) {
			BossSwitches[index].GetComponent<BossSwitches> ().Reset();
		}
	}

	void FlipSwitch (int Switch) {
		BossSwitches [Switch].GetComponent<BossSwitches> ().JoesChoice ();
	}

	void CheckForWin () {
		BossSwitches[] Switches = new BossSwitches[9];
		bool win = false;

		for (int index = 0; index < BossSwitches.Length; index++) {
			Switches[index] = BossSwitches[index].GetComponent<BossSwitches> ();
		}

		int player = 0;
		for (; player < 2; player++) {
			if (Switches[0].CheckStatus(player)) {
				if (Switches[1].CheckStatus(player) && Switches[2].CheckStatus(player) || Switches[3].CheckStatus(player) && Switches[5].CheckStatus(player) ||
				    Switches[4].CheckStatus(player) && Switches[8].CheckStatus(player)) {
					win = true;
					break;
				}
			} else if (Switches[1].CheckStatus(player) && Switches[4].CheckStatus(player) && Switches[8].CheckStatus(player)) {
				win = true;
				break;
			} else if (Switches[2].CheckStatus(player)) {
				if(Switches[4].CheckStatus(player) && Switches[6].CheckStatus(player) || Switches[5].CheckStatus(player) && Switches[8].CheckStatus(player)) {
					win = true;
					break;
				}
			} else if (Switches[3].CheckStatus(player) && Switches[4].CheckStatus(player) && Switches[5].CheckStatus(player)) {
				win = true;
				break;
			} else if (Switches[6].CheckStatus(player) && Switches[7].CheckStatus(player) && Switches[8].CheckStatus(player)) {
				win = true;
				break;
			}
		}

		if (win) {
            SoundManager.instance.BossSoundeffects[0].Play();
			if (player == 0) {
				BossWin();
			} else {
				PlayerWin();
			}
		}
	}

	void CheckActive () {
		BossSwitches[] Switches = new BossSwitches[9];
		bool allActive = true;

		for (int index = 0; index < BossSwitches.Length; index++) {
			Switches[index] = BossSwitches[index].GetComponent<BossSwitches> ();
		}

		for (int index = 0; index < BossSwitches.Length; index++) {
			if (!Switches[index].CheckStatus(0) && !Switches[index].CheckStatus(0)) {
				allActive = false;
				break;
			}
		}

		if (allActive)
			ResetSwitches ();
	}

	void PlayerWin () {
		vulnerableTimer = vulnerableTime;
		vulnerable = true;
	}

	void BossWin () {
        SoundManager.instance.BossSoundeffects[1].Play();
		for (int index = 0; index < BossSwitches.Length; index++) {
			Vector3 pos = BossSwitches[index].transform.position;
			pos.y += .25f;
			Instantiate(Explosion, pos, Quaternion.Euler(90, 0, 0));
		}

		ResetSwitches ();
	}
}
