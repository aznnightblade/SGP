using UnityEngine;
using System.Collections;

public class ColorSwitch : MonoBehaviour {

	Player player = null;

	// Use this for initialization
	void Start () {
		player = gameObject.GetComponentInChildren<Player> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("ColorSwitch"))
			SwitchColors ();
	}

	void SwitchColors() {

	}
}
