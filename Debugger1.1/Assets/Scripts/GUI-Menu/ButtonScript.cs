using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class ButtonScript : MonoBehaviour {

	Text text = null;

	bool clicked = false;
	bool textSet = true;

	[SerializeField]
	KeyBindingButtonManager manager = null;

	[SerializeField]
	bool controller = false;
	[SerializeField]
	int input = 0;
	[SerializeField]
	int selection = 0;

	// Use this for initialization
	void Start () {
		text = gameObject.GetComponentInChildren<Text> ();
		UpdateKey ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!textSet) {
			text.text = " ";
			textSet = true;
		}
	}

	public void UpdateKey () {
		KeyCode key;
		int axis = -1;
		InputType.AxisDirection axisDir = InputType.AxisDirection.negative;

		if (!controller)
			key = InputManager.instance.GetKey (input, selection);
		else {
			if(!InputManager.instance.GetControllerButton(input, selection, out key)) {
				InputManager.instance.GetControllerAxis(input, selection, out axis, out axisDir);
			}
		}
		
		if (!controller) {
			if (key > 0) {
				text.text = GetKeyName (false, 0, axisDir, key);
			} else {
				text.text = "None";
			}
		} else {
			if (key > 0) {
				text.text = GetKeyName (false, axis, axisDir, key);
			} else if (axis != (int)InputType.Axes.NonAxis) {
				text.text = GetKeyName (true, axis, axisDir, key);
			} else {
				text.text = "None";
			}
		}
	}

	public void OnClick () {
		clicked = true;
		textSet = false;
	}

	void OnGUI() {
		if (clicked) {
			if (Event.current.isKey)
			{
				InputManager.instance.ChangeKey(input, selection, Event.current.keyCode);
				text.text = Convert.ToString(Event.current.keyCode);
				manager.ButtonUpdated();
				clicked = false;
			} else if (Event.current.isMouse) {
				switch((int)Event.current.button){
				case 0:
					InputManager.instance.ChangeKey(input, selection, KeyCode.Mouse0);
					text.text = "LMouse Button";
					break;
				case 1:
					InputManager.instance.ChangeKey(input, selection, KeyCode.Mouse1);
					text.text = "RMouse Button";
					break;
				case 2:
					InputManager.instance.ChangeKey(input, selection, KeyCode.Mouse2);
					text.text = "MMouse Button";
					break;
				}

				manager.ButtonUpdated();
				clicked = false;
			}

			if(Input.GetKey (KeyCode.JoystickButton0)) {
				InputManager.instance.ChangeButtonOrAxis(input, selection, KeyCode.JoystickButton0, InputType.Axes.NonAxis, InputType.AxisDirection.negative);
				manager.ButtonUpdated();
				clicked = false;
			} else if (Input.GetKey (KeyCode.JoystickButton1)) {
				InputManager.instance.ChangeButtonOrAxis(input, selection, KeyCode.JoystickButton1, InputType.Axes.NonAxis, InputType.AxisDirection.negative);
				manager.ButtonUpdated();
				clicked = false;
			} else if (Input.GetKey (KeyCode.JoystickButton2)) {
				InputManager.instance.ChangeButtonOrAxis(input, selection, KeyCode.JoystickButton2, InputType.Axes.NonAxis, InputType.AxisDirection.negative);
				manager.ButtonUpdated();
				clicked = false;
			} else if (Input.GetKey (KeyCode.JoystickButton3)) {
				InputManager.instance.ChangeButtonOrAxis(input, selection, KeyCode.JoystickButton3, InputType.Axes.NonAxis, InputType.AxisDirection.negative);
				manager.ButtonUpdated();
				clicked = false;
			} else if (Input.GetKey (KeyCode.JoystickButton4)) {
				InputManager.instance.ChangeButtonOrAxis(input, selection, KeyCode.JoystickButton4, InputType.Axes.NonAxis, InputType.AxisDirection.negative);
				manager.ButtonUpdated();
				clicked = false;
			} else if (Input.GetKey (KeyCode.JoystickButton5)) {
				InputManager.instance.ChangeButtonOrAxis(input, selection, KeyCode.JoystickButton5, InputType.Axes.NonAxis, InputType.AxisDirection.negative);
				manager.ButtonUpdated();
				clicked = false;
			} else if (Input.GetKey (KeyCode.JoystickButton6)) {
				InputManager.instance.ChangeButtonOrAxis(input, selection, KeyCode.JoystickButton6, InputType.Axes.NonAxis, InputType.AxisDirection.negative);
				manager.ButtonUpdated();
				clicked = false;
			} else if (Input.GetKey (KeyCode.JoystickButton7)) {
				InputManager.instance.ChangeButtonOrAxis(input, selection, KeyCode.JoystickButton7, InputType.Axes.NonAxis, InputType.AxisDirection.negative);
				manager.ButtonUpdated();
				clicked = false;
			} else if (Input.GetKey (KeyCode.JoystickButton8)) {
				InputManager.instance.ChangeButtonOrAxis(input, selection, KeyCode.JoystickButton8, InputType.Axes.NonAxis, InputType.AxisDirection.negative);
				manager.ButtonUpdated();
				clicked = false;
			} else if (Input.GetKey (KeyCode.JoystickButton9)) {
				InputManager.instance.ChangeButtonOrAxis(input, selection, KeyCode.JoystickButton9, InputType.Axes.NonAxis, InputType.AxisDirection.negative);
				manager.ButtonUpdated();
				clicked = false;
			} else {
				float axis = 0.0f;

				if ((axis = Input.GetAxis ("AxisX")) != 0.0f) {
					if(axis > 0.0f) {
						InputManager.instance.ChangeButtonOrAxis(input, selection, KeyCode.None, InputType.Axes.AxisX, InputType.AxisDirection.positive);
					} else {
						InputManager.instance.ChangeButtonOrAxis(input, selection, KeyCode.None, InputType.Axes.AxisX, InputType.AxisDirection.negative);
					}

					manager.ButtonUpdated();
					clicked = false;
				} else if ((axis = Input.GetAxis ("AxisY")) != 0.0f) {
					if(axis > 0.0f) {
						InputManager.instance.ChangeButtonOrAxis(input, selection, KeyCode.None, InputType.Axes.AxisY, InputType.AxisDirection.positive);
					} else {
						InputManager.instance.ChangeButtonOrAxis(input, selection, KeyCode.None, InputType.Axes.AxisY, InputType.AxisDirection.negative);
					}
					
					manager.ButtonUpdated();
					clicked = false;
				} else if ((axis = Input.GetAxis ("Axis3")) != 0.0f) {
					if(axis > 0.0f) {
						InputManager.instance.ChangeButtonOrAxis(input, selection, KeyCode.None, InputType.Axes.Axis3, InputType.AxisDirection.positive);
					} else {
						InputManager.instance.ChangeButtonOrAxis(input, selection, KeyCode.None, InputType.Axes.Axis3, InputType.AxisDirection.negative);
					}
					
					manager.ButtonUpdated();
					clicked = false;
				} else if ((axis = Input.GetAxis ("Axis4")) != 0.0f) {
					if(axis > 0.0f) {
						InputManager.instance.ChangeButtonOrAxis(input, selection, KeyCode.None, InputType.Axes.Axis4, InputType.AxisDirection.positive);
					} else {
						InputManager.instance.ChangeButtonOrAxis(input, selection, KeyCode.None, InputType.Axes.Axis4, InputType.AxisDirection.negative);
					}
					
					manager.ButtonUpdated();
					clicked = false;
				} else if ((axis = Input.GetAxis ("Axis5")) != 0.0f) {
					if(axis > 0.0f) {
						InputManager.instance.ChangeButtonOrAxis(input, selection, KeyCode.None, InputType.Axes.Axis5, InputType.AxisDirection.positive);
					} else {
						InputManager.instance.ChangeButtonOrAxis(input, selection, KeyCode.None, InputType.Axes.Axis5, InputType.AxisDirection.negative);
					}
					
					manager.ButtonUpdated();
					clicked = false;
				} else if ((axis = Input.GetAxis ("Axis6")) != 0.0f) {
					if(axis > 0.0f) {
						InputManager.instance.ChangeButtonOrAxis(input, selection, KeyCode.None, InputType.Axes.Axis6, InputType.AxisDirection.positive);
					} else {
						InputManager.instance.ChangeButtonOrAxis(input, selection, KeyCode.None, InputType.Axes.Axis6, InputType.AxisDirection.negative);
					}
					
					manager.ButtonUpdated();
					clicked = false;
				} else if ((axis = Input.GetAxis ("Axis7")) != 0.0f) {
					if(axis > 0.0f) {
						InputManager.instance.ChangeButtonOrAxis(input, selection, KeyCode.None, InputType.Axes.Axis7, InputType.AxisDirection.positive);
					} else {
						InputManager.instance.ChangeButtonOrAxis(input, selection, KeyCode.None, InputType.Axes.Axis7, InputType.AxisDirection.negative);
					}
					
					manager.ButtonUpdated();
					clicked = false;
				} else if ((axis = Input.GetAxis ("Axis8")) != 0.0f) {
					if(axis > 0.0f) {
						InputManager.instance.ChangeButtonOrAxis(input, selection, KeyCode.None, InputType.Axes.Axis8, InputType.AxisDirection.positive);
					} else {
						InputManager.instance.ChangeButtonOrAxis(input, selection, KeyCode.None, InputType.Axes.Axis8, InputType.AxisDirection.negative);
					}
					
					manager.ButtonUpdated();
					clicked = false;
				} else if ((axis = Input.GetAxis ("Axis9")) != 0.0f) {
					if(axis > 0.0f) {
						InputManager.instance.ChangeButtonOrAxis(input, selection, KeyCode.None, InputType.Axes.Axis9, InputType.AxisDirection.positive);
					} else {
						InputManager.instance.ChangeButtonOrAxis(input, selection, KeyCode.None, InputType.Axes.Axis9, InputType.AxisDirection.negative);
					}
					
					manager.ButtonUpdated();
					clicked = false;
				} else if ((axis = Input.GetAxis ("Axis10")) != 0.0f) {
					if(axis > 0.0f) {
						InputManager.instance.ChangeButtonOrAxis(input, selection, KeyCode.None, InputType.Axes.Axis10, InputType.AxisDirection.positive);
					} else {
						InputManager.instance.ChangeButtonOrAxis(input, selection, KeyCode.None, InputType.Axes.Axis10, InputType.AxisDirection.negative);
					}
					
					manager.ButtonUpdated();
					clicked = false;
				}
			}
		}
	}

	string GetKeyName(bool controllerAxis, int axisNum, InputType.AxisDirection direction, KeyCode key) {
		if (controllerAxis) {
			string AxisName = "";

			switch (axisNum) {
			case 0:
				AxisName = "Left Stick ";
				if(direction == InputType.AxisDirection.negative) {
					AxisName += "Left";
				} else {
					AxisName += "Right";
				}
				return AxisName;
			case 1:
				AxisName = "Left Stick ";
				if(direction == InputType.AxisDirection.negative) {
					AxisName += "Down";
				} else {
					AxisName += "Up";
				}
				return AxisName;
			case 2:
				AxisName = "Triggers ";
				if(direction == InputType.AxisDirection.negative) {
					AxisName += "Left";
				} else {
					AxisName += "Right";
				}
				return AxisName;
			case 3:
				AxisName = "Right Stick ";
				if(direction == InputType.AxisDirection.negative) {
					AxisName += "Left";
				} else {
					AxisName += "Right";
				}
				return AxisName;
			case 4:
				AxisName = "Right Stick ";
				if(direction == InputType.AxisDirection.negative) {
					AxisName += "Down";
				} else {
					AxisName += "Up";
				}
				return AxisName;
			case 5:
				AxisName = "DPad ";
				if(direction == InputType.AxisDirection.negative) {
					AxisName += "Left";
				} else {
					AxisName += "Right";
				}
				return AxisName;
			case 6:
				AxisName = "DPad ";
				if(direction == InputType.AxisDirection.negative) {
					AxisName += "Up";
				} else {
					AxisName += "Down";
				}
				return AxisName;
			case 7:
				AxisName = "Left Stick ";
				if(direction == InputType.AxisDirection.negative) {
					AxisName += '-';
				} else {
					AxisName += '+';
				}
				return AxisName;
			case 8:
				AxisName = "Left Trigger";
				return AxisName;
			case 9:
				AxisName = "Right Trigger";
				return AxisName;
			default:
				return "None";
			}
		} else {
			switch ((int)key) {
			case (int)KeyCode.Mouse0:
				return "LMouse Button";
			case (int)KeyCode.Mouse1:
				return "RMouse Button";
			case (int)KeyCode.Mouse2:
				return "MMouse Button";
			case (int)KeyCode.JoystickButton0:
				return "Joystick A";
			case (int)KeyCode.JoystickButton1:
				return "Joystick B";
			case (int)KeyCode.JoystickButton2:
				return "Joystick X";
			case (int)KeyCode.JoystickButton3:
				return "Joystick Y";
			case (int)KeyCode.JoystickButton4:
				return "Joystick LB";
			case (int)KeyCode.JoystickButton5:
				return "Joystick RB";
			case (int)KeyCode.JoystickButton6:
				return "Joystick Back";
			case (int)KeyCode.JoystickButton7:
				return "Joystick Start";
			case (int)KeyCode.JoystickButton8:
				return "Joystick LS In";
			case (int)KeyCode.JoystickButton9:
				return "Joystick RS In";
			default:
				return Convert.ToString(key);
			}
		}
	}
}
