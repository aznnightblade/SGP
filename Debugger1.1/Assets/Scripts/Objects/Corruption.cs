using UnityEngine;
using System.Collections;

public class Corruption : Statistics {

	void Update(){
		if (CurrHealth <= 0)
			Destroy (gameObject);
	}
}
