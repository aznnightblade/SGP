using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ConvyorBelt : MonoBehaviour {

	List<Transform> Objects = new List<Transform>();
	[SerializeField]
	Vector3 dirVelocity = Vector3.zero;

	bool isEnabled = true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		for (int index = 0; index < Objects.Count; index++) {
			Objects[index].position += dirVelocity * Time.deltaTime;
		}
	}

	void OnTriggerEnter(Collider col) {
		if (col.gameObject.tag == "Player") {
			Objects.Add (col.transform.parent);
		} else {
			Objects.Add(col.transform);
		}
	}

	void OnTriggerExit(Collider col) {
		if (col.gameObject.tag == "Player") {
			Objects.Remove (col.transform.parent);
		} else {
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
}
