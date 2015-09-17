using UnityEngine;
using System.Collections;

public class Loading: MonoBehaviour {

	// Use this for initialization
	void Awake () {
        if (GameManager.instance != null)
            GameManager.instance.LoadPlayerstatsScene(GameManager.data);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
