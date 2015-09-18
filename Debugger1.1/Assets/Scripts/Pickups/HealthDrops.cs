using UnityEngine;
using System.Collections;

public class HealthDrops : MonoBehaviour {

	[SerializeField]
	float dropRate = 0.0f;
	[SerializeField]
	Transform itemDrop = null;

	public void Healthdrop() {
		float dropSuccess = Random.Range (0.0f, 1.0f);
		if (dropSuccess <= dropRate) {
			Instantiate(itemDrop, transform.position, Quaternion.identity);
		}
	}
}
