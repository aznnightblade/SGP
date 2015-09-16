using UnityEngine;
using System.Collections;

public class TutorialPC : MonoBehaviour {

	[SerializeField]
	GameObject tutorialMenu = null;
	[SerializeField]
	GameObject[] tutorials = null;

	bool containsPlayer = false;

	void Update () {
		if (containsPlayer) {
			if (InputManager.instance.GetButtonDown("Submit")) {
				GameManager.CTimeScale2 = 0.0f;
				GameObject.FindGameObjectWithTag("Player").GetComponentInParent<Rigidbody>().freezeRotation = true;
				tutorialMenu.SetActive(true);

				for (int index = 0; index < GameManager.indexLevel + 1 && index < tutorials.Length; index++) {
					tutorials[index].SetActive(true);
				}
			}
		}
	}

	public void ExitMenu () {
		GameManager.CTimeScale2 = 1.0f;
		GameObject.FindGameObjectWithTag("Player").GetComponentInParent<Rigidbody>().freezeRotation = false;
		tutorialMenu.SetActive(false);

		for (int index = 0; index < GameManager.indexLevel + 1 && index < tutorials.Length; index++) {
			tutorials[index].SetActive(false);
		}
	}

	void OnTriggerEnter (Collider col) {
		if (col.gameObject.tag == "Player Controller" || col.gameObject.tag == "Player")
			containsPlayer = true;
	}

	void OnTriggerExit (Collider col) {
		if (col.gameObject.tag == "Player Controller" || col.gameObject.tag == "Player")
			containsPlayer = false;
	}
}
