using UnityEngine;
using System.Collections;

public class TeleportToNewScene : MonoBehaviour {
	public string Next;
	// Use this for initialization
	void Start () {
	
	}
	void OnTriggerEnter(Collider col)
	{
		if (col.tag== "Player")
		{
			GameManager.instance.NextScene();
			PlayerPrefs.SetString ("Nextscene", Next);
			Application.LoadLevel ("Loadingscreen");
		}
	}
	// Update is called once per frame
	void Update () {
	
	}
}
