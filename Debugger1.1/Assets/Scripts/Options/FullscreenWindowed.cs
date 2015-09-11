using UnityEngine;
using System.Collections;

public class FullscreenWindowed : MonoBehaviour {

	public void SwitchDisplays() {
		if (gameObject.name == "Fullscreen") {
			Camera.main.aspect = (GameManager.ScreenResolution.x / GameManager.ScreenResolution.y);
			Screen.SetResolution((int)GameManager.ScreenResolution.x, (int)GameManager.ScreenResolution.y, true);
		} else if (gameObject.name == "Windowed") {
			Screen.SetResolution((int)GameManager.ScreenResolution.x, (int)GameManager.ScreenResolution.y, false);
		}
	}
}
