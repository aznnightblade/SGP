﻿using UnityEngine;
using System.Collections;

public class Computer : MonoBehaviour {

    bool triggerActive = false;
	public string Loadinglevel;
	public int levelNumber;
	public Player player;
	// Use this for initialization
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
			if (triggerActive == true && GameManager.indexLevel >= levelNumber) {
				GameManager.lastPosition = FindObjectOfType<Player> ().transform.position;
				GameManager.back = false;
				GameManager.nextlevelname = Loadinglevel;
				GameManager.instance.NextScene();
				PlayerPrefs.SetString ("Nextscene", "Select");
				Application.LoadLevel ("Loadingscreen");

			}
		}
    }
}
