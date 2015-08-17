using UnityEngine;
using System.Collections;

public class Companion : Statistics {

	enum StatToBuff {NONE, STRENGTH, ENDURANCE, AGILITY, DEXTERITY, INTELLIGENCE, LUCK};
	bool playerInRange = false;

	[SerializeField]
	StatToBuff statToBuff = StatToBuff.NONE; // which stat should be buffed
	[SerializeField]
	int buffAmount = 0; // amount to buff that stat by
	[SerializeField]
	Player player = null;
	[SerializeField]
	int shieldBuff = 0; // amount to increase player shield level by
	[SerializeField]
	float maxBuffRadius = 0.0f;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 playerPos = player.GetComponent<Transform> ().position;
		if (Vector3.Distance (playerPos, transform.position) <= maxBuffRadius) {
			playerInRange = true;
		} else {
			playerInRange = false;
		}

	}

	void AddBuffs() {
		if (playerInRange) {
			switch (statToBuff) {
			case StatToBuff.STRENGTH:
				player.Strength += buffAmount;
				break;
			case StatToBuff.ENDURANCE:
				player.Endurance += buffAmount;
				break;
			case StatToBuff.AGILITY:
				player.Agility += buffAmount;
				break;
			case StatToBuff.DEXTERITY:
				player.Dexterity += buffAmount;
				break;
			case StatToBuff.INTELLIGENCE:
				player.Intelligence += buffAmount;
				break;
			case StatToBuff.LUCK:
				player.Luck += buffAmount;
				break;
			case StatToBuff.NONE:
				break;
			}
		}
	}
}
