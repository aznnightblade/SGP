using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class SaveComputer : MonoBehaviour {

    SaveData data;
    bool triggerActive = false;
    public GameObject Panel;
    public GameObject Save1;
    public GameObject Save2;
    public GameObject Save3;
    Transform player;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {

        if (triggerActive == true && Input.GetButtonDown("Submit"))
        {
            Panel.SetActive(true);
            Save1.SetActive(true);
            Save2.SetActive(true);
            Save3.SetActive(true);
            player.GetComponentInParent<PlayerController>().enabled = false;
            player.GetComponentInParent<Rigidbody>().velocity = Vector3.zero;
            player.GetComponentInParent<Rigidbody>().freezeRotation = true;
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

    public void Onclick()
    {
        if (gameObject.tag=="Save1")
        {
            data = new SaveData();
            data.Level = GameManager.indexLevel;
            data.Strength = FindObjectOfType<Player>().Strength;
            data.Intelligence = FindObjectOfType<Player>().Intelligence;
            data.Agility = FindObjectOfType<Player>().Agility;
            data.Dexterity = FindObjectOfType<Player>().Dexterity;
            data.Luck = FindObjectOfType<Player>().Luck;
            data.Endurance = FindObjectOfType<Player>().Endurance;
            data.CurrentHealth = FindObjectOfType<Player>().CurrHealth;
            data.MaxHealth = FindObjectOfType<Player>().MaxHealth;
            data.Credits = FindObjectOfType<Player>().Money;
            data.XP = FindObjectOfType<Player>().EXP;
            data.newGame = 0;

            SaveData.Save(GameManager.saveSpot1 = "test", data);
            Debug.Log("Saved information");
            Panel.SetActive(false);
            Save1.SetActive(false);
            Save2.SetActive(false);
            Save3.SetActive(false);
            player.GetComponentInParent<PlayerController>().enabled = true;
        }
        if (gameObject.tag == "Save2")
        {
            data = new SaveData();
            data.Level = GameManager.indexLevel;
            data.Strength = FindObjectOfType<Player>().Strength;
            data.Intelligence = FindObjectOfType<Player>().Intelligence;
            data.Agility = FindObjectOfType<Player>().Agility;
            data.Dexterity = FindObjectOfType<Player>().Dexterity;
            data.Luck = FindObjectOfType<Player>().Luck;
            data.Endurance = FindObjectOfType<Player>().Endurance;
            data.CurrentHealth = FindObjectOfType<Player>().CurrHealth;
            data.MaxHealth = FindObjectOfType<Player>().MaxHealth;
            data.Credits = FindObjectOfType<Player>().Money;
            data.XP = FindObjectOfType<Player>().EXP;
            data.newGame = 0;

            SaveData.Save(GameManager.saveSpot2 = "test2", data);
            Debug.Log("Saved information");
            Panel.SetActive(false);
            Save1.SetActive(false);
            Save2.SetActive(false);
            Save3.SetActive(false);
            player.GetComponentInParent<PlayerController>().enabled = true;
        }
        if (gameObject.tag == "Save3")
        {
            data = new SaveData();
            data.Level = GameManager.indexLevel;
            data.Strength = FindObjectOfType<Player>().Strength;
            data.Intelligence = FindObjectOfType<Player>().Intelligence;
            data.Agility = FindObjectOfType<Player>().Agility;
            data.Dexterity = FindObjectOfType<Player>().Dexterity;
            data.Luck = FindObjectOfType<Player>().Luck;
            data.Endurance = FindObjectOfType<Player>().Endurance;
            data.CurrentHealth = FindObjectOfType<Player>().CurrHealth;
            data.MaxHealth = FindObjectOfType<Player>().MaxHealth;
            data.Credits = FindObjectOfType<Player>().Money;
            data.XP = FindObjectOfType<Player>().EXP;
            data.newGame = 0;

            SaveData.Save(GameManager.saveSpot3 = "test3", data);
            Debug.Log("Saved information");
            Panel.SetActive(false);
            Save1.SetActive(false);
            Save2.SetActive(false);
            Save3.SetActive(false);
            player.GetComponentInParent<PlayerController>().enabled = true;
        }
    }
}
