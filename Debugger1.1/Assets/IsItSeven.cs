using UnityEngine;
using System.Collections;

public class IsItSeven : MonoBehaviour {
	public GameObject door;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	if (GameManager.indexLevel == 7)
			door.SetActive (false);
	}
}
