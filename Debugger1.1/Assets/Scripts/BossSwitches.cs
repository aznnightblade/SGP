﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BossSwitch : MonoBehaviour {
	public int Health = 100;
	public int MaxHealth = 100;
	bool activateSwitch = false;
	public GameObject A;
	public GameObject B;
	public GameObject[] Attached;

	// Use this for initialization
	void Start(){
	
	}
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
	
		// Update is called once per frame
	void Update () 
		{
			gameObject.GetComponentInChildren<Image>().fillAmount = (float)Health / MaxHealth;
			if (Health == MaxHealth) 
			{
				gameObject.GetComponentInChildren<Light> ().enabled = true;
				if (Input.GetButtonDown ("Submit") && activateSwitch == true) {
					gameObject.GetComponentInChildren<Light> ().enabled = false;
					Health = 0;
					gameObject.GetComponentInChildren<Image>().fillAmount = 0;
					if(A.activeInHierarchy)
					{
						A.SetActive(false);
						B.SetActive(true);
						
						
					}else if (B.activeInHierarchy)
					{
						B.SetActive(false);
						A.SetActive(true);
						
					}
					
				}
				
			}
		}
	}

