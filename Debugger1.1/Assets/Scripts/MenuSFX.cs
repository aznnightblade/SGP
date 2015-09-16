using UnityEngine;
using System.Collections;

public class MenuSFX : MonoBehaviour {
    
	// Use this for initialization
	void Start () {
        gameObject.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("SFX")/100;
	}
	
	// Update is called once per frame
	void Update () {
        gameObject.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("SFX") / 100;
	}
}
