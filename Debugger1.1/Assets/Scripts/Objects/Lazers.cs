using UnityEngine;
using System.Collections;

public class Lazers : MonoBehaviour {

	[SerializeField]
	Sprite[] lazerSprites = null;
	DLLColor.Color color = DLLColor.Color.NEUTRAL;

	void OnTriggerEnter(Collider Col) {
		if (Col.gameObject.GetComponent<Statistics> () != null) {
			if (Col.gameObject.tag == "Player" || Col.gameObject.GetComponent<Statistics> ().Color != color) {
				if (Col.gameObject.tag == "Player")
					Col.gameObject.GetComponentInChildren<Player> ().CurrHealth = 0;
				else
					Col.gameObject.GetComponent<Statistics> ().CurrHealth = 0;
			}
		}
	}

	void ChangeColor (DLLColor.Color col) {
		color = col;
		
		gameObject.GetComponentInChildren<SpriteRenderer> ().sprite = lazerSprites[(int)color];
	}

	public DLLColor.Color Color {
		get { return color; }
		set { ChangeColor(value); }
	}
}
