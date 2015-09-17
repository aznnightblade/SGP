using UnityEngine;
using System.Collections;

public class VendingMachine : MonoBehaviour {

	bool triggerActive = false;
	public string loadingLevel;
	//public Player player;

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
		if (InputManager.instance.GetButtonDown ("Submit")) {
			if (triggerActive == true) {
				GameManager.lastPosition = FindObjectOfType<Player> ().transform.position;
				GameManager.instance.NextScene();
				PlayerPrefs.SetString ("Nextscene", loadingLevel);
				Application.LoadLevel ("Loadingscreen");
				
			}
		}
	}
}
