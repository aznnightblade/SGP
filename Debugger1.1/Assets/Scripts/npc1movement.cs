﻿using UnityEngine;
using System.Collections;

public class npc1movement : MonoBehaviour {
    public bool active = false;
    Random rnd = new Random();

    private NavMeshAgent agent = null;
    [SerializeField]
    Transform[] Waypoints = null;
    Vector3 destination = Vector3.zero;
	// Use this for initialization
	void Start () {

        agent = gameObject.GetComponent<NavMeshAgent>();
        agent.destination = Waypoints[Random.Range(0, Waypoints.Length)].transform.position;
	}
	
	// Update is called once per frame
    void Update()
    {
       if (agent.enabled==true && agent.remainingDistance <= 3)
       {
          agent.destination = Waypoints[Random.Range(0, Waypoints.Length)].transform.position;
       }
    }
}