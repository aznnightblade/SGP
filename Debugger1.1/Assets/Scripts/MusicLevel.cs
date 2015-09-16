using UnityEngine;
using System.Collections;

public class MusicLevel : MonoBehaviour {

	// Use this for initialization
	void Start () {
        gameObject.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("Music") / 100f;
	}
	
	// Update is called once per frame
	void Update () {
        gameObject.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("Music") / 100f;
	}
}
