using UnityEngine;
using System.Collections;

public class FullscreenWindowed : MonoBehaviour {

	public void SwitchDisplays() {
		if (gameObject.name == "Fullscreen" && !Screen.fullScreen) {
			Screen.SetResolution((int)GameManager.ScreenResolution.x, (int)GameManager.ScreenResolution.y, true);
		} else if (gameObject.name == "Windowed" && Screen.fullScreen) {
			Screen.SetResolution((int)GameManager.ScreenResolution.x, (int)GameManager.ScreenResolution.y, false);
		}
	}
}
