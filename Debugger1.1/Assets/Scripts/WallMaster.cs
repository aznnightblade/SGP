using UnityEngine;
using System.Collections;

public class WallMaster : MonoBehaviour {
	public GameObject red;
	public GameObject blue;
	public GameObject green;

	public void RedSet(bool OnOff)
	{
		red.SetActive (OnOff);
	}
	public void GreenSet(bool OnOff)
	{
		blue.SetActive (OnOff);
	}
	public void BlueSet(bool OnOff)
	{
		green.SetActive (OnOff);
	}
}
