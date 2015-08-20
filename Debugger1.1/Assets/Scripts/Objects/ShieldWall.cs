using UnityEngine;
using System.Collections;

public class ShieldWall : MonoBehaviour {
	public GameObject attached;
	public bool IsOn = true;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () 
	{
	/*
		if (attached.GetComponent<Switch>().getFlipped() == true){
			if (IsOn) {
				gameObject.SetActive(false);
				attached.GetComponent<Switch>().setFlipped(false);
				IsOn = false;
			} else {
				gameObject.SetActive (true);
				IsOn = true;
				attached.GetComponent<Switch>().setFlipped(false);
			}
		}
 		*/ 
	}
}
