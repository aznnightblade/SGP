using UnityEngine;
using System.Collections;

public class NPCanimationscript : MonoBehaviour {
    Animator interaction;
	// Use this for initialization
	void Start () {
        interaction = GetComponentInChildren<Animator>(); 
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            interaction.SetBool("Talking", true);
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            interaction.SetBool("Talking", false);
        }
    }

}
