using UnityEngine;
using System.Collections;

public class Friends : MonoBehaviour {
	public GameObject tutor;
	public GameObject player;
	public GameObject bugtype;
	public GameObject bug;
	Vector3 position;
    public int choice = 0;
	DLLColor.Color color;
	Enemy.Mode mode;
	public bool once = false;
	// Use this for initialization
	void Start () {
	
		position = bug.transform.position;
		switch(choice)
        {
            case 0:
                {
                    color = DLLColor.Color.NEUTRAL;
                    break;
                }
            case 1:
                {
                    color = DLLColor.Color.RED;
                    break;
                }
            case 2:
                {
                    color = DLLColor.Color.GREEN;
                    break;
                }
            case 3:
                {
                    color = DLLColor.Color.BLUE;
                    break;
                }
           
        }
        bug.GetComponentInChildren<Enemy>().CurrColor = color;
		mode = bug.GetComponentInChildren<Enemy> ().CurrMode;
	}
	// Update is called once per frame
	void Update () {

		if (player.GetComponentInChildren<Player> ()!= null) {
			if (player.GetComponentInChildren<Player> ().Friend != null && once)
				tutor.GetComponent<Dialogue> ().lineLevel = 1;
	
			if (bug == null && player.GetComponentInChildren<Player> ().Friend != bug) {
				bug = (GameObject)Instantiate (bugtype, position, Quaternion.identity);
				bug.GetComponentInChildren<Enemy> ().CurrMode = mode;
				bug.GetComponentInChildren<Enemy> ().Money = 0;
				bug.GetComponentInChildren<Enemy> ().EXP = 0;
				bug.GetComponentInChildren<Enemy> ().CurrColor = color;
			}
	
		}
	}
}
