using UnityEngine;
using System.Collections;

public class Upgrades : MonoBehaviour {

	public enum UpgradeType {NONE, STATS, WEAPONS, COMPANIONS};
	public enum CurrencyType {NONE, MONEY, EXP};

	[SerializeField]
	int moneyPrice = 0;
	[SerializeField]
	int expPrice = 0;
	[SerializeField]
	UpgradeType whatToUpgrade = UpgradeType.NONE;
	[SerializeField]
	CurrencyType currencyRequired = CurrencyType.NONE;

	int statUpgradeLevel = 1;
	int weaponUpgradeLevel = 1;
	int companionUpgradeLevel = 1;
	Player player = null;

	void Start() {
		player = GameObject.FindGameObjectWithTag ("Player").GetComponentInChildren<Player> ();
	}

	void OnCollisionStay(Collision col) {
		// When you hit the switch, can you afford the upgrade?
		if (InputManager.instance.GetButtonDown ("Submit") && (player.Money >= moneyPrice || player.EXP >= expPrice)) {
			switch (whatToUpgrade) {
			case UpgradeType.STATS:
				UpgradeStats ();
				break;
			case UpgradeType.WEAPONS:
				UpgradeWeapons ();
				break;
			case UpgradeType.COMPANIONS:
				UpgradeCompanions ();
				break;
			case UpgradeType.NONE:
				break;
			}

			switch(currencyRequired) {
			case CurrencyType.EXP:
				player.EXP -= expPrice;
				break;
			case CurrencyType.MONEY:
				player.Money -= moneyPrice;
				break;
			case CurrencyType.NONE:
				break;
			}
		}
		else if(InputManager.instance.GetButtonDown ("Submit") && (player.Money < moneyPrice || player.EXP < expPrice)) {

		}
	}

	void UpgradeStats() {

	}

	void UpgradeWeapons() {

	}

	void UpgradeCompanions() {

	}
}
