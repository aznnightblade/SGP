using UnityEngine;
using System.Collections;

public class Corruption : Statistics {
    public SoundManager sounds;

    void Start()
    {
        sounds = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
    }
	void Update(){
        if (gameObject.GetComponent<Statistics>().CurrHealth <= 0)
        {
            sounds.MiscSoundeffects[4].Play();
            Destroy(gameObject);
        }
	}
}
