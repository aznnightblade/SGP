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

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 playerPos = player.GetComponent<Transform> ().position;
	}
}
