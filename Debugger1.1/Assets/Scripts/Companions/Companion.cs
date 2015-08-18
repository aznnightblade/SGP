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
	int shieldBuff = 0; // amount to increase player shield level by

	// Use this for initialization
	void Start () {
		agent = gameObject.GetComponent<NavMeshAgent> ();
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ();
		agent.destination = player.GetComponent<Transform> ().position;
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
			
			player.Shield += shieldBuff;
		}

		buffApplied = true;
	}

	void RemoveBuffs() {
		// Only do this if player has a stat buff
		if(buffApplied) {
			switch (statToBuff) {
			case StatToBuff.STRENGTH:
				player.Strength -= buffAmount;
				break;
			case StatToBuff.ENDURANCE:
				player.Endurance -= buffAmount;
				break;
			case StatToBuff.AGILITY:
				player.Agility -= buffAmount;
				break;
			case StatToBuff.DEXTERITY:
				player.Dexterity -= buffAmount;
				break;
			case StatToBuff.INTELLIGENCE:
				player.Intelligence -= buffAmount;
				break;
			case StatToBuff.LUCK:
				player.Luck -= buffAmount;
				break;
			case StatToBuff.NONE:
				break;
			}

			player.Shield -= shieldBuff;
		}

		buffApplied = false;
	}
}
