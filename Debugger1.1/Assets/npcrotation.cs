using UnityEngine;
using System.Collections;

public class npcrotation : MonoBehaviour {

    Animator movement;
    NavMeshAgent agent;
	// Use this for initialization
	void Start () {
        movement = GetComponentInChildren<Animator>();
        agent = gameObject.GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.rotation.eulerAngles.y<180.0f)
        {
            movement.SetBool("RightAnimation", true);
        }
        else
        {
            movement.SetBool("RightAnimation", false);
        }
        
	}
}
