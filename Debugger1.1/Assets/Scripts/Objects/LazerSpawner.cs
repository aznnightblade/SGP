using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LazerSpawner : MonoBehaviour {

	[SerializeField]
	DLLColor.Color color = DLLColor.Color.NEUTRAL;
	List<GameObject> lazers = new List<GameObject> ();
	Vector3 direction = Vector3.zero;
	[SerializeField]
	float maxDistance = 100.0f;
	[SerializeField]
	float speed = 5.0f;
	Vector3 moveDirection = Vector3.zero;

	[SerializeField]
	bool isEnabled = true;

	[SerializeField]
	bool HasEndpoint = true;

	[SerializeField]
	bool IsStationary = true;
	Transform target = null;

	[SerializeField]
	Transform endPoint = null;
	[SerializeField]
	Transform startMove = null;
	[SerializeField]
	Transform endMove = null;
	[SerializeField]
	GameObject lazerStart = null;
	[SerializeField]
	GameObject lazerMiddle = null;
	[SerializeField]
	GameObject lazerEnd = null;

	// Use this for initialization
	void Start () {
		float rot = (transform.rotation.eulerAngles.y + 90.0f) * (Mathf.PI / 180.0f);
		direction = new Vector3 (-Mathf.Cos (rot), 0, Mathf.Sin (rot));

		if (!IsStationary)
			SetDestination (startMove);

		if (isEnabled) {
			int distance = 0;

			if (HasEndpoint) {
				distance = Mathf.CeilToInt (Vector3.Distance (transform.position, endPoint.position)) - 1;
			} else {
				RaycastHit hit;
				Vector3 fwd = transform.TransformDirection(Vector3.forward);
				Physics.Raycast(transform.position + direction, fwd, out hit, maxDistance);
				distance = Mathf.CeilToInt (hit.distance) + 1;

				if(distance < 0)
					distance = Mathf.CeilToInt(maxDistance);
			}

			for (int index = 0; index < distance; index++) {
				Vector3 pos = transform.position + direction * (1 + index);

				if (index == 0) {
					CreateLazer(lazerStart.transform, pos, transform.rotation);
				} else if (index == distance - 1 && HasEndpoint) {
					CreateLazer(lazerEnd.transform, pos, transform.rotation);
				} else {
					CreateLazer(lazerMiddle.transform, pos, transform.rotation);
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (!IsStationary && IsEnabled) {
			if(!HasEndpoint) {
				RaycastHit hit;
				Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, maxDistance);
				int distance = Mathf.CeilToInt (hit.distance) + 1;

				if (distance != lazers.Count) {
					int difference = 0;

					if (distance < lazers.Count) {
						difference = lazers.Count - distance;

						for (int index = lazers.Count; index == lazers.Count - difference; index--) {
							lazers.RemoveAt(index - 1);
						}
					} else {
						difference = distance - lazers.Count;

						for (int index = lazers.Count; index == lazers.Count + difference; index++) {
							Vector3 pos = transform.position + direction * (1 + index);

							CreateLazer(lazerMiddle.transform, pos, transform.rotation);
						}
					}
				}
			}
		}
	}

	void FixedUpdate() {
		if (!IsStationary && IsEnabled) {
			gameObject.GetComponent<Rigidbody> ().MovePosition (transform.position + moveDirection * speed * Time.fixedDeltaTime);

			if(Vector3.Distance (transform.position, target.position) < speed * Time.fixedDeltaTime){
				SetDestination(target == startMove ? endMove : startMove);
			}
		}
	}

	void ToggleLazers(bool toggle) {
		if (isEnabled != toggle) {
			isEnabled = toggle;

			if(toggle == true) {
				int distance = 0;

				if (HasEndpoint) {
					distance = Mathf.CeilToInt (Vector3.Distance (transform.position, endPoint.position)) - 1;
				} else {
					RaycastHit hit;
					Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, maxDistance);
					distance = Mathf.CeilToInt (hit.distance) + 1;

					if(distance <= 1)
						distance = Mathf.CeilToInt(maxDistance);
				}

				for (int index = 0; index < distance; index++) {
					Vector3 pos = transform.position + direction * (1 + index);
					
					if (index == 0) {
						CreateLazer(lazerStart.transform, pos, transform.rotation);
					} else if (index == distance - 1 && HasEndpoint) {
						CreateLazer(lazerEnd.transform, pos, transform.rotation);
					} else {
						CreateLazer(lazerMiddle.transform, pos, transform.rotation);
					}
				}
			} else {
				for(int index = lazers.Count; index == 0; index--) {
					Destroy(lazers[index - 1]);
				}

				lazers.Clear();
			}
		}
	}

	void CreateLazer(Transform Lazer, Vector3 Pos, Quaternion Rot) {
		GameObject newLazer = (Instantiate (Lazer, Pos, Rot) as Transform).gameObject;
		newLazer.transform.parent = transform;
		newLazer.GetComponent<Lazers> ().Color = color;
		lazers.Add (newLazer);
	}

	void SetDestination(Transform dest) {
		target = dest;
		moveDirection = (target.position - transform.position).normalized;
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.green;
		Gizmos.DrawWireCube(startMove.position, transform.localScale);
		
		Gizmos.color = Color.red;
		Gizmos.DrawWireCube(endMove.position, transform.localScale);
	}

	public bool IsEnabled {
		get { return isEnabled; }
		set { ToggleLazers (value); }
	}
}
