using UnityEngine;
using System.Collections;

public class Letter : MonoBehaviour {
    public GameObject letter;
    public Dampener[] dampeners;
	// Use this for initialization
	void Start () {
        letter.SetActive(false);
	}
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player" )
        {
            int count = 0;
            for (int i = 0; i < dampeners.Length; ++i )
            {
                if(dampeners[i].Toggle == true)
                    ++count;
            }
            if(count == dampeners.Length)
                letter.SetActive(true);
        }
    }

	// Update is called once per frame
	void Update () {
	
	}
}
