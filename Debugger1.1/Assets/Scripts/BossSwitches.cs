using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BossSwitches : MonoBehaviour {
	public int Health = 100;
	public int MaxHealth = 100;
	bool activateSwitch = false;
	public GameObject A;
	public GameObject B;
	public GameObject X;
	public GameObject O;

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
	public void Reset()
	{
		O.SetActive (false);
		X.SetActive (false);
		Health = MaxHealth;
	}
	public void JoesChoice()
	{
		X.SetActive (true);
		Health = 0;
		gameObject.GetComponentInChildren<Image>().fillAmount = 0;
		gameObject.GetComponentInChildren<Light> ().enabled = false;
	}
		// Update is called once per frame
	void Update () 
		{

			gameObject.GetComponentInChildren<Image>().fillAmount = (float)Health / MaxHealth;
			if (Health == MaxHealth) 
			{
				gameObject.GetComponentInChildren<Light> ().enabled = true;
				if (Input.GetButtonDown ("Submit") && activateSwitch == true) 
			{
					gameObject.GetComponentInChildren<Light> ().enabled = false;
					Health = 0;
					gameObject.GetComponentInChildren<Image>().fillAmount = 0;
					O.SetActive(true);
					if(A.activeInHierarchy)
					{
						A.SetActive(false);
						B.SetActive(true);
					}
				else if (B.activeInHierarchy)
					{
						B.SetActive(false);
						A.SetActive(true);
						
					}
					
				}
				
			}
		}
	}

