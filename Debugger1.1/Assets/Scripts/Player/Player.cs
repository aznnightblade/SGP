using UnityEngine;
using System.Collections;

public class Player : Statistics {

	[SerializeField]
	Transform shotLocation = null;
	[SerializeField]
	int money = 0;
	[SerializeField]
	int exp = 0;
	[SerializeField]
	Weapon currWeapon = null;
	[SerializeField]
	Weapon[] weapons = null;

	// Use this for initialization
	void Start () {
		currHealth = maxHealth = initialHealth + healthPerEndurance * endurance;
		critChance = initialCrit + critPerLuck * luck;
		currWeapon = weapons [0];
	}
	
	// Update is called once per frame
	void Update () {
		if (currHealth == 0) {
		}
	}

	public int EXP { 
		get { return exp; }
		set { exp = value; }
	}
	public int Money { 
		get { return money; }
		set { money = value; }
	}
	public Weapon CurrWeapon { get { return currWeapon; } }
	public Weapon[] Weapons { get { return weapons; } }
	public Transform ShotLocation { get { return shotLocation; } }
}
