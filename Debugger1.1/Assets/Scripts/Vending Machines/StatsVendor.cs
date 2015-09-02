using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class StatsVendor : MonoBehaviour {

    
    bool triggerActive = false;
    public GameObject Panel;
    public GameObject Button;
    public GameObject Panel2;
    public Text text;
    public Text costtext;
    int _upgradecost;
    int _currentstatlvl;
    Player player;
    int currstrength;
    int currdex;
    int currint;
    int currendurance;
    int curragility;
    int currluck;
  
    
    enum Stats { Strength, Dexterity, Intelligence, Endurance, Agility, Luck };
    [SerializeField]
    Stats something= Stats.Strength;
    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        GetStats();
    }

    // Update is called once per frame
    void Update()
    {
        GetStats();
        if (triggerActive == true && InputManager.instance.GetButtonDown("Submit"))
        {
            Panel.SetActive(true);
            Button.SetActive(true);
            player.GetComponentInParent<PlayerController>().enabled = false;
            player.GetComponentInParent<Rigidbody>().velocity = Vector3.zero;
            player.GetComponentInParent<Rigidbody>().freezeRotation = true;
            switch (something)
            {
                case Stats.Strength:
                    {
                        costtext.text = "Upgrade Cost will be " + Mathf.FloorToInt(currstrength * Mathf.Pow(10.0f, currstrength)).ToString() + " EXP";
                    }
                    break;
                case Stats.Dexterity:
                    {
                        costtext.text = "Upgrade Cost will be " + Mathf.FloorToInt(currdex * Mathf.Pow(10.0f, currdex)).ToString() + " EXP";
                    }
                    break;
                case Stats.Endurance:
                    {
                        costtext.text = "Upgrade Cost will be " + Mathf.FloorToInt(currendurance * Mathf.Pow(10.0f, currendurance)).ToString() + " EXP";
                    }
                    break;
                case Stats.Agility:
                    {
                        costtext.text = "Upgrade Cost will be " + Mathf.FloorToInt(curragility * Mathf.Pow(10.0f, curragility)).ToString() + " EXP";
                    }
                    break;
                case Stats.Intelligence:
                    {
                        costtext.text = "Upgrade Cost will be " + Mathf.FloorToInt(currint * Mathf.Pow(10.0f, currint)).ToString() + " EXP";
                    }
                    break;
                case Stats.Luck:
                    {
                        costtext.text = "Upgrade Cost will be " + Mathf.FloorToInt(currluck * Mathf.Pow(10.0f, currluck)).ToString() + " EXP";
                    }
                    break;
                
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
        if (gameObject.name=="Strength")
        {
            _currentstatlvl = player.Strength;
            if (UpgradeCost())
            {
                player.EXP -= _upgradecost;
                player.Strength += 1;
                text.text = "Your Strength has improved by 1";
                
            }
            else
            {
                text.text = "Not enough experience to upgrade";
            }
            
            
        }
        if (gameObject.name == "Dexterity")
        {
            _currentstatlvl = player.Dexterity;
            if (UpgradeCost())
            {
                player.EXP -= _upgradecost;
                player.Dexterity += 1;
                text.text = "Your Dexterity has improved by 1";

            }
            else
            {
                text.text = "Not enough experience to upgrade";
            }
        }
        if (gameObject.name == "Endurance")
        {
            _currentstatlvl = player.Endurance;
            if (UpgradeCost())
            {
                player.EXP -= _upgradecost;
                player.Endurance += 1;
                text.text = "Your Endurance has improved by 1";

            }
            else
            {
                text.text = "Not enough experience to upgrade";
            }
        }
        if (gameObject.name == "Agility")
        {
            _currentstatlvl = player.Agility;
            if (UpgradeCost())
            {
                player.EXP -= _upgradecost;
                player.Agility += 1;
                text.text = "Your Agility has improved by 1";

            }
            else
            {
                text.text = "Not enough experience to upgrade";
            }
        }
        if (gameObject.name == "Intelligence")
        {
            _currentstatlvl = player.Intelligence;
            if (UpgradeCost())
            {
                player.EXP -= _upgradecost;
                player.Intelligence += 1;
                text.text = "Your Intelligence has improved by 1";

            }
            else
            {
                text.text = "Not enough experience to upgrade";
            }
        }
        if (gameObject.name == "Luck")
        {
            _currentstatlvl = player.Luck;
            if (UpgradeCost())
            {
                player.EXP -= _upgradecost;
                player.Luck += 1;
                text.text = "Your Luck has improved by 1";

            }
            else
            {
                text.text = "Not enough experience to upgrade";
            }
        }
        player.UpdateStats();
        player.GetComponentInParent<PlayerController>().enabled = true;
    }


    bool UpgradeCost()
    {
        _upgradecost = Mathf.FloorToInt(_currentstatlvl * Mathf.Pow(10.0f,_currentstatlvl));

        if (player.EXP >= _upgradecost)
        {
            return true;
        }
        else
            return false;
        
    }

   void GetStats()
    {
        currstrength = player.Strength;
        currdex = player.Dexterity;
        currendurance = player.Endurance;
        curragility = player.Agility;
        currint = player.Intelligence;
        currluck = player.Luck;

    }
}