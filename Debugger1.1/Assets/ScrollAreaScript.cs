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
	int topVisibleIndex = 0;

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
				float buttonScrollLocation = currButton * scrollPerButton;
				float topScrollLocation = topVisibleIndex * scrollPerButton;
				float bottomScrollLocation = (topVisibleIndex + buttonsVisible) * scrollPerButton;

				if (buttonScrollLocation < topScrollLocation) {
					scrollbar.value += scrollMove;
					topVisibleIndex--;
				} else if (buttonScrollLocation >= bottomScrollLocation) {
					scrollbar.value -= scrollMove;
					topVisibleIndex++;
				}

				break;
			}
		}
	}
}
