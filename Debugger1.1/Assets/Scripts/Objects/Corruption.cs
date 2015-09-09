using UnityEngine;
using System.Collections;

public class Corruption : Statistics {
    private SpriteRenderer corruption;
    void Start()
    {
        corruption = gameObject.GetComponentInChildren<SpriteRenderer>();
    }
	void Update(){
        float b = ((float)currHealth / maxHealth);

        corruption.material.color = new Color(1, 1, 1, b);
	}
}
