using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class WeaponVendor : MonoBehaviour {
    bool triggerActive = false;
    public GameObject Panel;
    public GameObject Button;
    public GameObject Panel2;
    Player player;
    public Text costtext;
    public Text text;
    int _breakpoint;
    int _multithread;
    int _negation;
    public enum Weapons { Breakpoint, Multithread, NegationBoots };
       [SerializeField]
    Weapons choice = Weapons.Breakpoint;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        Weaponlevels();
	}
	
	// Update is called once per frameaw
	void Update () {
        Weaponlevels();
        if (triggerActive == true && !Panel.activeInHierarchy) {
			if (InputManager.instance.GetButtonDown ("Submit")) {
				Panel.SetActive (true);
				Button.SetActive (true);
				player.GetComponentInParent<PlayerController> ().enabled = false;
				player.GetComponentInParent<Rigidbody> ().velocity = Vector3.zero;
				player.GetComponentInParent<Rigidbody> ().freezeRotation = true;

				switch (choice) {
				case Weapons.Breakpoint:
					{
						if (GameManager.breakptlevel < 4) {
							costtext.text = "Upgrade Cost will be " + Mathf.FloorToInt (600 * _breakpoint).ToString () + " Credits";
						} else {
							costtext.text = "Breakpoint has max upgrades";
						}
                        
					}
					break;
				case Weapons.Multithread:
					{
						if (GameManager.multithreadlevel < 4) {
							costtext.text = "Upgrade Cost will be " + Mathf.FloorToInt (600 * _multithread).ToString () + " Credits";
						} else {
							costtext.text = "Multithread has max upgrades";
						}
                        
					}
					break;
				case Weapons.NegationBoots:
					{
						if (GameManager.negationbootlevel < 4) {
							costtext.text = "Upgrade Cost will be " + Mathf.FloorToInt (600 * _negation).ToString () + " Credits";
						} else {
							costtext.text = "NegationBoots has max upgrades";
						}
					}
					break;
               
				}
			}
		} else if (Panel.activeInHierarchy) {
			if (InputManager.instance.GetButtonDown ("Cancel")) {
				ExitMenu ();
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
        Panel2.SetActive(true);
        if (col.tag == "Player")
        {
            triggerActive = false;
        }
    }


    public void Onclick()
    {
        Panel2.SetActive(false);
        SoundManager.instance.MiscSoundeffects[8].Play();
        Panel.SetActive(false);
        Button.SetActive(false);
        if (gameObject.name=="Breakpoint")
        {
            if (GameManager.breakptlevel==1 && player.Money >= 600)
            {
                player.Money -= 600;
                GameManager.breakpointduration = player.Breakpoint.Duration = 3.25f;
                GameManager.breakptlevel = 2;
                text.text = "Breakpoint has increased duration!";
            }
            else if (GameManager.breakptlevel == 2 && player.Money >= 1200)
            {
                player.Money -= 1200;
                GameManager.breakpointduration = player.Breakpoint.Duration = 3.50f;
                GameManager.breakptlevel = 3;
                text.text = "Breakpoint has increased duration!";
            }
            else if (GameManager.breakptlevel == 3 && player.Money >= 1800)
            {
                player.Money -= 1800;
                GameManager.breakpointduration = player.Breakpoint.Duration = 3.75f;
                GameManager.breakptlevel = 4;
                text.text = "Breakpoint has increased duration!";
            }
            else
            {
                if (GameManager.breakptlevel > 3)
                {
                    text.text = "Breakpoint has reached its maximum capacity";
                }
                else
                {
                    text.text = "Not enough credits to purchase this upgrade";
                }
            }
        }
        if (gameObject.name == "Multithread")
        {
            if (GameManager.multithreadlevel == 1 && player.Money >= 600)
            {
                player.Money -= 600;
                
                GameManager.data.Multithread = GameManager.multithreadlevel = player.MultithreadLevel = 2;
                text.text = "Multithread can fire 2 shots at once!";
            }
			else if (GameManager.multithreadlevel == 2 && player.Money >= 1200)
            {
                player.Money -= 1200;
                GameManager.data.Multithread = GameManager.multithreadlevel = player.MultithreadLevel = 3;
                text.text = "Multithread can fire 3 shots at once!";
            }
			else if (GameManager.multithreadlevel == 3 && player.Money >= 1800)
            {
                player.Money -= 1800;
                GameManager.data.Multithread = GameManager.multithreadlevel = player.MultithreadLevel = 4;
                text.text = "Multithread can fire 4 shots at once!";
            }
            else
            {
                if (GameManager.multithreadlevel > 3)
                {
                    text.text = "Multithread has reached its maximum capacity";
                }
                else
                {
                    text.text = "Not enough credits to purchase this upgrade";
                }
            }
        }
        if (gameObject.name == "NegationBoots")
        {
            if (GameManager.negationbootlevel == 1 && player.Money >= 600)
            {
                player.Money -= 600;

                GameManager.negationbootlevel++;
                text.text = "Multithread can fire 2 shots at once!";
            }
            else if (GameManager.negationbootlevel == 2 && player.Money >= 1200)
            {
                player.Money -= 1200;
                //player.MultithreadLevel = 3;
                GameManager.negationbootlevel++;
                text.text = "Multithread can fire 3 shots at once!";
            }
            else if (GameManager.negationbootlevel == 3 && player.Money >= 1800)
            {
                player.Money -= 1800;
                
                GameManager.negationbootlevel++;
                text.text = "Multithread can fire 4 shots at once!";
            }
            else
            {
                if (GameManager.negationbootlevel > 3)
                {
                    text.text = "Multithread has reached its maximum capacity";
                }
                else
                {
                    text.text = "Not enough credits to purchase this upgrade";
                }
            }
        }

        player.GetComponentInParent<PlayerController>().enabled = true;
    }

	public void ExitMenu () {
		Panel.SetActive(false);
		Button.SetActive(false);
		player.GetComponentInParent<PlayerController>().enabled = true;
	}

	void Weaponlevels()
    {
        _breakpoint = GameManager.breakptlevel;
        _multithread = GameManager.multithreadlevel;
		_negation = GameManager.negationbootlevel;
    }
}
