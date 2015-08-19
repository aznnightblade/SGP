using UnityEngine;
using System.Collections;

public class Switch : MonoBehaviour {
	public int Health;
	public int MaxHealth;
	bool activateSwitch = false;
	public bool Flipped = false;
	public Light OnLight;
	// Use this for initialization
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

	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.tag == "Player Bullet") 
		{
			//  
			if(col.gameObject.GetComponent<Weapon>().Color == DLLColor.Color.NEUTRAL)
			{
				if (Health != MaxHealth)
				Health = Health + 5;
			}
		}
	}

	
	// Update is called once per frame
	void Update () 
	{
		if (Health == MaxHealth) 
		{
			gameObject.GetComponentInChildren<Light> ().enabled = true;
			if (Input.GetButtonDown ("Submit") && activateSwitch == true) {
				Flipped = true;
				gameObject.GetComponentInChildren<Light> ().enabled = false;
				Health = 0;
			}

		}
	}

}


