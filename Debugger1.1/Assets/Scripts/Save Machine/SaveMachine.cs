using UnityEngine;
using System.Collections;

public class SaveMachine : MonoBehaviour {

	void OnCollsionEnter(Collision col) {
		if (col.gameObject.tag == "Player") {
			Application.LoadLevel("SaveGame");
		}
	}
}
