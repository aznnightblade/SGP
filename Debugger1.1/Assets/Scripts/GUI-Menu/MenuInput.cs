using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuInput : EventSystem {
	[SerializeField]
	float moveDelay = 0.5f;
	float moveDelayTimer = 0.0f;
	float lastTime = 0.0f;

    [SerializeField]
	button[] Buttons = null;
	GameObject selectedButton = null;

	int currRow = 0;
	int currColumn = 0;

	override protected void Start () {
		lastTime = Time.timeSinceLevelLoad;
	}

	// Update is called once per frame
	override protected void Update () {
		float input = 0.0f;

		if (moveDelayTimer <= 0.0f) {
			if (selectedButton == null) {
				selectedButton = Buttons [currColumn].buttons [currRow];

				Buttons [currColumn].buttons [currRow].GetComponent<Button> ().Select ();
				moveDelayTimer = moveDelay;
			} else {
				if ((input = InputManager.instance.GetAxisRaw ("Horizontal")) != 0.0f) {
					if (Buttons[currColumn].buttons [currRow].tag == "Slider") {
						if (input < 0.0f) {
							Buttons[currColumn].buttons [currRow].GetComponent<Slider> ().value -= 1;
						} else if (input > 0.0f) {
							Buttons[currColumn].buttons [currRow].GetComponent<Slider> ().value += 1;
						}

						moveDelayTimer = moveDelay * 0.2f;
					} else {
						if (input < 0.0f && currColumn != 0) {
							currColumn--;
						} else if (input > 0.0f && currColumn != Buttons.Length - 1) {
							currColumn++;
						}

						if (currRow >= Buttons [currColumn].buttons.Length - 1)
							currRow = Buttons [currColumn].buttons.Length - 1;

						Buttons [currColumn].buttons [currRow].GetComponent<Button> ().Select ();
						selectedButton = Buttons [currColumn].buttons [currRow];
						moveDelayTimer = moveDelay;
					}
				} else if ((input = InputManager.instance.GetAxisRaw ("Vertical")) != 0.0f) {
					if (input < 0.0f && currRow != Buttons [currColumn].buttons.Length - 1) {
						currRow++;
					} else if (input > 0.0f && currRow != 0) {
						currRow--;
					}

					if (Buttons [currColumn].buttons [currRow].tag != "Slider")
						Buttons [currColumn].buttons [currRow].GetComponent<Button> ().Select ();
					selectedButton = Buttons [currColumn].buttons [currRow];
					moveDelayTimer = moveDelay;
				} else if ((input = InputManager.instance.GetAxisRaw ("Submit")) != 0.0f) {
					if (selectedButton != null)
						ExecuteEvents.Execute (selectedButton, null, ExecuteEvents.submitHandler);
				}
			}
		} else {
			float currTime = Time.timeSinceLevelLoad;
			moveDelayTimer -= currTime - lastTime;
			lastTime = currTime;

			if (moveDelayTimer <= 0.0f)
				moveDelayTimer = 0.0f;
		}
	}
}

[System.Serializable]
public struct button {
	public GameObject[] buttons;
}