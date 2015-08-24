using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SelectSaveSlot : MonoBehaviour {

    SaveData data;
    public string NextScene;
	void Start() {

	}

	public void OnClick()
    {
        if (gameObject.tag=="Save1")
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
            data.MaxHealth = FindObjectOfType<Player>().MaxHealth;
            data.Credits = FindObjectOfType<Player>().Money;
            data.XP = FindObjectOfType<Player>().EXP;
            data.newGame = 0;
            SaveData.Save(GameManager.saveSpot1="test", data);
            GameManager.back = true;
            SaveData temp = SaveData.Load("test");
            GameManager.instance.LoadPlayerstatsScene(temp);
            GameManager.instance.NextScene();
            Application.LoadLevel("HubWorld");
           
            Debug.Log("Saved information");
        }
        else if (gameObject.tag == "Save2")
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
        }
        else if (gameObject.tag == "Save3")
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
        }

       
    }
}
