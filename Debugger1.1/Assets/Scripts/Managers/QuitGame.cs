using UnityEngine;
using System.Collections;

public class QuitGame : MonoBehaviour {
	public string NextScene;
	// Use this for initialization
	void Start () {

	}
	public void click()
	{
		GameManager.instance.DestroyGame ();
		Time.timeScale = 1;
		PlayerPrefs.SetString ("Nextscene", NextScene);
		Application.LoadLevel("Loadingscreen");
	}
	// Update is called once per frame
	void Update () {
	
	}
}
