using UnityEngine;
using System.Collections;

public class CompanionCube : MonoBehaviour {

	// Use this for initialization
	public int Next;
	public string HubWorld;
	bool activateSwitch;
	void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Player" )
		{
			activateSwitch = true;
		}
	}
	
	void OnTriggerExit(Collider col)
	{
		if (col.tag== "Player")
		{
			activateSwitch = false;
		}
	}

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Submit") && activateSwitch == true) {
			if (GameManager.indexLevel < Next) {
				GameManager.levelComplete (Next);
			}
			PlayerPrefs.SetString ("Nextscene", HubWorld);
			Application.LoadLevel ("Loadingscreen");
			GameManager.back = true;

	}
}
}
