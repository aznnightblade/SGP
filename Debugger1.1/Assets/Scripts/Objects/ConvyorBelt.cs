using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ConvyorBelt : MonoBehaviour {

	List<Transform> Objects = new List<Transform>();
	[SerializeField]
	Vector3 dirVelocity = Vector3.zero;

	float speed = 0.0f;

	bool isEnabled = true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (isEnabled) {
			for (int index = 0; index < Objects.Count; index++) {
				if (Objects[index] != null)
					Objects [index].position += dirVelocity * Time.deltaTime * GameManager.CTimeScale;
				else
					Objects.RemoveAt(index);
			}
		}

		if (gameObject.GetComponentInChildren<Animator> ().speed == 0.0f && GameManager.CTimeScale > 0.0f)
			gameObject.GetComponentInChildren<Animator> ().speed = speed;
		else if (gameObject.GetComponentInChildren<Animator> ().speed > 0.0f && GameManager.CTimeScale == 0.0f)
			gameObject.GetComponentInChildren<Animator> ().speed = 0;
	}

	void OnTriggerEnter(Collider col) {
		if (col.gameObject.tag == "Player") {
			Objects.Add (col.transform.parent);
		} else {
            if (col.gameObject.tag != "Lazer")
			    Objects.Add(col.transform);
		}
	}

	void OnTriggerExit(Collider col) {
		if (col.gameObject.tag == "Player") {
			Objects.Remove (col.transform.parent);
		} else {
			if (col.gameObject.tag != "Lazer")
				Objects.Remove(col.transform);
		}
	}

	public Vector3 DirectionVelocity {
		get { return dirVelocity; }
		set { dirVelocity = value; }
	}

	public bool IsEnabled {
		get { return isEnabled; }
		set { isEnabled = value; }
	}

	public float Speed {
		get { return speed; }
		set { speed = value; }
	}
}
