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
			ControllerDefaults[input].axis = new InputType.Axes[4];
			ControllerDefaults[input].direction = new InputType.AxisDirection[4];

			ControllerDefaults[input].Positive = ControllerInputs[input].Positive;
			ControllerDefaults[input].Negative = ControllerInputs[input].Negative;
			ControllerDefaults[input].AltNegative = ControllerInputs[input].AltNegative;
			ControllerDefaults[input].AltPositive = ControllerInputs[input].AltPositive;

			for (int index = 0; index < 4; index++) {
				ControllerDefaults[input].axis[index] = ControllerInputs[input].axis[index];
				ControllerDefaults[input].direction[index] = ControllerInputs[input].direction[index];
			}
		}

		LoadInputs ();
	}

	void Update() {
		for (int index = 0; index < ControllerInputs.Length; index++) {
			for (int axis = 0; axis < 4; axis++) {
				if (ControllerInputs[index].axis[axis] == InputType.Axes.NonAxis)
					continue;
				if (ControllerInputs[index].axisDown[axis] == true) {
					if (GetAxisFromInput(ControllerInputs[index], axis) == 0.0f)
						ControllerInputs[index].axisDown[axis] = false;
				} else {
					if (GetAxisFromInput(ControllerInputs[index], axis) != 0.0f)
						ControllerInputs[index].axisDown[axis] = true;
				}
			}
		}
	}

	public void SaveInputs () {
		string KeyboardButtons = "";
		string ControllerButtons = "";

		for (int key = KeyboardInputs.Length - 1; key >= 0; key--) {
			KeyboardButtons = KeyboardInputs[key].Name + '*' + KeyboardInputs[key].Negative + '*' + KeyboardInputs[key].Positive +
				'*' + KeyboardInputs[key].AltNegative + '*' + KeyboardInputs[key].AltPositive + '*' + KeyboardButtons;
		}

		for (int key = ControllerInputs.Length - 1; key >= 0; key--) {
			ControllerButtons = ControllerInputs[key].Name + '*' + ControllerInputs[key].Negative + '*' + ControllerInputs[key].Positive +
				'*' + ControllerInputs[key].AltNegative + '*' + ControllerInputs[key].AltPositive + '*' + ControllerInputs[key].axis[0] +
				'*' + ControllerInputs[key].axis[1] + '*' + ControllerInputs[key].axis[2] + '*' + ControllerInputs[key].axis[3] + '*' +
				ControllerInputs[key].direction[0] + '*' + ControllerInputs[key].direction[1] + '*' + ControllerInputs[key].direction[2] +
				'*' + ControllerInputs[key].direction[3] + '*' + ControllerButtons;
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
				KeyboardInputs[key].Negative = (KeyCode)System.Enum.Parse(typeof(KeyCode), Inputs[key * 5 + 1]);
				KeyboardInputs[key].Positive = (KeyCode)System.Enum.Parse(typeof(KeyCode), Inputs[key * 5 + 2]);
				KeyboardInputs[key].AltNegative = (KeyCode)System.Enum.Parse(typeof(KeyCode), Inputs[key * 5 + 3]);
				KeyboardInputs[key].AltPositive = (KeyCode)System.Enum.Parse(typeof(KeyCode), Inputs[key * 5 + 4]);
			}
		}

		if (PlayerPrefs.HasKey ("ControllerKeys")) {
			string ControllerButtons = PlayerPrefs.GetString("ControllerKeys");
			string[] Inputs = ControllerButtons.Split('*');
			for (int key = 0; key < ControllerInputs.Length; key++) {
				ControllerInputs[key].Name = Inputs[key * 13];
				ControllerInputs[key].Negative = (KeyCode)System.Enum.Parse(typeof(KeyCode),Inputs[key * 13 + 1]);
				ControllerInputs[key].Positive = (KeyCode)System.Enum.Parse(typeof(KeyCode),Inputs[key * 13 + 2]);
				ControllerInputs[key].AltNegative = (KeyCode)System.Enum.Parse(typeof(KeyCode),Inputs[key * 13 + 3]);
				ControllerInputs[key].AltPositive = (KeyCode)System.Enum.Parse(typeof(KeyCode),Inputs[key * 13 + 4]);
				ControllerInputs[key].axis[0] = (InputType.Axes)System.Enum.Parse(typeof(InputType.Axes),Inputs[key * 13 + 5]);
				ControllerInputs[key].axis[1] = (InputType.Axes)System.Enum.Parse(typeof(InputType.Axes),Inputs[key * 13 + 6]);
				ControllerInputs[key].axis[2] = (InputType.Axes)System.Enum.Parse(typeof(InputType.Axes),Inputs[key * 13 + 7]);
				ControllerInputs[key].axis[3] = (InputType.Axes)System.Enum.Parse(typeof(InputType.Axes),Inputs[key * 13 + 8]);
				ControllerInputs[key].direction[0] = (InputType.AxisDirection)System.Enum.Parse(typeof(InputType.AxisDirection),Inputs[key * 13 + 9]);
				ControllerInputs[key].direction[1] = (InputType.AxisDirection)System.Enum.Parse(typeof(InputType.AxisDirection),Inputs[key * 13 + 10]);
				ControllerInputs[key].direction[2] = (InputType.AxisDirection)System.Enum.Parse(typeof(InputType.AxisDirection),Inputs[key * 13 + 11]);
				ControllerInputs[key].direction[3] = (InputType.AxisDirection)System.Enum.Parse(typeof(InputType.AxisDirection),Inputs[key * 13 + 12]);
			}
		}
	}

	public void SetDefaults (bool controller) {
		if (!controller) {
			for (int input = 0; input < KeyboardInputs.Length; input++) {
				KeyboardInputs [input] = KeyboardDefaults [input];
			}
		} else {
			for (int input = 0; input < ControllerInputs.Length; input++) {
				ControllerInputs [input].Negative = ControllerDefaults [input].Negative;
				ControllerInputs [input].Positive = ControllerDefaults [input].Positive;
				ControllerInputs [input].AltNegative = ControllerDefaults [input].AltNegative;
				ControllerInputs [input].AltPositive = ControllerDefaults [input].AltPositive;

				for (int index = 0; index < 4; index++) {
					ControllerInputs [input].axis[index] = ControllerDefaults [input].axis[index];
					ControllerInputs [input].direction[index] = ControllerDefaults [input].direction[index];
				}
			}
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
				if (Input.GetKeyDown(ControllerInputs[keys].Negative) || Input.GetKeyDown(ControllerInputs[keys].Positive) ||
				    Input.GetKeyDown(ControllerInputs[keys].AltNegative) || Input.GetKeyDown(ControllerInputs[keys].AltPositive)){
					if(!usingController)
						usingController = true;
					
					return true;
				}

				for (int index = 0; index < 4; index++) {
					if (ControllerInputs[keys].axis[index] != InputType.Axes.NonAxis && ControllerInputs[keys].axisDown[index] == false) {
						if (ControllerInputs[keys].direction[index] == InputType.AxisDirection.negative && GetAxisFromInput(ControllerInputs[keys], index) < 0.0f ||
						    ControllerInputs[keys].direction[index] == InputType.AxisDirection.positive && GetAxisFromInput(ControllerInputs[keys], index) > 0.0f) {
							
							if(!usingController)
								usingController = true;
							
							ControllerInputs[keys].axisDown[index] = true;
							return true;
						}
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
				if (Input.GetKey(ControllerInputs[keys].Negative) || Input.GetKey(ControllerInputs[keys].Positive) ||
				    Input.GetKey(ControllerInputs[keys].AltNegative) || Input.GetKey(ControllerInputs[keys].AltPositive)){
					if(!usingController)
						usingController = true;
					
					return true;
				}

				for (int index = 0; index < 4; index++) {
					if (ControllerInputs[keys].axis[index] != InputType.Axes.NonAxis) {
						if (ControllerInputs[keys].direction[index] == InputType.AxisDirection.negative && GetAxisFromInput(ControllerInputs[keys], index) < 0.0f ||
						    ControllerInputs[keys].direction[index] == InputType.AxisDirection.positive && GetAxisFromInput(ControllerInputs[keys], index) > 0.0f) {

							if(!usingController)
								usingController = true;
	
							ControllerInputs[keys].axisDown[index] = true;
							return true;
						}
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
				if (Input.GetKeyUp(ControllerInputs[keys].Negative) || Input.GetKeyUp(ControllerInputs[keys].Positive) ||
				    Input.GetKeyUp(ControllerInputs[keys].AltNegative) || Input.GetKeyUp(ControllerInputs[keys].AltPositive)){
					if(!usingController)
						usingController = true;
					
					return true;
				}
				
				for (int index = 0; index < 4; index++) {
					if (ControllerInputs[keys].axis[index] != InputType.Axes.NonAxis && ControllerInputs[keys].axisDown[index] == true) {
						if (ControllerInputs[keys].direction[index] == InputType.AxisDirection.negative && GetAxisFromInput(ControllerInputs[keys], index) == 0.0f ||
						    ControllerInputs[keys].direction[index] == InputType.AxisDirection.positive && GetAxisFromInput(ControllerInputs[keys], index) == 0.0f) {
							
							if(!usingController)
								usingController = true;
							
							ControllerInputs[keys].axisDown[index] = false;
							return true;
						}
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

				CAxis = 1 * (Mathf.Ceil (Convert.ToInt32 (Input.GetKey (ControllerInputs [keys].Positive)) * 0.5f + Convert.ToInt32 (Input.GetKey (ControllerInputs [keys].AltPositive)) * 0.5f)) -
						1 * (Mathf.Ceil (Convert.ToInt32 (Input.GetKey (ControllerInputs [keys].Negative)) * 0.5f + Convert.ToInt32 (Input.GetKey (ControllerInputs [keys].AltNegative)) * 0.5f));

				for (int index = 0; index < 4; index++) {
					CAxis += GetAxisFromInput(ControllerInputs[keys], index);
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

	public float GetAxisFromInput (InputType button, int axis) {
		switch ((int)button.axis[axis]) {
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

	public void ChangeKey (int input, int selection, KeyCode key) {
		switch(selection) {
		case 0:
			for (int index = 0; index < KeyboardInputs.Length; index++) {
				if(KeyboardInputs[index].Negative == key) {
					KeyboardInputs[index].Negative = KeyboardInputs[input].Negative;
					break;
				} else if (KeyboardInputs[index].Positive == key) {
					KeyboardInputs[index].Positive = KeyboardInputs[input].Negative;
					break;
				} else if (KeyboardInputs[index].AltNegative == key) {
					KeyboardInputs[index].AltNegative = KeyboardInputs[input].Negative;
					break;
				} else if (KeyboardInputs[index].AltPositive == key) {
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
				} else if (KeyboardInputs[index].Positive == key) {
					KeyboardInputs[index].Positive = KeyboardInputs[input].Positive;
					break;
				} else if (KeyboardInputs[index].AltNegative == key) {
					KeyboardInputs[index].AltNegative = KeyboardInputs[input].Positive;
					break;
				} else if (KeyboardInputs[index].AltPositive == key) {
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
				} else if (KeyboardInputs[index].Positive == key) {
					KeyboardInputs[index].Positive = KeyboardInputs[input].AltNegative;
					break;
				} else if (KeyboardInputs[index].AltNegative == key) {
					KeyboardInputs[index].AltNegative = KeyboardInputs[input].AltNegative;
					break;
				} else if (KeyboardInputs[index].AltPositive == key) {
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
				} else if (KeyboardInputs[index].Positive == key) {
					KeyboardInputs[index].Positive = KeyboardInputs[input].AltPositive;
					break;
				} else if (KeyboardInputs[index].AltNegative == key) {
					KeyboardInputs[index].AltNegative = KeyboardInputs[input].AltPositive;
					break;
				} else if (KeyboardInputs[index].AltPositive == key) {
					KeyboardInputs[index].AltPositive = KeyboardInputs[input].AltPositive;
					break;
				}
			}

			KeyboardInputs[input].AltPositive = key;
			break;
		}
	}

	public void ChangeButtonOrAxis (int input, int selection, KeyCode key, InputType.Axes axis, InputType.AxisDirection axisDir) {
		if (axis == InputType.Axes.NonAxis) {
			switch (selection) {
			case 0:
				for (int index = 0; index < ControllerInputs.Length; index++) {
					if (ControllerInputs [index].Negative == key) {
						ControllerInputs [index].Negative = ControllerInputs [input].Negative;
						ControllerInputs [index].axis[0] = ControllerInputs [input].axis[0];
						ControllerInputs [index].direction[0] = ControllerInputs [input].direction[0];
						break;
					} else if (ControllerInputs [index].Positive == key) {
						ControllerInputs [index].Positive = ControllerInputs [input].Negative;
						ControllerInputs [index].axis[1] = ControllerInputs [input].axis[0];
						ControllerInputs [index].direction[1] = ControllerInputs [input].direction[0];
						break;
					} else if (ControllerInputs [index].AltNegative == key) {
						ControllerInputs [index].AltNegative = ControllerInputs [input].Negative;
						ControllerInputs [index].axis[2] = ControllerInputs [input].axis[0];
						ControllerInputs [index].direction[2] = ControllerInputs [input].direction[0];
						break;
					} else if (ControllerInputs [index].AltPositive == key) {
						ControllerInputs [index].AltPositive = ControllerInputs [input].Negative;
						ControllerInputs [index].axis[3] = ControllerInputs [input].axis[0];
						ControllerInputs [index].direction[3] = ControllerInputs [input].direction[0];
						break;
					}
				}

				ControllerInputs [input].axis[0] = InputType.Axes.NonAxis;
			
				ControllerInputs [input].Negative = key;
				break;
			case 1:
				for (int index = 0; index < ControllerInputs.Length; index++) {
					if (ControllerInputs [index].Negative == key) {
						ControllerInputs [index].Negative = ControllerInputs [input].Positive;
						ControllerInputs [index].axis[0] = ControllerInputs [input].axis[1];
						ControllerInputs [index].direction[0] = ControllerInputs [input].direction[1];
						break;
					} else if (ControllerInputs [index].Positive == key) {
						ControllerInputs [index].Positive = ControllerInputs [input].Positive;
						ControllerInputs [index].axis[1] = ControllerInputs [input].axis[1];
						ControllerInputs [index].direction[1] = ControllerInputs [input].direction[1];
						break;
					} else if (ControllerInputs [index].AltNegative == key) {
						ControllerInputs [index].AltNegative = ControllerInputs [input].Positive;
						ControllerInputs [index].axis[2] = ControllerInputs [input].axis[1];
						ControllerInputs [index].direction[2] = ControllerInputs [input].direction[1];
						break;
					} else if (ControllerInputs [index].AltPositive == key) {
						ControllerInputs [index].AltPositive = ControllerInputs [input].Positive;
						ControllerInputs [index].axis[3] = ControllerInputs [input].axis[1];
						ControllerInputs [index].direction[3] = ControllerInputs [input].direction[1];
						break;
					}
				}

				ControllerInputs [input].axis[1] = InputType.Axes.NonAxis;
			
				ControllerInputs [input].Positive = key;
				break;
			case 2:
				for (int index = 0; index < ControllerInputs.Length; index++) {
					if (ControllerInputs [index].Negative == key) {
						ControllerInputs [index].Negative = ControllerInputs [input].AltNegative;
						ControllerInputs [index].axis[0] = ControllerInputs [input].axis[2];
						ControllerInputs [index].direction[0] = ControllerInputs [input].direction[2];
						break;
					} else if (ControllerInputs [index].Positive == key) {
						ControllerInputs [index].Positive = ControllerInputs [input].AltNegative;
						ControllerInputs [index].axis[1] = ControllerInputs [input].axis[2];
						ControllerInputs [index].direction[1] = ControllerInputs [input].direction[2];
						break;
					} else if (ControllerInputs [index].AltNegative == key) {
						ControllerInputs [index].AltNegative = ControllerInputs [input].AltNegative;
						ControllerInputs [index].axis[2] = ControllerInputs [input].axis[2];
						ControllerInputs [index].direction[2] = ControllerInputs [input].direction[2];
						break;
					} else if (ControllerInputs [index].AltPositive == key) {
						ControllerInputs [index].AltPositive = ControllerInputs [input].AltNegative;
						ControllerInputs [index].axis[3] = ControllerInputs [input].axis[2];
						ControllerInputs [index].direction[3] = ControllerInputs [input].direction[2];
						break;
					}
				}
				ControllerInputs [input].axis[2] = InputType.Axes.NonAxis;
			
				ControllerInputs [input].AltNegative = key;
				break;
			case 3:
				for (int index = 0; index < ControllerInputs.Length; index++) {
					if (ControllerInputs [index].Negative == key) {
						ControllerInputs [index].Negative = ControllerInputs [input].AltPositive;
						ControllerInputs [index].axis[0] = ControllerInputs [input].axis[3];
						ControllerInputs [index].direction[0] = ControllerInputs [input].direction[3];
						break;
					} else if (ControllerInputs [index].Positive == key) {
						ControllerInputs [index].Positive = ControllerInputs [input].AltPositive;
						ControllerInputs [index].axis[1] = ControllerInputs [input].axis[3];
						ControllerInputs [index].direction[1] = ControllerInputs [input].direction[3];
						break;
					} else if (ControllerInputs [index].AltNegative == key) {
						ControllerInputs [index].AltNegative = ControllerInputs [input].AltPositive;
						ControllerInputs [index].axis[2] = ControllerInputs [input].axis[3];
						ControllerInputs [index].direction[2] = ControllerInputs [input].direction[3];
						break;
					} else if (ControllerInputs [index].AltPositive == key) {
						ControllerInputs [index].AltPositive = ControllerInputs [input].AltPositive;
						ControllerInputs [index].axis[3] = ControllerInputs [input].axis[3];
						ControllerInputs [index].direction[3] = ControllerInputs [input].direction[3];
						break;
					}
				}

				ControllerInputs [input].axis[3] = InputType.Axes.NonAxis;
			
				ControllerInputs [input].AltPositive = key;
				break;
			}
		} else {
			switch (selection) {
			case 0:
				for (int index = 0; index < ControllerInputs.Length; index++) {
					if (ControllerInputs [index].axis[0] == axis && ControllerInputs [index].direction[0] == axisDir) {
						ControllerInputs [index].axis[0] = ControllerInputs [input].axis[0];
						ControllerInputs [index].direction[0] = ControllerInputs [input].direction[0];
						ControllerInputs [index].Negative = ControllerInputs [input].Negative;
						break;
					} else if (ControllerInputs [index].axis[1] == axis && ControllerInputs [index].direction[1] == axisDir) {
						ControllerInputs [index].axis[1] = ControllerInputs [input].axis[0];
						ControllerInputs [index].direction[1] = ControllerInputs [input].direction[0];
						ControllerInputs [index].Positive = ControllerInputs [input].Negative;
						break;
					} else if (ControllerInputs [index].axis[2] == axis && ControllerInputs [index].direction[2] == axisDir) {
						ControllerInputs [index].axis[2] = ControllerInputs [input].axis[0];
						ControllerInputs [index].direction[2] = ControllerInputs [input].direction[0];
						ControllerInputs [index].AltNegative = ControllerInputs [input].Negative;
						break;
					} else if (ControllerInputs [index].axis[3] == axis && ControllerInputs [index].direction[3] == axisDir) {
						ControllerInputs [index].axis[3] = ControllerInputs [input].axis[0];
						ControllerInputs [index].direction[3] = ControllerInputs [input].direction[0];
						ControllerInputs [index].AltPositive = ControllerInputs [input].Negative;
						break;
					}
				}

				ControllerInputs[input].Negative = KeyCode.None;

				
				ControllerInputs [input].axis[0] = axis;
				ControllerInputs [input].direction[0] = axisDir;
				break;
			case 1:
				for (int index = 0; index < ControllerInputs.Length; index++) {
					if (ControllerInputs [index].axis[0] == axis && ControllerInputs [index].direction[0] == axisDir) {
						ControllerInputs [index].axis[0] = ControllerInputs [input].axis[1];
						ControllerInputs [index].direction[0] = ControllerInputs [input].direction[1];
						ControllerInputs [index].Negative = ControllerInputs [input].Positive;
						break;
					} else if (ControllerInputs [index].axis[1] == axis && ControllerInputs [index].direction[1] == axisDir) {
						ControllerInputs [index].axis[1] = ControllerInputs [input].axis[1];
						ControllerInputs [index].direction[1] = ControllerInputs [input].direction[1];
						ControllerInputs [index].Positive = ControllerInputs [input].Positive;
						break;
					} else if (ControllerInputs [index].axis[2] == axis && ControllerInputs [index].direction[2] == axisDir) {
						ControllerInputs [index].axis[2] = ControllerInputs [input].axis[1];
						ControllerInputs [index].direction[2] = ControllerInputs [input].direction[1];
						ControllerInputs [index].AltNegative = ControllerInputs [input].Positive;
						break;
					} else if (ControllerInputs [index].axis[3] == axis && ControllerInputs [index].direction[3] == axisDir) {
						ControllerInputs [index].axis[3] = ControllerInputs [input].axis[1];
						ControllerInputs [index].direction[3] = ControllerInputs [input].direction[1];
						ControllerInputs [index].AltPositive = ControllerInputs [input].Positive;
						break;
					}
				}

				ControllerInputs[input].Positive = KeyCode.None;

				ControllerInputs [input].axis[1] = axis;
				ControllerInputs [input].direction[1] = axisDir;
				break;
			case 2:
				for (int index = 0; index < ControllerInputs.Length; index++) {
					if (ControllerInputs [index].axis[0] == axis && ControllerInputs [index].direction[0] == axisDir) {
						ControllerInputs [index].axis[0] = ControllerInputs [input].axis[2];
						ControllerInputs [index].direction[0] = ControllerInputs [input].direction[2];
						ControllerInputs [index].Negative = ControllerInputs [input].AltNegative;
						break;
					} else if (ControllerInputs [index].axis[1] == axis && ControllerInputs [index].direction[1] == axisDir) {
						ControllerInputs [index].axis[1] = ControllerInputs [input].axis[2];
						ControllerInputs [index].direction[1] = ControllerInputs [input].direction[2];
						ControllerInputs [index].Positive = ControllerInputs [input].AltNegative;
						break;
					} else if (ControllerInputs [index].axis[2] == axis && ControllerInputs [index].direction[2] == axisDir) {
						ControllerInputs [index].axis[2] = ControllerInputs [input].axis[2];
						ControllerInputs [index].direction[2] = ControllerInputs [input].direction[2];
						ControllerInputs [index].AltNegative = ControllerInputs [input].AltNegative;
						break;
					} else if (ControllerInputs [index].axis[3] == axis && ControllerInputs [index].direction[3] == axisDir) {
						ControllerInputs [index].axis[3] = ControllerInputs [input].axis[2];
						ControllerInputs [index].direction[3] = ControllerInputs [input].direction[2];
						ControllerInputs [index].AltPositive = ControllerInputs [input].AltNegative;
						break;
					}
				}
				ControllerInputs[input].AltNegative = KeyCode.None;
				
				ControllerInputs [input].axis[2] = axis;
				ControllerInputs [input].direction[2] = axisDir;
				break;
			case 3:
				for (int index = 0; index < ControllerInputs.Length; index++) {
					if (ControllerInputs [index].axis[0] == axis && ControllerInputs [index].direction[0] == axisDir) {
						ControllerInputs [index].axis[0] = ControllerInputs [input].axis[3];
						ControllerInputs [index].direction[0] = ControllerInputs [input].direction[3];
						ControllerInputs [index].Negative = ControllerInputs [input].AltPositive;
						break;
					} else if (ControllerInputs [index].axis[1] == axis && ControllerInputs [index].direction[1] == axisDir) {
						ControllerInputs [index].axis[1] = ControllerInputs [input].axis[3];
						ControllerInputs [index].direction[1] = ControllerInputs [input].direction[3];
						ControllerInputs [index].Positive = ControllerInputs [input].AltPositive;
						break;
					} else if (ControllerInputs [index].axis[2] == axis && ControllerInputs [index].direction[2] == axisDir) {
						ControllerInputs [index].axis[2] = ControllerInputs [input].axis[3];
						ControllerInputs [index].direction[2] = ControllerInputs [input].direction[3];
						ControllerInputs [index].AltNegative = ControllerInputs [input].AltPositive;
						break;
					} else if (ControllerInputs [index].axis[3] == axis && ControllerInputs [index].direction[3] == axisDir) {
						ControllerInputs [index].axis[3] = ControllerInputs [input].axis[3];
						ControllerInputs [index].direction[3] = ControllerInputs [input].direction[3];
						ControllerInputs [index].AltPositive = ControllerInputs [input].AltPositive;
						break;
					}
				}

				ControllerInputs[input].AltPositive = KeyCode.None;
				
				ControllerInputs [input].axis[3] = axis;
				ControllerInputs [input].direction[3] = axisDir;
				break;
			}
		}
	}

	public KeyCode GetKey (int input, int selection) {
		switch (selection) {
		case 0:
			return KeyboardInputs [input].Negative;
		case 1:
			return KeyboardInputs [input].Positive;
		case 2:
			return KeyboardInputs [input].AltNegative;
		case 3:
			return KeyboardInputs [input].AltPositive;
		}

		return (KeyCode)0;
	}

	public bool GetControllerButton (int input, int selection, out KeyCode code) {
		switch (selection) {
		case 0:
			code = ControllerInputs [input].Negative;
			break;
		case 1:
			code = ControllerInputs [input].Positive;
			break;
		case 2:
			code = ControllerInputs [input].AltNegative;
			break;
		case 3:
			code = ControllerInputs [input].AltPositive;
			break;
		default:
			code = KeyCode.None;
			break;
		}

		if (code == KeyCode.None)
			return false;
		else
			return true;
	}

	public bool GetControllerAxis (int input, int selection, out int Axis, out InputType.AxisDirection axisDir) {
		switch (selection) {
		case 0:
			Axis = (int)ControllerInputs [input].axis[selection];
			axisDir = ControllerInputs [input].direction[selection];
			break;
		case 1:
			Axis = (int)ControllerInputs [input].axis[selection];
			axisDir = ControllerInputs [input].direction[selection];
			break;
		case 2:
			Axis = (int)ControllerInputs [input].axis[selection];
			axisDir = ControllerInputs [input].direction[selection];
			break;
		case 3:
			Axis = (int)ControllerInputs [input].axis[selection];
			axisDir = ControllerInputs [input].direction[selection];
			break;
		default:
			Axis = -1;
			axisDir = InputType.AxisDirection.negative;
			break;
		}

		return false;
	}

	public bool UsingController { get { return usingController; } }
	public InputType[] keyboardInputs { get { return KeyboardInputs; } }
	public InputType[] controllerInputs { get { return ControllerInputs; } }
}

[System.Serializable]
public struct InputType {
	public enum Axes { AxisX, AxisY, Axis3, Axis4, Axis5, Axis6, Axis7, Axis8, Axis9, Axis10, NonAxis };
	public enum AxisDirection { negative, positive };
	public enum DPad { DPadUp, DPadDown, DPadLeft, DPadRight, NonDPad };

	public string Name;
	public Axes[] axis;
	public AxisDirection[] direction;
	public bool[] axisDown;
	public KeyCode Negative;
	public KeyCode Positive;
	public KeyCode AltNegative;
	public KeyCode AltPositive;
}