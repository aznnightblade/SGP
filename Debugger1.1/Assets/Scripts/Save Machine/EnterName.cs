using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnterName : MonoBehaviour {

	public string saveName;
	SaveData data;
	public void SubmitSaveName() {
		switch (saveName) {
		case "Save 1":
		{
			GameManager.saveSpot1 = GetComponent<GUIText>().text;
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
			break;
		case "Save 2":
			GameManager.saveSpot2 = GetComponent<GUIText>().text;
			break;
		case "Save 3":
			GameManager.saveSpot3 = GetComponent<GUIText>().text;
			break;
		}
		//saveName = GetComponent<GUIText>().text;

		Application.LoadLevel("SaveGame");
	}
}
