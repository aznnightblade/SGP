using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class InfoBoard : MonoBehaviour {

    public GameObject Board;
    public GameObject HUD;
    public Text info;
    private bool triggerActive=false;
    int roomnumber;

	// Use this for initialization
	void Start () {
        Board.SetActive(false);
        switch (GameManager.indexLevel)
        {
            case 0:
                {
                    roomnumber = 0;
                }
                break;
            case 1:
                {
                    roomnumber = 101;
                }
                break;
            case 2:
                {
                    roomnumber = 102;
                }
                break;
            case 3:
                {
                    roomnumber = 103;
                }
                break;
            case 4:
                {
                    roomnumber = 104;
                }
                break;
            case 5:
                {
                    roomnumber = 105;
                }
                break;
            case 6:
                {
                    roomnumber = 106;
                }
                break;
            case 7:
                {
                    roomnumber = 107;
                }
                break;
            default:
                break;
        }
	}
	
	// Update is called once per frame
	void Update () {

        if (triggerActive == true && Input.GetButtonDown("Submit"))
        {
            Board.SetActive(true);
            HUD.SetActive(false);

            if (roomnumber!= 0)
            {
                info.text = "You need to go to Room # " + roomnumber;
            }
            else
            {
                info.text = "You need to go to the Tutorial Stage next to this Board!";
            }
        }



	}

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            triggerActive = true;
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            triggerActive = false;
            HUD.SetActive(true);
        }
    }
}
