using UnityEngine;
using System.Collections;

public class Confirm : MonoBehaviour {

	//string previousMenu;

	/*[SerializeField]
	VendingMachineUI item = null;*/
	[SerializeField]
	int price = 0;
	[SerializeField]
	Player buyer = null;

	void Start() {
		//price = item.Price;
	}

	public void Purchase() {
		buyer.Money -= price;
	}

	/* public string PreviousMenu {
		get { return previousMenu;}
		set { previousMenu = value; }
	}*/
}
