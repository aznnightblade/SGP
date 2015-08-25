using UnityEngine;
using System.Collections;

public class Loading: MonoBehaviour {

	// Use this for initialization
	void Awake () {
		GameManager.instance.LoadScene();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
