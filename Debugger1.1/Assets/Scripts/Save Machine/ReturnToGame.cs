using UnityEngine;
using System.Collections;

public class ReturnToGame : MonoBehaviour {

	public void Exit() {
		GameManager.back = true;

		Application.LoadLevel ("MainMenu");
	}
}
