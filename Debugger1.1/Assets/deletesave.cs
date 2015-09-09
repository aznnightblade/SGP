using UnityEngine;
using System.Collections;

public class deletesave : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Deletesave()
    {
        GameManager.deletefile = true;
    }
}
