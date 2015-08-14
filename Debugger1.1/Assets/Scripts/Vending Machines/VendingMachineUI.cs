using UnityEngine;
using System.Collections;

public class VendingMachineUI : MonoBehaviour {

	[SerializeField]
	int price = 0;
	[SerializeField]
	Statistics buyer = null;

	public void ConfirmPurchase() {
		if(CheckPrices())
			Application.LoadLevelAsync ("ConfirmPurchase");
	}

	// returns true if the player can afford to purchase this item; returns false otherwise
	bool CheckPrices() {
		return true;
	}
}
