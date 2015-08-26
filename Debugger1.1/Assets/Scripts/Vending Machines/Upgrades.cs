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
	
	Player player = null;
	public GameObject panel;
	public GameObject text;
	public GameObject confirm;
	public GameObject cancel;
	bool triggerActive = false;

	void Start() {
		player = GameObject.FindGameObjectWithTag ("Player").GetComponentInChildren<Player> ();
	}

	void Update() {
		if (triggerActive == true && InputManager.instance.GetButtonDown("Submit") && (player.Money >= moneyPrice || player.EXP >= expPrice))
		{
			panel.SetActive(true);
			text.SetActive(true);
			confirm.SetActive(true);
			cancel.SetActive(true);
			player.GetComponentInParent<PlayerController>().enabled = false;
			player.GetComponentInParent<Rigidbody>().velocity = Vector3.zero;
			player.GetComponentInParent<Rigidbody>().freezeRotation = true;
		}
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Player")
		{
			triggerActive = true;
		}
	}

	void OnTriggerExit(Collider col)
	{
		if (col.tag == "Player")
		{
			triggerActive = false;
			
		}
	}

	public void ButtonClick() {
		// When you hit the switch, can you afford the upgrade?
		if (gameObject.tag == "Confirm") {
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
		
		panel.SetActive(false);
		text.SetActive(false);
		confirm.SetActive(false);
		cancel.SetActive(false);
	}

	protected virtual void UpgradeStats() {

	}

	protected virtual void UpgradeWeapons() {

	}

	protected virtual void UpgradeCompanions() {

	}
}
