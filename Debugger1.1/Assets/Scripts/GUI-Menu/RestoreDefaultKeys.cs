using UnityEngine;
using System.Collections;

public class RestoreDefaultKeys : MonoBehaviour {

	[SerializeField]
	GameObject KeyboardPanel = null;
	[SerializeField]
	KeyBindingButtonManager KeyboardButtonManager = null;
	[SerializeField]
	GameObject ControllerPanel = null;
	[SerializeField]
	KeyBindingButtonManager ControllerButtonManager = null;

	public void OnClick () {
		if (KeyboardPanel.activeInHierarchy) {
			InputManager.instance.SetDefaults(false);
			KeyboardButtonManager.ButtonUpdated();
		} else if (ControllerPanel.activeInHierarchy) {
			InputManager.instance.SetDefaults(true);
			ControllerButtonManager.ButtonUpdated();
		}
	}
}
