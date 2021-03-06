﻿using UnityEngine;
using System.Collections;

public class CompanionCube : MonoBehaviour {

	// Use this for initialization
	public int Next;
	public string HubWorld;
	bool activateSwitch;
	void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Player" && col.name == "Player Stats")
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
		if (InputManager.instance.GetButtonDown ("Submit") && activateSwitch == true) {
			Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player> ();

			if (GameManager.indexLevel < Next) {
				GameManager.levelComplete (Next);
			}

			if (player.IsCompanionActive)
				player.ActiveCompanion.GetComponent<Companion> ().RemoveAllie ();

			GameManager.back = true;
			GameManager.instance.NextScene();
			PlayerPrefs.SetString ("Nextscene", HubWorld);
			Application.LoadLevel ("Loadingscreen");

	}
}
}
