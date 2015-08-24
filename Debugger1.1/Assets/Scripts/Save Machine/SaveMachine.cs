using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class SaveMachine : MonoBehaviour {

	bool triggerActive = false;
    public GameObject Panel;
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
				GameManager.lastPosition = FindObjectOfType<Player> ().transform.position;
				Application.LoadLevel ("SaveGame");

			}
		}
	}
}
