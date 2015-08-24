using UnityEngine;
using System;
using System.Collections;

public class InputManager : MonoBehaviour {
	public static InputManager instance { get; private set; }
	
	[SerializeField]
	InputType[] KeyboardInputs = null;
	[SerializeField]
	InputType[] ControllerInputs = null;
	InputType[] KeyboardDefaults = null;
	InputType[] ControllerDefaults = null;

	static bool usingController = false;

	// Use this for initialization
	void Start () {
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);

		DontDestroyOnLoad (gameObject);

		KeyboardDefaults = new InputType[KeyboardInputs.Length];
		for (int input = 0; input < KeyboardInputs.Length; input++) {
			KeyboardDefaults[input] = KeyboardInputs[input];
		}

		ControllerDefaults = new InputType[ControllerInputs.Length];
		for (int input = 0; input < ControllerInputs.Length; input++) {
			ControllerDefaults[input] = ControllerInputs[input];
		}

		LoadInputs ();
	}

	void Update() {
		for (int index = 0; index < ControllerInputs.Length; index++) {
			if (ControllerInputs[index].axis == InputType.Axes.NonAxis)
				continue;

			if (ControllerInputs[index].axisDown == true) {
				if (GetAxisFromInput(ControllerInputs[index]) == 0.0f)
					ControllerInputs[index].axisDown = false;
			} else {
				if (GetAxisFromInput(ControllerInputs[index]) != 0.0f)
					ControllerInputs[index].axisDown = true;
			}
		}
	}

	public void SaveInputs () {
		string KeyboardButtons = "";
		string ControllerButtons = "";

		for (int key = 0; key < KeyboardInputs.Length; key++) {
			KeyboardButtons = KeyboardButtons + '*' + KeyboardInputs[key].Name + '*' + KeyboardInputs[key].Negative + '*' +
				KeyboardInputs[key].Positive + '*' + KeyboardInputs[key].AltNegative + '*' + KeyboardInputs[key].AltPositive;
		}

		for (int key = 0; key < ControllerInputs.Length; key++) {
			ControllerButtons = ControllerButtons + '*' + ControllerInputs[key].Name + '*' + ControllerInputs[key].Negative + '*' +
				ControllerInputs[key].Positive + '*' + ControllerInputs[key].AltNegative + '*' + ControllerInputs[key].AltPositive;
		}

		PlayerPrefs.SetString ("KeyboardKeys", KeyboardButtons);
		PlayerPrefs.SetString ("ControllerKeys", ControllerButtons);
	}

	public void LoadInputs () {
		if (PlayerPrefs.HasKey ("KeyboardKeys")) {
			string KeyboardButtons = PlayerPrefs.GetString("KeyboardKeys");
			string[] Inputs = KeyboardButtons.Split('*');
			for (int key = 0; key < KeyboardInputs.Length; key++) {
				KeyboardInputs[key].Name = Inputs[key * 5];
				KeyboardInputs[key].Negative = (KeyCode)Convert.ToInt32(Inputs[key * 5 + 1]);
				KeyboardInputs[key].Positive = (KeyCode)Convert.ToInt32(Inputs[key * 5 + 2]);
				KeyboardInputs[key].AltNegative = (KeyCode)Convert.ToInt32(Inputs[key * 5 + 3]);
				KeyboardInputs[key].AltPositive = (KeyCode)Convert.ToInt32(Inputs[key * 5 + 4]);
			}
		}

		if (PlayerPrefs.HasKey ("ControllerKeys")) {
			string ControllerButtons = PlayerPrefs.GetString("ControllerKeys");
			string[] Inputs = ControllerButtons.Split('*');
			for (int key = 0; key < ControllerInputs.Length; key++) {
				ControllerInputs[key].Name = Inputs[key * 5];
				ControllerInputs[key].Negative = (KeyCode)Convert.ToInt32(Inputs[key * 5 + 1]);
				ControllerInputs[key].Positive = (KeyCode)Convert.ToInt32(Inputs[key * 5 + 2]);
				ControllerInputs[key].AltNegative = (KeyCode)Convert.ToInt32(Inputs[key * 5 + 3]);
				ControllerInputs[key].AltPositive = (KeyCode)Convert.ToInt32(Inputs[key * 5 + 4]);
			}
		}
	}

	public void SetDefaults () {
		for (int input = 0; input < KeyboardInputs.Length; input++) {
			KeyboardInputs[input] = KeyboardDefaults[input];
		}

		for (int input = 0; input < ControllerInputs.Length; input++) {
			ControllerInputs[input] = ControllerDefaults[input];
		}
	}

	public bool GetButtonDown (string button) {
		for (int keys = 0; keys < KeyboardInputs.Length; keys++) {
			if (button == KeyboardInputs[keys].Name) {
				if (Input.GetKeyDown(KeyboardInputs[keys].Negative) || Input.GetKeyDown(KeyboardInputs[keys].Positive) ||
				    Input.GetKeyDown(KeyboardInputs[keys].AltNegative) || Input.GetKeyDown(KeyboardInputs[keys].AltPositive)) {
					if (usingController)
						usingController = false;

					return true;
				}
			}
		}

		for (int keys = 0; keys < ControllerInputs.Length; keys++) {
			if (button == ControllerInputs[keys].Name) {
				if (ControllerInputs[keys].axis == InputType.Axes.NonAxis) {
					if (Input.GetKeyDown(ControllerInputs[keys].Negative) || Input.GetKeyDown(ControllerInputs[keys].Positive) ||
					    Input.GetKeyDown(ControllerInputs[keys].AltNegative) || Input.GetKeyDown(ControllerInputs[keys].AltPositive)){
						if(!usingController)
							usingController = true;
	
						return true;
					}
				} else {
					if(GetAxisFromInput(ControllerInputs[keys]) != 0.0f && ControllerInputs[keys].axisDown == false) {
						if(!usingController)
							usingController = true;

						ControllerInputs[keys].axisDown = true;
						
						return true;
					}
				}
			}
		}

		return false;
	}

	public bool GetButton (string button) {
		for (int keys = 0; keys < KeyboardInputs.Length; keys++) {
			if (button == KeyboardInputs[keys].Name) {
				if (Input.GetKey(KeyboardInputs[keys].Negative) || Input.GetKey(KeyboardInputs[keys].Positive) ||
				    Input.GetKey(KeyboardInputs[keys].AltNegative) || Input.GetKey(KeyboardInputs[keys].AltPositive)) {
					if (usingController)
						usingController = false;
					
					return true;
				}
			}
		}
		
		for (int keys = 0; keys < ControllerInputs.Length; keys++) {
			if (button == ControllerInputs[keys].Name) {
				if (ControllerInputs[keys].axis == InputType.Axes.NonAxis) {
					if (Input.GetKey(ControllerInputs[keys].Negative) || Input.GetKey(ControllerInputs[keys].Positive) ||
					    Input.GetKey(ControllerInputs[keys].AltNegative) || Input.GetKey(ControllerInputs[keys].AltPositive)){
						if(!usingController)
							usingController = true;
					
						return true;
					}
				} else {
					if (GetAxisFromInput(ControllerInputs[keys]) != 0.0f) {
						if(!usingController)
							usingController = true;

						ControllerInputs[keys].axisDown = true;

						return true;
					}
				}
			}
		}
		
		return false;
	}

	public bool GetButtonUp (string button) {
		for (int keys = 0; keys < KeyboardInputs.Length; keys++) {
			if (button == KeyboardInputs[keys].Name) {
				if (Input.GetKeyUp(KeyboardInputs[keys].Negative) || Input.GetKeyUp(KeyboardInputs[keys].Positive) ||
				    Input.GetKeyUp(KeyboardInputs[keys].AltNegative) || Input.GetKeyUp(KeyboardInputs[keys].AltPositive)) {
					if (usingController)
						usingController = false;
					
					return true;
				}
			}
		}
		
		for (int keys = 0; keys < ControllerInputs.Length; keys++) {
			if (button == ControllerInputs[keys].Name) {
				if (ControllerInputs[keys].axis == InputType.Axes.NonAxis) {
					if (Input.GetKeyUp(ControllerInputs[keys].Negative) || Input.GetKeyUp(ControllerInputs[keys].Positive) ||
					    Input.GetKeyUp(ControllerInputs[keys].AltNegative) || Input.GetKeyUp(ControllerInputs[keys].AltPositive)){
						if(!usingController)
							usingController = true;
					
						return true;
					}
				} else {
					if(GetAxisFromInput(ControllerInputs[keys]) == 0.0f && ControllerInputs[keys].axisDown == true) {
						if(!usingController)
							usingController = true;

						ControllerInputs[keys].axisDown = false;
						
						return true;
					}
				}
			}
		}

		return false;
	}

	public float GetAxisRaw (string button) {
		float Axis = 0.0f;

		for (int keys = 0; keys < KeyboardInputs.Length; keys++) {
			if (button == KeyboardInputs[keys].Name) {
				Axis = 1 * (Mathf.Ceil(Convert.ToInt32(Input.GetKey(KeyboardInputs[keys].Positive)) * 0.5f + Convert.ToInt32(Input.GetKey(KeyboardInputs[keys].AltPositive)) * 0.5f)) -
					1 * (Mathf.Ceil(Convert.ToInt32(Input.GetKey(KeyboardInputs[keys].Negative)) * 0.5f + Convert.ToInt32(Input.GetKey(KeyboardInputs[keys].AltNegative)) * 0.5f));

				if (Axis != 0.0f) {
					if(usingController)
						usingController = false;
				}
			}
		}


		for (int keys = 0; keys < ControllerInputs.Length; keys++) {
			if (button == ControllerInputs [keys].Name) {
				float CAxis = 0.0f;

				if (ControllerInputs[keys].axis == InputType.Axes.NonAxis) {
					CAxis = 1 * (Mathf.Ceil (Convert.ToInt32 (Input.GetKey (ControllerInputs [keys].Positive)) * 0.5f + Convert.ToInt32 (Input.GetKey (ControllerInputs [keys].AltPositive)) * 0.5f)) -
						1 * (Mathf.Ceil (Convert.ToInt32 (Input.GetKey (ControllerInputs [keys].Negative)) * 0.5f + Convert.ToInt32 (Input.GetKey (ControllerInputs [keys].AltNegative)) * 0.5f));
				} else {
					CAxis = GetAxisFromInput(ControllerInputs[keys]);
				}

				Axis += CAxis;

				if(!usingController && CAxis != 0)
					usingController = true;
			}
		}

		if (Axis > 1.0f)
			Axis = 1.0f;
		else if (Axis < -1.0f)
			Axis = -1.0f;

		return Axis;
	}

	public float GetAxisFromInput (InputType button) {
		switch ((int)button.axis) {
		case 0:
			return Input.GetAxis("AxisX");
		case 1:
			return Input.GetAxis("AxisY");
		case 2:
			return Input.GetAxis("Axis3");
		case 3:
			return Input.GetAxis("Axis4");
		case 4:
			return Input.GetAxis("Axis5");
		case 5:
			return Input.GetAxis("Axis6");
		case 6:
			return Input.GetAxis("Axis7");
		case 7:
			return Input.GetAxis("Axis8");
		case 8:
			return Input.GetAxis("Axis9");
		case 9:
			return Input.GetAxis("Axis10");
		default:
			return 0;
		}
	}

	public void ChangeKey (bool controller, int input, int selection, KeyCode key) {
		if (!controller) {
			switch(selection){
			case 0:
				for (int index = 0; index < KeyboardInputs.Length; index++) {
					if(KeyboardInputs[index].Negative == key) {
						KeyboardInputs[index].Negative = KeyboardInputs[input].Negative;
						break;
					} else if (KeyboardInputs[index].Negative == key) {
						KeyboardInputs[index].Positive = KeyboardInputs[input].Negative;
						break;
					} else if (KeyboardInputs[index].Negative == key) {
						KeyboardInputs[index].AltNegative = KeyboardInputs[input].Negative;
						break;
					} else {
						KeyboardInputs[index].AltPositive = KeyboardInputs[input].Negative;
						break;
					}
				}

				KeyboardInputs[input].Negative = key;
				break;
			case 1:
				for (int index = 0; index < KeyboardInputs.Length; index++) {
					if(KeyboardInputs[index].Negative == key) {
						KeyboardInputs[index].Negative = KeyboardInputs[input].Positive;
						break;
					} else if (KeyboardInputs[index].Negative == key) {
						KeyboardInputs[index].Positive = KeyboardInputs[input].Positive;
						break;
					} else if (KeyboardInputs[index].Negative == key) {
						KeyboardInputs[index].AltNegative = KeyboardInputs[input].Positive;
						break;
					} else {
						KeyboardInputs[index].AltPositive = KeyboardInputs[input].Positive;
						break;
					}
				}

				KeyboardInputs[input].Positive = key;
				break;
			case 2:
				for (int index = 0; index < KeyboardInputs.Length; index++) {
					if(KeyboardInputs[index].Negative == key) {
						KeyboardInputs[index].Negative = KeyboardInputs[input].AltNegative;
						break;
					} else if (KeyboardInputs[index].Negative == key) {
						KeyboardInputs[index].Positive = KeyboardInputs[input].AltNegative;
						break;
					} else if (KeyboardInputs[index].Negative == key) {
						KeyboardInputs[index].AltNegative = KeyboardInputs[input].AltNegative;
						break;
					} else {
						KeyboardInputs[index].AltPositive = KeyboardInputs[input].AltNegative;
						break;
					}
				}

				KeyboardInputs[input].AltNegative = key;
				break;
			case 3:
				for (int index = 0; index < KeyboardInputs.Length; index++) {
					if(KeyboardInputs[index].Negative == key) {
						KeyboardInputs[index].Negative = KeyboardInputs[input].AltPositive;
						break;
					} else if (KeyboardInputs[index].Negative == key) {
						KeyboardInputs[index].Positive = KeyboardInputs[input].AltPositive;
						break;
					} else if (KeyboardInputs[index].Negative == key) {
						KeyboardInputs[index].AltNegative = KeyboardInputs[input].AltPositive;
						break;
					} else {
						KeyboardInputs[index].AltPositive = KeyboardInputs[input].AltPositive;
						break;
					}
				}

				KeyboardInputs[input].AltPositive = key;
				break;
			}
		} else {
			switch(selection){
			case 0:
				for (int index = 0; index < ControllerInputs.Length; index++) {
					if(ControllerInputs[index].Negative == key) {
						ControllerInputs[index].Negative = ControllerInputs[input].Negative;
						break;
					} else if (ControllerInputs[index].Negative == key) {
						ControllerInputs[index].Positive = ControllerInputs[input].Negative;
						break;
					} else if (ControllerInputs[index].Negative == key) {
						ControllerInputs[index].AltNegative = ControllerInputs[input].Negative;
						break;
					} else {
						ControllerInputs[index].AltPositive = ControllerInputs[input].Negative;
						break;
					}
				}

				ControllerInputs[input].Negative = key;
				break;
			case 1:
				for (int index = 0; index < ControllerInputs.Length; index++) {
					if(ControllerInputs[index].Negative == key) {
						ControllerInputs[index].Negative = ControllerInputs[input].Positive;
						break;
					} else if (ControllerInputs[index].Negative == key) {
						ControllerInputs[index].Positive = ControllerInputs[input].Positive;
						break;
					} else if (ControllerInputs[index].Negative == key) {
						ControllerInputs[index].AltNegative = ControllerInputs[input].Positive;
						break;
					} else {
						ControllerInputs[index].AltPositive = ControllerInputs[input].Positive;
						break;
					}
				}

				ControllerInputs[input].Positive = key;
				break;
			case 2:
				for (int index = 0; index < ControllerInputs.Length; index++) {
					if(ControllerInputs[index].Negative == key) {
						ControllerInputs[index].Negative = ControllerInputs[input].AltNegative;
						break;
					} else if (ControllerInputs[index].Negative == key) {
						ControllerInputs[index].Positive = ControllerInputs[input].AltNegative;
						break;
					} else if (ControllerInputs[index].Negative == key) {
						ControllerInputs[index].AltNegative = ControllerInputs[input].AltNegative;
						break;
					} else {
						ControllerInputs[index].AltPositive = ControllerInputs[input].AltNegative;
						break;
					}
				}

				ControllerInputs[input].AltNegative = key;
				break;
			case 3:
				for (int index = 0; index < ControllerInputs.Length; index++) {
					if(ControllerInputs[index].Negative == key) {
						ControllerInputs[index].Negative = ControllerInputs[input].AltPositive;
						break;
					} else if (ControllerInputs[index].Negative == key) {
						ControllerInputs[index].Positive = ControllerInputs[input].AltPositive;
						break;
					} else if (ControllerInputs[index].Negative == key) {
						ControllerInputs[index].AltNegative = ControllerInputs[input].AltPositive;
						break;
					} else {
						ControllerInputs[index].AltPositive = ControllerInputs[input].AltPositive;
						break;
					}
				}

				ControllerInputs[input].AltPositive = key;
				break;
			}
		}
	}

	public bool UsingController { get { return usingController; } }
	public InputType[] keyboardInputs { get { return KeyboardInputs; } }
	public InputType[] controllerInputs { get { return ControllerInputs; } }
}

[System.Serializable]
public struct InputType {
	public enum Axes { AxisX, AxisY, Axis3, Axis4, Axis5, Axis6, Axis7, Axis8, Axis9, Axis10, NonAxis };

	public string Name;
	public Axes axis;
	public bool axisDown;
	public KeyCode Negative;
	public KeyCode Positive;
	public KeyCode AltNegative;
	public KeyCode AltPositive;
}