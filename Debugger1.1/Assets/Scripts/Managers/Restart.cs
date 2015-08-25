using UnityEngine;
using System.Collections;

public class Restart : MonoBehaviour {
	public string thislevel;
	// Use this for initialization
	void Start () {

	}

	public void restart()
	{
		Time.timeScale = 1;
		PlayerPrefs.SetString ("Nextscene", thislevel);
		GameManager.back = false;
		Application.LoadLevel("Loadingscreen");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
