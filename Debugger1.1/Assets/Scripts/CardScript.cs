using UnityEngine;
using System.Collections;

public class CardScript : Statistics {

	[SerializeField]
	Sprite[] sprites = null;

	// Use this for initialization
	void Start () {
		gameObject.GetComponentInChildren<SpriteRenderer> ().sprite = sprites [(int)color];

		UpdateStats ();
	}
	
	// Update is called once per frame
	void Update () {
		if (currHealth <= 0.0f)
			Destroy (gameObject);
	}
}
