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
	public static Vector2 ScreenResolution = Vector2.zero;
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

			ScreenResolution = new Vector2 (Screen.currentResolution.width, Screen.currentResolution.height);

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
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		data.Agility = player.Agility;
        data.Strength = player.Strength;
        data.Endurance=  player.Endurance;
        data.Luck = player.Luck;
        data.Intelligence = player.Intelligence; 
        data.Dexterity= player.Dexterity;   
        data.CurrentHealth = player.CurrHealth;   
        data.MaxHealth = player.MaxHealth;    
        data.Credits = player.Money;       
        data.XP = player.EXP;
        data.newGame = player.newGame;      
        data.DLLShot = DLLShot;
        data.ChargeShot = Chargeshot;       
	}
     public void LoadPlayerstatsScene(SaveData _data)
     {
         player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
         player.Agility = _data.Agility;
         player.Strength = _data.Strength;
         player.Endurance = _data.Endurance;
         player.Luck = _data.Endurance;
         player.Intelligence = _data.Intelligence;
         player.Dexterity = _data.Dexterity;
         player.CurrHealth = _data.CurrentHealth;
         player.MaxHealth = _data.MaxHealth;
         player.Money = _data.Credits;
         player.EXP = _data.XP;
         player.newGame = _data.newGame;
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
