using UnityEngine;
using System.Collections;

public class RescaleButton : MonoBehaviour {

	public void Rescale () {
		Vector3 rect = gameObject.GetComponent<RectTransform> ().localScale;
		rect.x = rect.y = rect.z = 1.0f;
		gameObject.GetComponent<RectTransform> ().localScale = rect;
	}
}
