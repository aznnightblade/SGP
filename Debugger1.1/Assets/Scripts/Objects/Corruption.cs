using UnityEngine;
using System.Collections;

public class Corruption : Statistics {

	void Update(){
		if (gameObject.GetComponent<Statistics> ().CurrHealth <= 0)
			Destroy (gameObject);
	}
}
