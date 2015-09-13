using UnityEngine;
using System.Collections;

public class ReturnToGame : MonoBehaviour {

    [SerializeField]
    string levelToLoad = "MainMenu";
	public void Exit() {
		GameManager.back = true;

		Application.LoadLevel (levelToLoad);
	}
}
