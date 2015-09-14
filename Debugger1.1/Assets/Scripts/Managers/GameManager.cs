using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	public static GameManager instance;
	public static int difficulty = 1;
	public static string nextlevelname;
	public static int indexLevel = 0;
	public  static Player player;
	public static bool back = false;
	public  static string saveSpot1;
	public  static string saveSpot2;
	public  static string saveSpot3;
	public bool BBool;
	public  static SaveData data;
	static float cTimeScale = 1.0f;
	static float cTimeScale2 = 1.0f;
	static public Vector3 lastPosition = new Vector3(0.0f, 0.0f, 0.0f);
	static bool first = true;
	public static bool quit = false;
	public static bool loadfirst = true;
    public static int Chargeshot = 0;
    public static int DLLShot = 0;
    public static int Friendshot = 0;
    public static int RecursiveShot = 0;
    public static int breakptlevel = 1;
    public static int negationbootlevel = 1;
    public static int multithreadlevel = 1;
	public static Vector2 ScreenResolution = new Vector2 (Screen.currentResolution.width, Screen.currentResolution.height);
    public static bool deletefile = false;
	void Awake(){
		if (instance == null) {
			instance = this;

			DontDestroyOnLoad (gameObject);

			if (FindObjectOfType<Player> ().newGame == 1 && first) {
				FindObjectOfType<Player> ().Agility = 1;
				FindObjectOfType<Player> ().Strength = 1;
				FindObjectOfType<Player> ().Endurance = 1;
				FindObjectOfType<Player> ().Luck = 1;
				FindObjectOfType<Player> ().Intelligence = 1;  
				FindObjectOfType<Player> ().Dexterity = 1;
				FindObjectOfType<Player> ().CurrHealth = 50;
				FindObjectOfType<Player> ().MaxHealth = 50;
				FindObjectOfType<Player> ().Money = 100;
				FindObjectOfType<Player> ().EXP = 0;
				FindObjectOfType<Player> ().newGame = 0;
	            Chargeshot = 0;
	            DLLShot = 0;
				first = false;
			} 

			if(back)
			{
			    GameObject.FindGameObjectWithTag("Player Controller").GetComponent<Rigidbody> ().position = lastPosition;
			}
			
		} else if (instance != this) {
			Destroy (gameObject);
		}
	}

	public void NextScene()
	{
		data = new SaveData();
		data.Agility = FindObjectOfType<Player> ().Agility;
		data.Strength = FindObjectOfType<Player> ().Strength;
		data.Endurance = FindObjectOfType<Player> ().Endurance;
		data.Luck = FindObjectOfType<Player> ().Luck;
		data.Intelligence = FindObjectOfType<Player> ().Intelligence;
		data.Dexterity = FindObjectOfType<Player> ().Dexterity;
		data.CurrentHealth = FindObjectOfType<Player> ().CurrHealth;
		data.MaxHealth = FindObjectOfType<Player> ().MaxHealth;
		data.Credits = FindObjectOfType<Player> ().Money;
		data.XP = FindObjectOfType<Player> ().EXP;
		data.newGame = FindObjectOfType<Player> ().newGame;
        data.ChargeShot = Chargeshot;
        data.DLLShot = DLLShot;
        data.Friendwpn = Friendshot;
        data.Waveshot = RecursiveShot;
	}
	 public static void LoadScene()
	{
       
		if (loadfirst == true) {
			loadfirst = false;
			return;
		}
		if (back)
			back = false;
		FindObjectOfType<Player> ().Agility = data.Agility ;
		FindObjectOfType<Player> ().Strength = data.Strength;
		FindObjectOfType<Player> ().Endurance = data.Endurance;
		FindObjectOfType<Player> ().Luck = data.Endurance;
		FindObjectOfType<Player> ().Intelligence = data.Intelligence;
		FindObjectOfType<Player> ().Dexterity = data.Dexterity;
		FindObjectOfType<Player> ().CurrHealth = data.CurrentHealth;
		FindObjectOfType<Player> ().MaxHealth = data.MaxHealth;
		FindObjectOfType<Player> ().Money = data.Credits;
		FindObjectOfType<Player> ().EXP = data.XP;
		FindObjectOfType<Player> ().newGame = data.newGame;
        DLLShot = data.DLLShot;
        Chargeshot = data.ChargeShot;
	}
     public void LoadPlayerstatsScene(SaveData _data)
     {

         FindObjectOfType<Player>().Agility = _data.Agility;
         FindObjectOfType<Player>().Strength = _data.Strength;
         FindObjectOfType<Player>().Endurance = _data.Endurance;
         FindObjectOfType<Player>().Luck = _data.Endurance;
         FindObjectOfType<Player>().Intelligence = _data.Intelligence;
         FindObjectOfType<Player>().Dexterity = _data.Dexterity;
         FindObjectOfType<Player>().CurrHealth = _data.CurrentHealth;
         FindObjectOfType<Player>().MaxHealth = _data.MaxHealth;
         FindObjectOfType<Player>().Money = _data.Credits;
         FindObjectOfType<Player>().EXP = _data.XP;
         FindObjectOfType<Player>().newGame = _data.newGame;
         DLLShot = _data.DLLShot;
         Chargeshot = _data.ChargeShot;

     }
	public static void levelComplete(int _index)
	{
		indexLevel = _index;
	}

	public static float CTimeScale {
		get { return cTimeScale; }
		set { 
			if (value < 0.0f)
				cTimeScale = 0.0f;
			else
				cTimeScale = value;
		}
	}

	public static float CTimeScale2 {
		get { return cTimeScale2; }
		set { 
			if (value < 0.0f)
				cTimeScale2 = 0.0f;
			else
				cTimeScale2 = value;
		}
	}
	public void DestroyGame()
	{
		Destroy (gameObject);
	}
}
