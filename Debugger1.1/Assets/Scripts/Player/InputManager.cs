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
				if (Input.GetKeyDown(ControllerInputs[keys].Negative) || Input.GetKeyDown(ControllerInputs[keys].Positive) ||
				    Input.GetKeyDown(ControllerInputs[keys].AltNegative) || Input.GetKeyDown(ControllerInputs[keys].AltPositive)){
					if(!usingController)
						usingController = true;

					return true;
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

		if (button != "Horizontal" && button != "Vertical" && button != "Horizontal2" && button != "Vertical2") {
			for (int keys = 0; keys < ControllerInputs.Length; keys++) {
				if (button == ControllerInputs [keys].Name) {
					Axis = Axis + 1 * (Mathf.Ceil (Convert.ToInt32 (Input.GetKey (ControllerInputs [keys].Positive)) * 0.5f + Convert.ToInt32 (Input.GetKey (ControllerInputs [keys].AltPositive)) * 0.5f)) -
						1 * (Mathf.Ceil (Convert.ToInt32 (Input.GetKey (ControllerInputs [keys].Negative)) * 0.5f + Convert.ToInt32 (Input.GetKey (ControllerInputs [keys].AltNegative)) * 0.5f));
				
					if(!usingController)
						usingController = true;
				}
			}
		} else {
			if (button == "Horizontal") {
				if (Input.GetAxisRaw ("Horizontal") != 0.0f) {
					Axis += Input.GetAxisRaw ("Horizontal");

					if(!usingController)
						usingController = true;
				}
			} else if (button == "Vertical") {
				if (Input.GetAxisRaw ("Vertical") != 0.0f) {
					Axis += Input.GetAxisRaw ("Vertical");

					if(!usingController)
						usingController = true;
				}
			} else if (button == "Horizontal2") {
				if (Input.GetAxisRaw ("Horizontal2") != 0.0f) {
					Axis += Input.GetAxisRaw ("Horizontal2");
					
					if(!usingController)
						usingController = true;
				}
			} else {
				if (Input.GetAxisRaw ("Vertical2") != 0.0f) {
					Axis += Input.GetAxisRaw ("Vertical2");
					
					if(!usingController)
						usingController = true;
				}
			}
		}

		if (Axis > 1.0f)
			Axis = 1.0f;
		else if (Axis < -1.0f)
			Axis = -1.0f;

		return Axis;
	}

	public void ChangeKey (bool controller, int input, int selection, KeyCode key) {
		if (!controller) {
			switch(selection){
			case 0:
				KeyboardInputs[input].Negative = key;
				break;
			case 1:
				KeyboardInputs[input].Positive = key;
				break;
			case 2:
				KeyboardInputs[input].AltNegative = key;
				break;
			case 3:
				KeyboardInputs[input].AltPositive = key;
				break;
			}
		} else {
			switch(selection){
			case 0:
				ControllerInputs[input].Negative = key;
				break;
			case 1:
				ControllerInputs[input].Positive = key;
				break;
			case 2:
				ControllerInputs[input].AltNegative = key;
				break;
			case 3:
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
	public string Name;
	public KeyCode Negative;
	public KeyCode Positive;
	public KeyCode AltNegative;
	public KeyCode AltPositive;
}