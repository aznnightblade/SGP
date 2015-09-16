using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScrollAreaScript : MonoBehaviour {

	[SerializeField]
	Scrollbar scrollbar = null;

	GameObject[] Buttons = null;
	[SerializeField]
	int buttonsVisible = 0;
	float scrollPerButton = 0.0f;
	float totalScrollVisible = 0.0f;
	float scrollMove = 0.0f;

	// Use this for initialization
	void Start () {
		Buttons = new GameObject[transform.childCount];

		for (int child = 0; child < Buttons.Length; child++)
			Buttons[child] = transform.GetChild(child).gameObject;

		scrollPerButton = 1.0f / Buttons.Length;
		totalScrollVisible = scrollPerButton * buttonsVisible;
		scrollMove = 1.0f / (Buttons.Length - buttonsVisible);
	}

	void UpdateScrollbar (GameObject selectedButton) {
		for (int currButton = 0; currButton < Buttons.Length; currButton++) {
			if (selectedButton == Buttons[currButton]) {
				float bottomScrollLocation = 1.0f - (scrollbar.value * (1 - totalScrollVisible));
				float topScrollLocation = bottomScrollLocation - totalScrollVisible;
				float buttonScrollLocation = currButton * scrollPerButton;

				if (topScrollLocation - buttonScrollLocation > 0) {
					scrollbar.value += scrollMove;
				} else if (bottomScrollLocation - buttonScrollLocation <= 0.001f) {
					scrollbar.value -= scrollMove;
				}

				break;
			}
		}
	}
}
