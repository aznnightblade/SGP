using UnityEngine;
using System.Collections;

public class Corruption : Statistics {
    private Renderer corruption;
    void Start()
    {
        corruption = GetComponentInParent<Renderer>();
    }
	void Update(){

	}
    public void LessAlpha()
    {
        float b = (float)(currHealth/maxHealth);
        corruption.material.color = new Color(0, 0, 0, b);
    }
}
