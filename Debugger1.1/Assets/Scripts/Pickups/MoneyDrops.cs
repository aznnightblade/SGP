using UnityEngine;
using System.Collections;

public class MoneyDrops : MonoBehaviour {

	[SerializeField]
	float dropRate = 0.0f;
	[SerializeField]
	Transform itemDrop = null;
	
	void OnDestroy() {
		float dropSuccess = Random.Range (0.0f, 1.0f);
		if (dropSuccess <= dropRate) {
			Transform item = Instantiate(itemDrop, transform.position, Quaternion.Euler(Vector3.zero)) as Transform;
			item.GetComponent<MoneyPickup>().DropValue = gameObject.GetComponent<Statistics>().Money;
		}
	}
}
