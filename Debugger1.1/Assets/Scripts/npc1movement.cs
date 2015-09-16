using UnityEngine;
using System.Collections;

public class npc1movement : MonoBehaviour {

    private NavMeshAgent agent = null;
    [SerializeField]
    Transform[] Waypoints = null;
	// Use this for initialization
	void Start () {

        agent = gameObject.GetComponent<NavMeshAgent>();
        agent.destination = Waypoints[Random.Range(0, Waypoints.Length)].transform.position;
	}
	
	// Update is called once per frame
    void Update()
    {
        if (GameManager.CTimeScale == 0.0f)
        {
            agent.velocity = Vector3.zero;
            agent.enabled = false;
        }

        if (GameManager.CTimeScale > 0.0f && !agent.updateRotation)
        {
            agent.enabled = true;
        }
       if (agent.enabled==true && agent.remainingDistance <= 3)
       {
          agent.destination = Waypoints[Random.Range(0, Waypoints.Length)].transform.position;
       }
      
    }
}
