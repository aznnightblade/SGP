using UnityEngine;
using System.Collections;

public class Sound : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		if (SoundManager.instance != null) {
			for (int i = 0; i < SoundManager.instance.Music.Count; i++) {
				SoundManager.instance.Music [i].Stop ();
			}
			for (int i = 0; i < SoundManager.instance.BossMusic.Count; i++) {
				SoundManager.instance.BossMusic [i].Stop ();
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
