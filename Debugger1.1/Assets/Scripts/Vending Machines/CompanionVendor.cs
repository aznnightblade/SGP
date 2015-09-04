using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CompanionVendor : MonoBehaviour {

	bool triggerActive = false;
	public GameObject Panel;
	public GameObject Button;
	public GameObject Panel2;
	Player player;
	public Text costtext;
	public Text text;
	
	public Player.COMPANIONS CompanionToUpgrade = Player.COMPANIONS.None;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Panel.activeSelf == true) {
			if (InputManager.instance.GetButtonDown ("Cancel")) {
				Panel.SetActive (false);
				Button.SetActive (false);
				Panel2.SetActive (true);

				print("Cancel was hit");
				
				player.GetComponentInParent<PlayerController> ().enabled = true;
			}
		}

		if (triggerActive == true && InputManager.instance.GetButtonDown ("Submit") && !Panel.activeInHierarchy) {
			Panel.SetActive (true);
			Button.SetActive (true);

			player.GetComponentInParent<PlayerController> ().enabled = false;
			player.GetComponentInParent<Rigidbody> ().velocity = Vector3.zero;
			player.GetComponentInParent<Rigidbody> ().freezeRotation = true;

			Button.GetComponent<CompanionVendor> ().CompanionToUpgrade = CompanionToUpgrade;

			Text text = Button.GetComponentInChildren<Text> ();
			
			if (player.Companions [(int)CompanionToUpgrade] == 0) {
				text.text = "Acquire " + CompanionToUpgrade.ToString();
			} else {
				text.text = "Upgrade " + CompanionToUpgrade.ToString();
			}

			if (player.Companions[(int)CompanionToUpgrade] == 0) {
				costtext.text = "Acquiring " + CompanionToUpgrade.ToString() + "'s aid will cost 300 Credits";
			} else if (player.Companions[(int)CompanionToUpgrade] < 5) {
				costtext.text = "Upgrade " + CompanionToUpgrade.ToString() + "'s power for " + Mathf.FloorToInt(600 * player.Companions[(int)CompanionToUpgrade]).ToString() + " Credits";
			} else {
				costtext.text = CompanionToUpgrade.ToString() + " has max upgrades";
			}
		}
	}

	void OnTriggerEnter(Collider col) {
		if (col.tag == "Player") {
			triggerActive = true;
		}
	}

	void OnTriggerExit(Collider col) {
		Panel2.SetActive(true);

		if (col.tag == "Player") {
			triggerActive = false;
		}
	}

	public void OnClick () {
		Panel.SetActive(false);
		Button.SetActive(false);
		Panel2.SetActive(false);

		print ("Got in OnClick()");

		if (player.Companions [(int)CompanionToUpgrade] < 5) {
			int cost = 0;

			if (player.Companions [(int)CompanionToUpgrade] == 0) {
				cost = 300;
			} else {
				cost = Mathf.FloorToInt (600 * player.Companions [(int)CompanionToUpgrade]);
			}

			if (player.Money >= cost) {
				player.Money -= cost;

				player.Companions [(int)CompanionToUpgrade]++;

				if (player.Companions [(int)CompanionToUpgrade] == 1) {
					text.text = CompanionToUpgrade.ToString() + " has been acquired!";
				} else {
					text.text = CompanionToUpgrade.ToString() + " has been upgraded to level " + player.Companions[(int)CompanionToUpgrade];
				}
			} else {
				text.text = "Not enough money to upgrade " + CompanionToUpgrade.ToString();
			}
		} else {
			text.text = CompanionToUpgrade.ToString() + " is maxed out!";
		}

		player.GetComponentInParent<PlayerController>().enabled = true;
	}
}
