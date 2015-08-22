using UnityEngine;
using System.Collections;

public class Corruption : Statistics {

    void Start()
    {
        sounds = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
    }
	void Update(){
		if (CurrHealth <= 0)
        {
            Destroy(gameObject);
            
        }
			
	}
}
