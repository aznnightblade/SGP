using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuInput : MonoBehaviour {

	[SerializeField]
	GameObject initialSelected = null;

	[SerializeField]
	float moveDelay = 0.5f;
	float moveDelayTimer = 0.0f;

    [SerializeField]
	button[] Buttons = null;
	GameObject selectedButton = null;

	int currRow = 0;
	int currColumn = 0;

	[SerializeField]
	bool hasScrollbar = false;
	bool reselectButton = false;
	bool changedSelection = true;

	void Start () {

	}

	// Update is called once per frame
	void Update () {
		float input = 0.0f;

		if (reselectButton) {
			if (Buttons [currColumn].buttons [currRow].activeInHierarchy && changedSelection) {
				Buttons [currColumn].buttons [currRow].GetComponent<Button> ().Select ();
				changedSelection = false;
			}

			reselectButton = false;
		}

		if (moveDelayTimer <= 0.0f) {
			if (selectedButton == null && (InputManager.instance.GetButtonDown("Horizontal") ||
			                               InputManager.instance.GetButtonDown("Vertical") ||
			                               InputManager.instance.GetButtonDown("Submit"))) {
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
							changedSelection = true;
						} else if (input > 0.0f && currColumn != Buttons.Length - 1) {
							currColumn++;
							changedSelection = true;
						}

						if (currRow >= Buttons [currColumn].buttons.Length - 1)
							currRow = Buttons [currColumn].buttons.Length - 1;

						Buttons [currColumn].buttons [currRow].GetComponent<Button> ().Select ();
						selectedButton = Buttons [currColumn].buttons [currRow];
						moveDelayTimer = moveDelay;
					}
				} else if ((input = InputManager.instance.GetAxisRaw ("Vertical")) != 0.0f) {
                    int tempRow = currRow;
                    bool bottomReached = false;

					if (input < 0.0f && currRow != Buttons [currColumn].buttons.Length - 1) {
                        tempRow++;

                        while (!Buttons[currColumn].buttons[tempRow].activeInHierarchy) {
                            if (tempRow >= Buttons[currColumn].buttons.Length - 1) {
                                bottomReached = true;
                                break;
                            } else
                                tempRow++;
                        }

                        if (!bottomReached) {
						    currRow = tempRow;
							changedSelection = true;
						}
					} else if (input > 0.0f && currRow != 0) {
                        tempRow--;

                        while (!Buttons[currColumn].buttons[tempRow].activeInHierarchy) {
                            if (tempRow <= 0) {
                                bottomReached = true;
                                break;
                            }
                            else
                                tempRow--;
                        }

                        if (!bottomReached) {
                            currRow = tempRow;
							changedSelection = true;
						}
					}

					if (Buttons [currColumn].buttons [currRow].tag != "Slider")
						Buttons [currColumn].buttons [currRow].GetComponent<Button> ().Select ();
					else
						Buttons [currColumn].buttons [currRow].GetComponent<Slider> ().Select ();
					selectedButton = Buttons [currColumn].buttons [currRow];
					moveDelayTimer = moveDelay;

					if (hasScrollbar)
						BroadcastMessage("UpdateScrollbar", selectedButton);
				} else if (InputManager.instance.GetButtonDown ("Submit")) {
					if (selectedButton != null)
						ExecuteEvents.Execute (selectedButton, null, ExecuteEvents.submitHandler);
				} else if (InputManager.instance.GetButtonDown ("Cancel")) {
					ExecuteEvents.Execute(FindObjectOfType<EventTrigger> ().gameObject, null, ExecuteEvents.cancelHandler);
				}
			}
		} else if (!Input.anyKey && InputManager.instance.GetAxisRaw ("Vertical") == 0.0f && InputManager.instance.GetAxisRaw ("Horizontal") == 0.0f) {
			moveDelayTimer = 0.0f;
		} else if (moveDelayTimer > 0.0f) {
			moveDelayTimer -= Time.deltaTime;

			if (moveDelayTimer <= 0.0f)
				moveDelayTimer = 0.0f;
		}
	}

    void OnEnable() {
		if (InputManager.instance == null)
			return;

		if (InputManager.instance.UsingController) {			
			if (initialSelected == null) {
				for (int column = 0; column < Buttons.Length; column++) {
					for (int row = 0; row < Buttons[column].buttons.Length; row++) {
						if (Buttons[column].buttons[row].activeInHierarchy) {
							selectedButton = Buttons[column].buttons[row];
							break;
						}
					}
				}
			} else
				selectedButton = initialSelected;

			reselectButton = true;

			for (int column = 0; column < Buttons.Length; column++) {
				for (int row = 0; row < Buttons[column].buttons.Length; row++) {
					if (Buttons [column].buttons [row] == selectedButton) {
						currColumn = column;
						currRow = row;
						break;
					}
				}
			}
		}

		GameManager.InMenu = true;
    }

	void OnDisable() {
	}

    public button[] CurrButtons {
        get { return Buttons; }
        set { Buttons = value; }
    }
}

[System.Serializable]
public struct button {
	public GameObject[] buttons;
}