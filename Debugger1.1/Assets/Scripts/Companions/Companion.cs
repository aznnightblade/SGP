using UnityEngine;
using System.Collections;

public class Companion : Statistics {

	enum StatToBuff {NONE, STRENGTH, ENDURANCE, AGILITY, DEXTERITY, INTELLIGENCE, LUCK};
	bool buffApplied = false;
	Player player = null;
	NavMeshAgent agent = null;

	[SerializeField]
	StatToBuff statToBuff = StatToBuff.NONE; // which stat should be buffed
	[SerializeField]
	int buffAmount = 0; // amount to buff that stat by
	[SerializeField]
	int initShieldBuff = 0; // amount to increase player shield level by
	[SerializeField]
	int shieldIncreasePerLevel = 50;
	[SerializeField]
	int currLevel = 1;

	// Use this for initialization
	void Start () {
		agent = gameObject.GetComponent<NavMeshAgent> ();
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ();
		agent.destination = player.GetComponent<Transform> ().position;

		maxHealth = currHealth = initialHealth + healthPerEndurance * endurance;
		critChance = initialCrit + critPerLuck * luck;
		player.Shield = player.MaxShield = initShieldBuff + shieldIncreasePerLevel * currLevel;
	}
	
	// Update is called once per frame
	void Update () {
		agent.destination = player.GetComponent<Transform> ().position;

		// apply stat buff if companion is alive, remove it when companion dies
		if (currHealth <= 0) {
			RemoveBuffs ();
			DestroyObject ();
		} else {
			AddBuffs();
		}
	}

	void AddBuffs() {
		// Only apply stat buff once
		if(!buffApplied){
			switch (statToBuff) {
			case StatToBuff.STRENGTH:
				player.Strength += buffAmount * currLevel;
				break;
			case StatToBuff.ENDURANCE:
				player.Endurance += buffAmount * currLevel;
				break;
			case StatToBuff.AGILITY:
				player.Agility += buffAmount * currLevel;
				break;
			case StatToBuff.DEXTERITY:
				player.Dexterity += buffAmount * currLevel;
				break;
			case StatToBuff.INTELLIGENCE:
				player.Intelligence += buffAmount * currLevel;
				break;
			case StatToBuff.LUCK:
				player.Luck += buffAmount * currLevel;
				break;
			case StatToBuff.NONE:
				break;
			}
		}

		buffApplied = true;
	}

	void RemoveBuffs() {
		// Only do this if player has a stat buff
		if(buffApplied) {
			switch (statToBuff) {
			case StatToBuff.STRENGTH:
				player.Strength -= buffAmount * currLevel;
				break;
			case StatToBuff.ENDURANCE:
				player.Endurance -= buffAmount * currLevel;
				break;
			case StatToBuff.AGILITY:
				player.Agility -= buffAmount * currLevel;
				break;
			case StatToBuff.DEXTERITY:
				player.Dexterity -= buffAmount * currLevel;
				break;
			case StatToBuff.INTELLIGENCE:
				player.Intelligence -= buffAmount * currLevel;
				break;
			case StatToBuff.LUCK:
				player.Luck -= buffAmount * currLevel;
				break;
			case StatToBuff.NONE:
				break;
			}

			player.Shield = player.MaxShield = 0;
		}

		buffApplied = false;
	}
}
