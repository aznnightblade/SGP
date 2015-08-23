using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Switch : MonoBehaviour {
	public int Health = 0;
	public int MaxHealth = 100;
	bool activateSwitch = false;
	public int colorNumber;
	public GameObject A;
	public GameObject B;
	public GameObject[] Attached;
	DLLColor.Color color = DLLColor.Color.NEUTRAL;
	// Use this for initialization
	void Start(){
		if (colorNumber == 0)
			color = DLLColor.Color.NEUTRAL;

		if (colorNumber == 1)
			color = DLLColor.Color.RED;

		if (colorNumber == 2)
			color = DLLColor.Color.GREEN;

		if (colorNumber == 3)
			color = DLLColor.Color.BLUE;
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

	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.tag == "Player Bullet") 
		{
	
			if( col.gameObject.GetComponent<Weapon>().Color == color)
			{
				if (Health != MaxHealth)
				Health = Health + 5;
			

			}
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

				for(int i = 0; i < Attached.Length; ++i)
				{
					if(Attached[i].tag == "Lazer")
					{
						Attached[i].GetComponentInChildren<LazerSpawner>().IsEnabled = !Attached[i].GetComponentInChildren<LazerSpawner>().IsEnabled;
					}
					else if(Attached[i].tag == "Belt")
					{
						Attached[i].GetComponentInChildren<ConveyorBeltSpawner>().MoveDirection = -Attached[i].GetComponentInChildren<ConveyorBeltSpawner>().MoveDirection;
					}					
					else if(Attached[i].activeSelf == true)
					{
						Attached[i].SetActive(false);
					}
					else
					{
						Attached[i].SetActive(true);
					}

				}

			}

		}
	}
}
