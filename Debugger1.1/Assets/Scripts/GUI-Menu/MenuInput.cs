using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class MenuInput : MonoBehaviour {

    StandaloneInputModule input = null;

	// Use this for initialization
	void Start () {
        input = gameObject.GetComponent<StandaloneInputModule>();

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
