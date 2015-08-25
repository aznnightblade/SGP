﻿using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour {
	public bool on = false;
	public GameObject pause;
	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {
	
		if (InputManager.instance.GetButtonDown ("Cancel")) {
			if (on == false) {
				Time.timeScale = 0;
				pause.SetActive(true);
				Transform player = GameObject.FindGameObjectWithTag("Player").transform;
				player.GetComponentInParent<Rigidbody>().freezeRotation = true;
				on = true;
			} else {
				Time.timeScale = 1;
				pause.SetActive(false);
				Transform player = GameObject.FindGameObjectWithTag("Player").transform;
				player.GetComponentInParent<Rigidbody>().freezeRotation = false;
				on = false;
			}
		}
	
	}
}
