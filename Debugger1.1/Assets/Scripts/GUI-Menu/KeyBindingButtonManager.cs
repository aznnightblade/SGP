using UnityEngine;
using System.Collections;

public class KeyBindingButtonManager : MonoBehaviour {

	[SerializeField]
	ButtonScript[] buttons = null;

	public void ButtonUpdated () {
		for (int index = 0; index < buttons.Length; index++) {
			buttons[index].UpdateKey();
		}
	}
}
