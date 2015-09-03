using UnityEngine;
using System.Collections;

public class Binary : MonoBehaviour {
	public GameObject one;
	public GameObject zero;

	public void isOne()
	{
		zero.SetActive (false);
		one.SetActive (true);
	}
	public void isZero()
	{
		zero.SetActive (true);
		one.SetActive (false);
	}
	public void reset()
	{
		zero.SetActive (false);
		one.SetActive (false);
	}

}
