using UnityEngine;
using System.Collections;

public class Loading: MonoBehaviour {

	// Use this for initialization
	void Awake () {
        if (GameManager.instance !=null)
        {
            GameManager.instance.LoadScene();
        }
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
