using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.Collections;

public class DisplayVolume : MonoBehaviour {

	// Use this for initialization
	void Start () {
		gameObject.GetComponent<Text> ().text = (AudioListener.volume * 100).ToString(); // placeholder line, for testing purposes
	}
	
	// Update is called once per frame
	void Update () {
		if (gameObject.GetComponentInParent<Transform> ().name == "SFX") {

		} else if (gameObject.GetComponentInParent<Transform> ().name == "Music") {

		}
	}
}
