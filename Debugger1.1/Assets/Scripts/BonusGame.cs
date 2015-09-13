using UnityEngine;
using System.Collections;

public class BonusGame : MonoBehaviour {
	public int credits;
	public GameObject player;
	public GameObject[] show;
	public GameObject five;
	public GameObject one;
	// Use this for initialization
	void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Enemy" )
		{
			player.GetComponentInChildren<Player>().Money += credits;

			switch(credits)
			{
			case 1:
			{
				show[0].SetActive(true);
				show[1].SetActive(false);
				show[2].SetActive(false);
				break;
			}
			case 5:
			{
				show[0].SetActive(false);
				show[1].SetActive(true);
				show[2].SetActive(false);
				break;
			}
			case 25:
			{
				show[0].SetActive(false);
				show[1].SetActive(false);
				show[2].SetActive(true);
				break;
			}
		}
	}
}
}
