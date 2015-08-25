using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class ButtonScript : MonoBehaviour {

	Text text = null;

	bool clicked = false;
	bool textSet = true;

	[SerializeField]
	bool controller = false;
	[SerializeField]
	int input = 0;
	[SerializeField]
	int selection = 0;

	// Use this for initialization
	void Start () {
		text = gameObject.GetComponentInChildren<Text> ();
		text.text = "Alt";
	}
	
	// Update is called once per frame
	void Update () {
		if (!textSet) {
			text.text = " ";
			textSet = true;
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
				text.text = Convert.ToString(Event.current.keyCode);
				clicked = false;
			} else if (Event.current.isMouse) {
				switch((int)Event.current.button){
				case 0:
					InputManager.instance.ChangeKey(controller, input, selection, KeyCode.Mouse0);
					text.text = "LMouse Button";
					break;
				case 1:
					InputManager.instance.ChangeKey(controller, input, selection, KeyCode.Mouse1);
					text.text = "RMouse Button";
					break;
				case 2:
					InputManager.instance.ChangeKey(controller, input, selection, KeyCode.Mouse2);
					text.text = "MMouse Button";
					break;
				}
				clicked = false;
			}
		}
	}
}
