using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Dialogue : MonoBehaviour {
    bool triggerActive = false;
	// Use this for initialization
    public GameObject panel;
    public GameObject HUD;
    public Text dialogue;
    public TextAsset textfile;
    public string[] textlines;
    public int currentline;
    public int endline;

    public PlayerController player;
	// Update is called once per frame
    void Start()
    {

        player = FindObjectOfType<PlayerController>();
        if (textfile !=null)
        {
            textlines = (textfile.text.Split('\n'));
        }
       panel.SetActive(false);

       if (endline==0)
       {
           endline = textlines.Length-1;
       }
    }
    void Update()
    {

        if (triggerActive == true && Input.GetButtonDown("Submit"))
        {
            panel.SetActive(true);
            HUD.SetActive(false);
            dialogue.text = textlines[currentline];
            if (currentline==0)
            {
                currentline++;
            } 
            else
            {
                currentline += 1;
            }
        }

        if (currentline> endline)
        {
            panel.SetActive(false);
            HUD.SetActive(true);
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
            
        }
    }

}
