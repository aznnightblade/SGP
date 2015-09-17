using UnityEngine;
using System.Collections;

public class StartParticleSystem : MonoBehaviour {

	ParticleSystem system = null;

	// Use this for initialization
	void Start () {
		system = gameObject.GetComponent<ParticleSystem> ();
	}
	
	public void Activate () {
		system.Play ();
	}
}
