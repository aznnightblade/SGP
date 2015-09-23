using UnityEngine;
using System.Collections;

public class LoadGame : MonoBehaviour {
	public string NextScene;

	void Start () {
   
	}

	public void play()
	{
		if (GameManager.CTimeScale < 1.0f || GameManager.CTimeScale2 < 1.0f) {
			GameManager.CTimeScale = 1.0f;
			GameManager.CTimeScale2 = 1.0f;
		}
		PlayerPrefs.SetString ("Nextscene", NextScene);
        Application.LoadLevel("Loadingscreen");
	}

    
	// Update is called once per frame
	void Update () {
        
	}
}
