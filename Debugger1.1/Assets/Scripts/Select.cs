using UnityEngine;
using System.Collections;

public class Select : MonoBehaviour {
	public string Next;
	public int difficulty;
	// Use this for initialization
	void Start () {
	
	}
	public void Click(){
		GameManager.back = false;
		GameManager.difficulty = difficulty;
		Next = GameManager.nextlevelname;
		GameManager.instance.NextScene();
		PlayerPrefs.SetString ("Nextscene", Next);
		Application.LoadLevel ("Loadingscreen");

	}
	// Update is called once per frame
	void Update () {
	
	}
}
