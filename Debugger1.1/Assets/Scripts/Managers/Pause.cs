using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour {
	public bool on = false;
	public GameObject pause;
	public GameObject options;
	public GameObject keyconfig;
	public GameObject freezeTint;

	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {
	
		if (InputManager.instance.GetButtonDown ("Cancel")) {
			if (on == false && !GameManager.InMenu) {
                GameManager.CTimeScale = 0;
                GameManager.CTimeScale2 = 0;
				pause.SetActive(true);
				freezeTint.SetActive(true);
				Transform player = GameObject.FindGameObjectWithTag("Player").transform;
				player.GetComponentInParent<Rigidbody>().freezeRotation = true;
				on = true;
			} else if (pause.activeInHierarchy) {
                GameManager.CTimeScale = 1;
                GameManager.CTimeScale2 = 1;
				pause.SetActive(false);
				freezeTint.SetActive(false);
				Transform player = GameObject.FindGameObjectWithTag("Player").transform;
				player.GetComponentInParent<Rigidbody>().freezeRotation = false;
				on = false;
			} else if (options.activeInHierarchy) {
				options.SetActive(false);
				pause.SetActive(true);
			} else if (keyconfig.activeInHierarchy){
				InputManager.instance.SaveInputs ();
				keyconfig.SetActive(false);
				options.SetActive(true);
			}
		}
	
	}
}
