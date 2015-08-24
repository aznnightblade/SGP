using UnityEngine;
using System.Collections;

public class loadinglevel : MonoBehaviour {

	// Use this for initialization
	void Start () {

        Application.LoadLevelAsync(PlayerPrefs.GetString("Nextscene"));

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
