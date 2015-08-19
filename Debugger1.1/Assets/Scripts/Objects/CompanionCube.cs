using UnityEngine;
using System.Collections;

public class CompanionCube : MonoBehaviour {

	// Use this for initialization
	public int Next;
	public string HubWorld;

	void OnCollisionEnter(Collision col)
	{
		if (GameManager.indexLevel < Next) {
			GameManager.levelComplete (Next);
		}
		PlayerPrefs.SetString ("Nextscene", HubWorld);
		Application.LoadLevel ("Loadingscreen");

	}
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
