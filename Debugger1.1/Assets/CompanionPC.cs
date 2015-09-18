using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CompanionPC : MonoBehaviour {

	[SerializeField]
	GameObject CompanionMenu = null;
	[SerializeField]
	Text text = null;
	[SerializeField]
	GameObject[] CompanionButtons = null;
	Player player = null;

	bool containsPlayer = false;

	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ();
	}

	// Update is called once per frame
	void Update () {
		if (containsPlayer && CompanionMenu.activeInHierarchy == false) {
			if (InputManager.instance.GetButtonDown ("Submit")) {
				GameManager.CTimeScale2 = 0.0f;
				GameObject.FindGameObjectWithTag ("Player").GetComponentInParent<Rigidbody> ().freezeRotation = true;
				text.text = "Selected Companion: " + player.SelectedCompanion;
				CompanionMenu.SetActive (true);
				
				for (int index = 0; index < CompanionButtons.Length; index++) {
					if (player.Companions[index] > 0)
						CompanionButtons [index].SetActive (true);
				}
			}
		} else if (CompanionMenu.activeInHierarchy == true) {
			if (InputManager.instance.GetButtonDown ("Cancel")) {
				ExitMenu ();
			}
		}
	}

	public void SelectCompanion (int companion) {
		switch ((Player.COMPANIONS)companion) {
		case Player.COMPANIONS.Bill:
			player.SelectedCompanion = Player.COMPANIONS.Bill;
			break;
		case Player.COMPANIONS.Bahim:
			player.SelectedCompanion = Player.COMPANIONS.Bahim;
			break;
		case Player.COMPANIONS.Chris:
			player.SelectedCompanion = Player.COMPANIONS.Chris;
			break;
		case Player.COMPANIONS.Daemon:
			player.SelectedCompanion = Player.COMPANIONS.Daemon;
			break;
		case Player.COMPANIONS.LeRoc:
			player.SelectedCompanion = Player.COMPANIONS.LeRoc;
			break;
		case Player.COMPANIONS.None:
			player.SelectedCompanion = Player.COMPANIONS.None;
			break;
		}

		text.text = "Selected Companion: " + player.SelectedCompanion;
        GameObject.FindGameObjectWithTag("HUD").GetComponent<HUDImage>().Changecompanion(player.SelectedCompanion);
	}

	public void ExitMenu () {
		GameManager.CTimeScale2 = 1.0f;
		GameObject.FindGameObjectWithTag("Player").GetComponentInParent<Rigidbody>().freezeRotation = false;
		CompanionMenu.SetActive(false);
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
