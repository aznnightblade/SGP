using UnityEngine;
using System.Collections;
using UnityEditor;

public class SaveData{

	public string Name;
	public int XP;
	public int Credits;
	public int Level;
	public int CurrentHealth;
	public int Strength;
	public int Intellagence;
	public int Luck;
	public int Agility;
	public int Endurance;
	public int Dexterity;
	public bool SucessfulLoad;

	public SaveData()
	{
		SucessfulLoad = false;
	}

	public static SaveData Load( string _name ){

		if (PlayerPrefs.HasKey (_name))
			return new SaveData ();
		SaveData data = new SaveData ();

		data.Name = PlayerPrefs.GetString (_name + "NAME");
		data.Level = PlayerPrefs.GetInt (_name + "LEVEL");
		data.Strength = PlayerPrefs.GetInt (_name + "STRENGTH");
		data.Intellagence = PlayerPrefs.GetInt (_name + "INTELLAGANCE");
		data.Luck = PlayerPrefs.GetInt (_name + "LUCK");
		data.Agility = PlayerPrefs.GetInt (_name + "AGILITY");
		data.Endurance = PlayerPrefs.GetInt (_name + "ENDURANCE");
		data.Dexterity = PlayerPrefs.GetInt (_name + "DEXTERITY");
		data.CurrentHealth= PlayerPrefs.GetInt (_name + "CURRENT_HEALTH");
		data.XP = PlayerPrefs.GetInt (_name + "XP");
		data.Credits = PlayerPrefs.GetInt (_name + "CREDITS");


		return data;
	}
	public static void  Save( string _name, SaveData _data){

		PlayerPrefs.SetString(_name,_name);
		PlayerPrefs.SetString (_name + "NAME", _data.Name);
		PlayerPrefs.SetInt (_name + "LEVEL", _data.Level);
		PlayerPrefs.SetInt (_name + "STRENGTH", _data.Strength);
		PlayerPrefs.SetInt (_name + "INTELLAGANCE", _data.Intellagence);
		PlayerPrefs.SetInt (_name + "LUCK", _data.Luck);
		PlayerPrefs.SetInt (_name + "AGILITY", _data.Agility);
		PlayerPrefs.SetInt (_name + "ENDURANCE", _data.Endurance);
		PlayerPrefs.SetInt (_name + "DEXTERITY", _data.Dexterity);
		PlayerPrefs.SetInt (_name + "CURRENT_HEALTH", _data.CurrentHealth);
		PlayerPrefs.SetInt (_name + "XP", _data.XP);
		PlayerPrefs.SetInt (_name + "CREDITS", _data.Credits);

	}

	[MenuItem("Tools/DeleteAll")]
	public static void DeleteAllPrefs(){
		PlayerPrefs.DeleteAll ();
	}
}
