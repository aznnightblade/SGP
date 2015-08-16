using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static GameManager instance;
	private int credits;
	private int xp;
	void Awake(){ 

		instance = this;
	}

	public void AddXp(int _numXp){
		xp = xp + _numXp;
	}
	public void AddCredits(int _numCredits){
		credits = credits + _numCredits;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
