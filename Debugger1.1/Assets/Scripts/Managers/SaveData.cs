using UnityEngine;
using System.Collections;
//using UnityEditor;

public class SaveData {

	public string Name;
	public int XP = 0;
	public int Credits = 0;
	public int Level = 0;
	public int CurrentHealth = 0;
	public int Strength = 0;
	public int Intelligence = 0;
	public int Luck = 0;
	public int Agility = 0;
	public int Endurance = 0;
	public int Dexterity= 0;
	public bool SucessfulLoad;
	public int newGame = 1;
	public int MaxHealth = 0;
    public int ChargeShot = 0;
    public int DLLShot = 0;
    public int Waveshot = 0;
    public int Friendwpn = 0;
    public int Multithread = 0;
    public float breakpointduration = 0;
    public int breakpointlevel = 1;
    public int NegationBoots = 0;
	public Player.COMPANIONS Companion = Player.COMPANIONS.None;
	public int[] companions = new int[5];

	public SaveData()
	{
		SucessfulLoad = false;
	}


	public static SaveData Load( string _name ) {

		if (PlayerPrefs.HasKey (_name))
			return new SaveData ();
		SaveData data = new SaveData ();

		data.Name = PlayerPrefs.GetString (_name + "NAME");
		
        data.Level = PlayerPrefs.GetInt (_name + "LEVEL");
        GameManager.indexLevel = data.Level;
		data.Strength = PlayerPrefs.GetInt (_name + "STRENGTH");
		data.Intelligence = PlayerPrefs.GetInt (_name + "INTELLIGENCE");
		data.Luck = PlayerPrefs.GetInt (_name + "LUCK");
		data.Agility = PlayerPrefs.GetInt (_name + "AGILITY");
		data.Endurance = PlayerPrefs.GetInt (_name + "ENDURANCE");
		data.Dexterity = PlayerPrefs.GetInt (_name + "DEXTERITY");
		data.CurrentHealth= PlayerPrefs.GetInt (_name + "CURRENT_HEALTH");
        data.MaxHealth = PlayerPrefs.GetInt(_name + "MAX_HEALTH");
		data.XP = PlayerPrefs.GetInt (_name + "XP");
		data.Credits = PlayerPrefs.GetInt (_name + "CREDITS");
        data.newGame = PlayerPrefs.GetInt(_name + "NEWGAME");
        data.ChargeShot = PlayerPrefs.GetInt(_name + "CHARGESHOT");
        data.DLLShot = PlayerPrefs.GetInt(_name + "DLLSHOT");
        data.Friendwpn = PlayerPrefs.GetInt(_name + "FriendShot");
        data.Waveshot = PlayerPrefs.GetInt(_name + "Recursive");
        data.Multithread = PlayerPrefs.GetInt(_name + "Multithreadlevel");
        data.breakpointduration = PlayerPrefs.GetFloat(_name + "Breakpoint");
        data.breakpointlevel = PlayerPrefs.GetInt(_name + "BreakpointLevel");
        data.NegationBoots = PlayerPrefs.GetInt(_name + "NegationBoots");
		data.Companion = (Player.COMPANIONS)PlayerPrefs.GetInt (_name + "CurrentCompanion");
      
		for (int index = 0; index < data.companions.Length; index++)
			data.companions [index] = PlayerPrefs.GetInt (_name + "Companion" + index);

		return data;
	}
	public static void  Save( string _name, SaveData _data){
        string temp = _name+"Name";
     
		PlayerPrefs.SetString(temp,_name);
        PlayerPrefs.SetString(temp, _data.Name);
        temp = _name + "LEVEL";
        PlayerPrefs.SetInt(temp, _data.Level);
        temp = _name + "STRENGTH";
		PlayerPrefs.SetInt (temp, _data.Strength);
        temp = _name + "INTELLIGENCE";
		PlayerPrefs.SetInt (temp, _data.Intelligence);
        temp = _name + "LUCK";
		PlayerPrefs.SetInt (temp, _data.Luck);
        temp = _name + "AGILITY";
		PlayerPrefs.SetInt (temp, _data.Agility);
        temp = _name + "ENDURANCE";
        PlayerPrefs.SetInt(temp, _data.Endurance);
        temp = _name + "DEXTERITY";
        PlayerPrefs.SetInt(temp, _data.Dexterity);
        temp = _name + "CURRENT_HEALTH";
        PlayerPrefs.SetInt(temp, _data.CurrentHealth);
        temp = _name + "MAX_HEALTH";
        PlayerPrefs.SetInt(temp, _data.MaxHealth);
        temp = _name + "XP";
        PlayerPrefs.SetInt(temp, _data.XP);
        temp = _name + "CREDITS";
        PlayerPrefs.SetInt(temp, _data.Credits);
        temp = _name + "NEWGAME";
        PlayerPrefs.SetInt(temp, _data.newGame);
        temp = _name + "CHARGESHOT";
        PlayerPrefs.SetInt(temp, _data.ChargeShot);
        temp = _name + "DLLSHOT";
        PlayerPrefs.SetInt(temp, _data.DLLShot);
        temp = _name + "FriendShot";
        PlayerPrefs.SetInt(temp, _data.Friendwpn);
        temp = _name + "Recursive";
        PlayerPrefs.SetInt(temp, _data.Waveshot);
        temp = _name + "NegationBoots";
        PlayerPrefs.SetInt(temp, _data.NegationBoots);
        temp = _name + "Multithreadlevel";
        PlayerPrefs.SetInt(temp, GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().MultithreadLevel);
        temp = _name + "Breakpoint";
        PlayerPrefs.SetFloat(temp, GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().Breakpoint.Duration);
        temp = _name + "Breakpointlevel";
        PlayerPrefs.SetInt(temp, GameManager.breakptlevel);
		temp = _name + "CurrentCompanion";
		PlayerPrefs.SetInt (temp, (int)_data.Companion);

		for (int index = 0; index < _data.companions.Length; index++) {
			temp = _name + "Companion" + index;
			PlayerPrefs.SetInt (temp, _data.companions[index]);
		}
	}

	//[MenuItem("Tools/DeleteAll")]
	public static void DeleteAllPrefs(){
		PlayerPrefs.DeleteAll ();
	}
}
