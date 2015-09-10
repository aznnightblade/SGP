using UnityEngine;
using System.Collections;

public class LazerMaster : MonoBehaviour {
	public GameObject[] Bosslazers;
	// Use this for initialization
	public void allOff()
	{
		for (int i = 0; i < Bosslazers.Length; ++i) {
			Bosslazers[i].SetActive(false);
		}
	}
	public void allOn()
	{
		for (int i = 0; i < Bosslazers.Length; ++i) {
			Bosslazers[i].SetActive(true);
		}
	}

	public void SingleToggle(int index,bool OnorOff)
	{
		Bosslazers [index].SetActive (OnorOff);
	}
}
