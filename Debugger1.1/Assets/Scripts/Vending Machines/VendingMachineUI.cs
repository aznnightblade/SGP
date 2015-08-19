using UnityEngine;
using System.Collections;

public class VendingMachineUI : MonoBehaviour {

	[SerializeField]
	int currMoney = 999;
	[SerializeField]
	int price = 0;
	/*[SerializeField]
	Player buyer = null;*/
	[SerializeField]
	Confirm confirm = null;

	public void ConfirmPurchase() {
		if (CheckPrices ()) {
			//confirm.PreviousMenu = 
			//Application.LoadLevelAsync ("ConfirmPurchase");
			currMoney -= price;
		}
	}

	// returns true if the player can afford to purchase this item; returns false otherwise
	bool CheckPrices() {
		if (currMoney >= price)
			return true;
		else
			return false;
	}

	public int Price {
		get { return price; }
		set { price = value; }
	}
}
