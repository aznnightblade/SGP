using UnityEngine;
using System.Collections;

public class SaveComputer : MonoBehaviour {

    SaveData data;
    bool triggerActive = false;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

        if (triggerActive == true && Input.GetButtonDown("Submit"))
        {
            data = new SaveData();
            data.Level = GameManager.indexLevel;
            data.Strength = FindObjectOfType<Player>().Strength;
            data.Intelligence =FindObjectOfType<Player>().Intelligence;
            data.Agility = FindObjectOfType<Player>().Agility;
            data.Dexterity = FindObjectOfType<Player>().Dexterity; 
            data.Luck = FindObjectOfType<Player>().Luck;
            data.Endurance = FindObjectOfType<Player>().Endurance;
            data.CurrentHealth = FindObjectOfType<Player>().CurrHealth;
            data.Credits = FindObjectOfType<Player>().Money;
            data.XP = FindObjectOfType<Player>().EXP;
            

            SaveData.Save(GameManager.saveSpot1, data);
            Debug.Log("Saved information");
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
