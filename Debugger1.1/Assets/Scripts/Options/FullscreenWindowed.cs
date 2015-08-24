using UnityEngine;
using System.Collections;

public class FullscreenWindowed : MonoBehaviour {

	public void SwitchDisplays() {
		if (gameObject.name == "Fullscreen") {
			Screen.fullScreen = true;
		} else if (gameObject.name == "Windowed") {
			Screen.fullScreen = false;
		}
	}
}
