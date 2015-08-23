using UnityEngine;
using System.Collections;

public class SaveMachine : MonoBehaviour {

	bool triggerActive = false;

	void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Player" )
		{
			triggerActive = true;
		}
	}

	void OnTriggerExit(Collider col)
	{
		if (col.tag== "Player")
		{
			triggerActive = false;
		}
	}

	void Update()
	{
		if (Input.GetButtonDown ("Submit")) {
			if (triggerActive == true) {
				Application.LoadLevel ("SaveGame");
			}
		}
	}
}
