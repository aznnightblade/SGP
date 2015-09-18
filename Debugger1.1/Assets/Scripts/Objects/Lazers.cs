using UnityEngine;
using System.Collections;

public class Lazers : MonoBehaviour {

	[SerializeField]
	Sprite[] lazerSprites = null;
	DLLColor.Color color = DLLColor.Color.NEUTRAL;

	void OnTriggerEnter(Collider Col) {
		if (Col.gameObject.GetComponent<Statistics> () != null) {
			if (Col.gameObject.tag == "Player" && Col.name == "Player" || Col.gameObject.GetComponent<Statistics> ().CurrColor != color) {
				if (Col.gameObject.tag == "Player")
					Col.gameObject.GetComponentInChildren<Player> ().DamagePlayer(9999, transform);
				else if(Col.gameObject.tag == "Dampener")
					return;
				else
					Col.gameObject.GetComponent<Statistics> ().Damage(9999, transform);
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
