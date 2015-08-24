using UnityEngine;
using System.Collections;

public class ColorSwitch : MonoBehaviour {

	Player player = null;
	//InputType colorSwapKey = null;

	// Use this for initialization
	void Start () {
		player = gameObject.GetComponentInChildren<Player> ();
		//colorSwapKey = InputManager.instance.keyboardInputs [7];
	}
	
	// Update is called once per frame
	void Update () {
		if (InputManager.instance.GetButtonDown("ColorSwap") && player.HasDLLs)
			SwitchColors ();
	}

	void SwitchColors() {
		switch (player.CurrWeapon.CurrColor) {
		case DLLColor.Color.RED:
			player.CurrWeapon.CurrColor = DLLColor.Color.GREEN;
			break;
		case DLLColor.Color.GREEN:
			player.CurrWeapon.CurrColor = DLLColor.Color.BLUE;
			break;
		case DLLColor.Color.BLUE:
			player.CurrWeapon.CurrColor = DLLColor.Color.RED;
			break;
		case DLLColor.Color.NEUTRAL:
			player.CurrWeapon.CurrColor = DLLColor.Color.RED;
			break;
		}
	}
}
