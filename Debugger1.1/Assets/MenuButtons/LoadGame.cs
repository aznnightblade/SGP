﻿using UnityEngine;
using System.Collections;

public class LoadGame : MonoBehaviour {
	public string NextScene;

	void Start () {
   
	}

	public void play()
	{
		PlayerPrefs.SetString ("Nextscene", NextScene);
        Application.LoadLevel("Loadingscreen");
	}

    
	// Update is called once per frame
	void Update () {
        
	}
}
