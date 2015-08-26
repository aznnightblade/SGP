using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CardSpawner : MonoBehaviour {

	[SerializeField]
	Transform card = null;
	List<GameObject> cards = new List<GameObject> ();

	[SerializeField]
	float distanceFromBase = 1.5f;

	[SerializeField]
	float rotSpeed = 3.0f;
	float rot = 0.0f;

	// Use this for initialization
	void Start () {
		SpawnCards ();
	}
	
	// Update is called once per frame
	void Update () {
		rot += rotSpeed * Time.deltaTime * GameManager.CTimeScale;
		transform.rotation = Quaternion.Euler (0, rot, 0);
	}

	public void SpawnCards () {
		if (cards.Count > 0) {
			for (int index = cards.Count; index >= 0; index--){
				Destroy(cards[index]);
			}

			cards.Clear();
		}
	
		cards.Add((Instantiate(card, new Vector3(transform.position.x - distanceFromBase, transform.position.y, transform.position.z), Quaternion.Euler(0, 180, 0)) as Transform).gameObject);
		cards.Add((Instantiate(card, new Vector3(transform.position.x, transform.position.y, transform.position.z - distanceFromBase), Quaternion.Euler(0, 90, 0)) as Transform).gameObject);
		cards.Add((Instantiate(card, new Vector3(transform.position.x + distanceFromBase, transform.position.y, transform.position.z), Quaternion.Euler(0, 0, 0)) as Transform).gameObject);
		cards.Add((Instantiate(card, new Vector3(transform.position.x, transform.position.y, transform.position.z + distanceFromBase), Quaternion.Euler(0, 270, 0)) as Transform).gameObject);
		cards.Add((Instantiate(card, new Vector3(transform.position.x + distanceFromBase * Mathf.Sqrt(2) * 0.5f, transform.position.y, transform.position.z - distanceFromBase * Mathf.Sqrt(2) * 0.5f), Quaternion.Euler(0, 225, 0)) as Transform).gameObject);
		cards.Add((Instantiate(card, new Vector3(transform.position.x - distanceFromBase * Mathf.Sqrt(2) * 0.5f, transform.position.y, transform.position.z - distanceFromBase * Mathf.Sqrt(2) * 0.5f), Quaternion.Euler(0, 135, 0)) as Transform).gameObject);
		cards.Add((Instantiate(card, new Vector3(transform.position.x + distanceFromBase * Mathf.Sqrt(2) * 0.5f, transform.position.y, transform.position.z + distanceFromBase * Mathf.Sqrt(2) * 0.5f), Quaternion.Euler(0, 135, 0)) as Transform).gameObject);
		cards.Add((Instantiate(card, new Vector3(transform.position.x - distanceFromBase * Mathf.Sqrt(2) * 0.5f, transform.position.y, transform.position.z + distanceFromBase * Mathf.Sqrt(2) * 0.5f), Quaternion.Euler(0, 225, 0)) as Transform).gameObject);

		for (int index = 0; index < cards.Count; index++) {
			cards [index].transform.parent = transform;
			cards [index].GetComponent<Statistics> ().Color = (DLLColor.Color)Random.Range(0, 4);
		}
	}

	public List<GameObject> Cards { get { return cards; } }
}
