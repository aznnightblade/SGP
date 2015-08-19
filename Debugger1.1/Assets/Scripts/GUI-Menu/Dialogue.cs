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

    public bool isActive;
    private bool isTyping = false;
    private bool canceltyping = false;
    public float TypeSpeed;
   // public PlayerController player;
	// Update is called once per frame
    void Start()
    {
        if (textfile != null)
        {
            textlines = (textfile.text.Split('\n'));
        }
        if (endline == 0)
        {
            endline = textlines.Length - 1;
        }
       panel.SetActive(false);

    }
    void Update()
    {

        if (triggerActive == true && Input.GetButtonDown("Submit"))
        {
            panel.SetActive(true);
            HUD.SetActive(false);
            if (!isTyping)
            {
                currentline += 1;

                if (currentline > endline)
                {

                    panel.SetActive(false);
                    HUD.SetActive(true);
                    currentline = 0;
                }
                else
                {
                    StartCoroutine(Textscroll(textlines[currentline]));
                }
            }
            else if (isTyping && !canceltyping)
            {
                canceltyping = true;
            }
           // dialogue.text = textlines[currentline];

        }

       
       
    }

    private IEnumerator Textscroll (string lineoftext)
    {
        int letter = 0;
        isTyping = true;
        canceltyping = false;
        dialogue.text = "";
        while (isTyping && !canceltyping && (letter < lineoftext.Length-1))
        {
            dialogue.text += lineoftext[letter];
            letter+=1;
            yield return new WaitForSeconds(TypeSpeed);
        }
        dialogue.text = lineoftext;
        isTyping = false;
        canceltyping = false;
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
