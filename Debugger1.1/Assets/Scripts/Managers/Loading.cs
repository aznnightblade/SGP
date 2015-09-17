using UnityEngine;
using System.Collections;

public class Loading: MonoBehaviour {

	// Use this for initialization
	void Awake () {
     
        GameManager.instance.LoadPlayerstatsScene(GameManager.data);
     	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
