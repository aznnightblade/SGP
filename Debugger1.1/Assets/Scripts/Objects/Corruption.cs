using UnityEngine;
using System.Collections;

public class Corruption : Statistics {

    void Start()
    {
        
    }
	void Update(){
		if (CurrHealth <= 0)
        {
            SoundManager.instance.MiscSoundeffects[4].Play();
            Destroy(gameObject);
            
        }
			
	}
}
