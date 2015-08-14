using UnityEngine;
using System.Collections;

public class Player : Statistics {

	[SerializeField]
	int money = 0;
	[SerializeField]
	int exp = 0;

	// Use this for initialization
	void Start () {
		currHealth = maxHealth = initialHealth + healthPerEndurance * endurance;
		critChance = initialCrit + critPerLuck * luck;
	}
	
	// Update is called once per frame
	void Update () {
		if (currHealth == 0) {
		}
	}

	public int EXP { get; set; }
	public int Money { get; set; }
}
