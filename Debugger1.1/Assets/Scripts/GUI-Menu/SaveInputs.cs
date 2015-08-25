using UnityEngine;
using System.Collections;

public class SaveInputs : MonoBehaviour {

	public void Save () {
		InputManager.instance.SaveInputs ();
	}
}
