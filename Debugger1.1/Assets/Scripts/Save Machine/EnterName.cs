using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnterName : MonoBehaviour {

	public string saveName;

	public void SubmitSaveName() {
		switch (saveName) {
		case "Save 1":
			GameManager.saveSpot1 = GetComponent<GUIText>().text;
			break;
		case "Save 2":
			GameManager.saveSpot2 = GetComponent<GUIText>().text;
			break;
		case "Save 3":
			GameManager.saveSpot3 = GetComponent<GUIText>().text;
			break;
		}
		//saveName = GetComponent<GUIText>().text;

		Application.LoadLevel("SaveGame");
	}
}
