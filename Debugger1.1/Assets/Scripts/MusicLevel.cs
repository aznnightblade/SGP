using UnityEngine;
using System.Collections;

public class MusicLevel : MonoBehaviour {
    bool on = false;
	// Use this for initialization
	void Start () {
        gameObject.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("Music") / 100f;
	}
	
	// Update is called once per frame
	void Update () {
        gameObject.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("Music") / 100f;

        if (InputManager.instance.GetButtonDown ("Cancel"))
        {
            if (on == false)
            {
                gameObject.GetComponent<AudioSource>().Pause();
                on = true;
            }
            else
            {
                gameObject.GetComponent<AudioSource>().UnPause();
                on = false;
            }
				 
        }
           
	}
}
