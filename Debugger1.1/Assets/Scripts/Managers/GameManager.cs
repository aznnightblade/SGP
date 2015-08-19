using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	public static GameManager instance;
	public static int indexLevel = 0;
	public static SaveData playersGame;
	public static  string saveSpot;
	 static bool first = true;


	void Awake(){
		if (instance==null)
		{
			instance = this;
		}
		else if (instance != this)
		{
			Destroy(gameObject);
		}
		DontDestroyOnLoad(gameObject);
		if (first) {
			playersGame = new SaveData ();
			playersGame.Level = 0;
			playersGame.Strength = 1;
			playersGame.Intelligence = 1;
			playersGame.Agility = 14;
			playersGame.Dexterity = 1;
			playersGame.Luck = 1;
			playersGame.Endurance = 1;
			playersGame.CurrentHealth = 1;
			playersGame.Credits = 100;
			playersGame.XP = 0;
			first = true;
		}
	
		FindObjectOfType<Player>().Agility = playersGame.Agility; 
		FindObjectOfType<Player>().Strength = playersGame.Strength; 
		FindObjectOfType<Player>().Endurance = playersGame.Endurance; 
		FindObjectOfType<Player>().Luck = playersGame.Luck; 
		FindObjectOfType<Player>().Intelligence = playersGame.Intelligence; 
		FindObjectOfType<Player>().Dexterity = playersGame.Dexterity;
		FindObjectOfType<Player>().CurrHealth = playersGame.CurrentHealth;
		FindObjectOfType<Player> ().Money = playersGame.Credits;
		FindObjectOfType<Player> ().Endurance= playersGame.XP;


	}
	public static void levelComplete(int _index)
	{
		indexLevel = _index;
	}
}
