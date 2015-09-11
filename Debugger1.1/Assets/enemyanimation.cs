using UnityEngine;
using System.Collections;

public class enemyanimation : MonoBehaviour {
    Enemy enemy = null;
	// Use this for initialization
	void Start () {
        enemy = transform.parent.gameObject.GetComponent<Enemy>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Attack()
    {
        enemy.Attack();
    }
}
