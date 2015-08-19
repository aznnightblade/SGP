using UnityEngine;
using System.Collections;

public class MoneyPickup : MonoBehaviour {

	public SoundManager sound;
	[SerializeField]
	int dropValue = 0;

	// Use this for initialization
	void Start () {
		sound = GameObject.FindGameObjectWithTag ("SoundManager").GetComponent<SoundManager> ();
	}
	
	void OnTriggerEnter(Collider col) {
		if (col.tag == "Player") {
			Player player = col.GetComponent<Player>();
			player.Money += dropValue;
			DestroyObject(gameObject);
		}
	}

	public int DropValue {
		get { return dropValue; }
		set { dropValue = value; }
	}
}
