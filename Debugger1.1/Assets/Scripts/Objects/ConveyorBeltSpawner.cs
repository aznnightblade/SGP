using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ConveyorBeltSpawner : MonoBehaviour {

	[SerializeField]
	Transform conveyorBelt = null;
	List<GameObject> conveyorBelts = new List<GameObject> ();
	Vector3 direction = Vector3.zero;
	[SerializeField]
	Vector3 moveDirection = Vector3.zero;

	[SerializeField]
	bool isEnabled = true;
	bool isReversed = false;

	[SerializeField]
	Transform startPoint = null;
	[SerializeField]
	Transform endPoint = null;

	// Use this for initialization
	void Start () {
		direction = (endPoint.position - startPoint.position).normalized;
		int distance = Mathf.CeilToInt (Vector3.Distance (startPoint.position, endPoint.position));

		for (int index = 0; index <= distance; index += 2) {
			Vector3 pos = startPoint.transform.position + direction * index;

			GameObject newConveyorBelt = (Instantiate (conveyorBelt, pos, transform.rotation) as Transform).gameObject;
			newConveyorBelt.GetComponent<ConvyorBelt> ().DirectionVelocity = moveDirection;
			newConveyorBelt.GetComponent<ConvyorBelt> ().Speed = moveDirection.magnitude * 0.5f;
			newConveyorBelt.GetComponentInChildren<Animator> ().speed = moveDirection.magnitude * 0.5f;
			newConveyorBelt.transform.parent = transform;
		}
	}

	void UpdateDirection (Vector3 dir) {
		moveDirection = dir;
		float speed = moveDirection.magnitude * 0.5f;

		if (moveDirection.normalized == direction) {
			isReversed = false;

			for (int index = 0; index < conveyorBelts.Count; index++) {
				conveyorBelts[index].GetComponentInChildren<Animator> ().SetBool ("IsReversed", isReversed);
				conveyorBelts[index].GetComponentInChildren<Animator> ().speed = speed;
				conveyorBelts[index].GetComponentInChildren<ConvyorBelt> ().Speed = speed;
			}
		} else {
			isReversed = true;

			for (int index = 0; index < conveyorBelts.Count; index++) {
				conveyorBelts[index].GetComponentInChildren<Animator> ().SetBool ("IsReversed", isReversed);
				conveyorBelts[index].GetComponentInChildren<Animator> ().speed = speed;
				conveyorBelts[index].GetComponentInChildren<ConvyorBelt> ().Speed = speed;
			}
		}

		for (int index = 0; index < conveyorBelts.Count; index++) {
			conveyorBelts[index].GetComponent<ConvyorBelt> ().DirectionVelocity = moveDirection;
		}
	}

	void UpdateEnabled (bool enabled) {
		for (int index = 0; index < conveyorBelts.Count; index++) {
			conveyorBelts[index].GetComponent<ConvyorBelt> ().IsEnabled = enabled;
		}
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.green;
		Gizmos.DrawWireCube(startPoint.position, transform.localScale);
		
		Gizmos.color = Color.red;
		Gizmos.DrawWireCube(endPoint.position, transform.localScale);
	}

	public Vector3 MoveDirection {
		get { return moveDirection; }
		set { UpdateDirection(value); }
	}

	public bool IsEnabled {
		get { return isEnabled; }
		set { UpdateEnabled(value); }
	}
}
