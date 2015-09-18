using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Dialogue : MonoBehaviour {
    bool triggerActive = false;
	// Use this for initialization
    private NavMeshAgent agent = null;
    public GameObject panel;
    public GameObject HUD;
    public Text dialogue;
    public TextAsset[] textfile;
    public string[] textlines;
    public int currentline;
    public int endline;
   public int current_level = 0;
	public int lineLevel = 0;
	public bool inTutor = false;
    public bool isActive;
    private bool isTyping = false;
    private bool canceltyping = false;
    public float TypeSpeed;
	// Update is called once per frame
    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        
       panel.SetActive(false);

    }
    void Update()
    {
        if (textfile != null)
        {
            textlines = (textfile[current_level].text.Split('\n'));
        }
        if (endline == 0)
        {
            endline = textlines.Length - 1;
        }
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
		if (!inTutor) {
			switch (GameManager.indexLevel) {
			case 0:
				{
					current_level = 0;
				}
				break;
			case 1:
				{
					current_level = 1;
				}
				break;
			case 2:
				{
					current_level = 2;
				}
				break;
			case 3:
				{
					current_level = 3;
				}
				break;
			case 4:
				{
					current_level = 4;
				}
				break;
			case 5:
				{
					current_level = 5;
				}
				break;
			case 6:
				{
					current_level = 6;
				}
				break;
			case 7:
				{
					current_level = 7;
				}
				break;
			default:
				break;
			}
		} else { 
			switch (lineLevel)
			{
			case 0:
			{
				current_level = 0;
			}
				break;
			case 1:
			{
				current_level = 1;
			}
				break;
			case 2:
			{
				current_level = 2;
			}
				break;
			case 3:
			{
				current_level = 3;
			}
				break;
			case 4:
			{
				current_level = 4;
			}
				break;
			case 5:
			{
				current_level = 5;
			}
				break;
			case 6:
			{
				current_level = 6;
			}
				break;
			case 7:
			{
				current_level = 7;
			}
				break;
			default:
				break;
			}
		}
        if (triggerActive == true && InputManager.instance.GetButtonDown("Submit"))
        {
           
            player.GetComponentInParent<PlayerController>().enabled = false;
            player.GetComponentInParent<Rigidbody>().velocity = Vector3.zero;
            player.GetComponentInParent<Rigidbody>().freezeRotation = true;
			Time.timeScale = 0;
            agent.enabled = false;
            panel.SetActive(true);
            HUD.SetActive(false);
            if (!isTyping)
            {
                currentline += 1;

                if (currentline > endline)
                {
                    panel.SetActive(false);
                    HUD.SetActive(true);
                    player.GetComponentInParent<PlayerController>().enabled = true;
                   // player.GetComponentInParent<Rigidbody>().freezeRotation = false;
                    currentline = 0;
					Time.timeScale = 1;
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
            agent.enabled = true;
        }
    }

}
