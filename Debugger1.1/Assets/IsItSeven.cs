using UnityEngine;
using System.Collections;

public class IsItSeven : MonoBehaviour {
	public GameObject door;
	public GameObject pressEnter;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	if (GameManager.indexLevel == 7)
			door.SetActive (false);

	if (GameManager.indexLevel > 1)
			pressEnter.SetActive (false);
	}

}
