using UnityEngine;
using System.Collections;

public class KeyMappingSwitch : MonoBehaviour {

	[SerializeField]
	GameObject KeyboardPanel = null;
	[SerializeField]
	GameObject ControllerPanel = null;

	public void KeyboardSelected () {
		if (!KeyboardPanel.activeInHierarchy) {
			KeyboardPanel.SetActive(true);
			ControllerPanel.SetActive(false);
		}
	}

	public void ControllerSelected () {
		if (!ControllerPanel.activeInHierarchy) {
			ControllerPanel.SetActive(true);
			KeyboardPanel.SetActive(false);
		}
	}
}
