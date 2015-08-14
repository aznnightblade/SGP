using UnityEngine;
using System.Collections;

public class EnterVendorMenu : MonoBehaviour {

	[SerializeField]
	string vendorMenu; // the name of the scene for each type of vending machine

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision col) {
		Application.LoadLevel (vendorMenu);
	}
}
