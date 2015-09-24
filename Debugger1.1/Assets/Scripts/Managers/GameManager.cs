using UnityEngine;
using System;
using System.Collections;

public class GameManager : MonoBehaviour {
	public static GameManager instance;
	public static int difficulty = 1;
	public static string nextlevelname;
	public static int indexLevel = 3;
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
	//static bool first = true;
	public static bool quit = false;
	public static bool loadfirst = true;
    public static int Chargeshot = 0;
    public static int DLLShot = 0;
    public static int Friendshot = 0;
    public static int RecursiveShot = 0;
    public static int breakptlevel = 1;
    public static int negationbootlevel = 1;
    public static int multithreadlevel = 1;
    public static int Negationboots = 0;
    public static float breakpointduration = 3;
	public static Vector2 ScreenResolution = Vector2.zero;
    public static bool deletefile = false;
	public static bool InMenu = false;
	void Awake(){
		if (instance == null) {
			instance = this;

            data = new SaveData();
            
			DontDestroyOnLoad (gameObject);

			ScreenResolution = new Vector2 (Screen.width, Screen.height);

			if (FindObjectOfType<Player>()==null) {
				return;
			}
			if(back) {
			    GameObject.FindGameObjectWithTag("Player Controller").GetComponent<Rigidbody> ().position = lastPosition;
			}
		} else if (instance != this) {
			Destroy (gameObject);
		}
	}

    public void Initizialize()
    {
		Player player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ();
		data.Agility = player.Agility = 1;
		data.Strength = player.Strength = 1;
		data.Endurance = player.Endurance = 1;
		data.Luck = player.Luck = 1;
		data.Intelligence = player.Intelligence = 1;
		data.Dexterity = player.Dexterity = 1;
		data.CurrentHealth = player.CurrHealth = 50;
		data.MaxHealth = player.MaxHealth = 50;
		data.Credits = player.Money = 1000;
		data.XP = player.EXP = 0;
		data.newGame = player.newGame = 0;
        data.Multithread = 1;
        data.breakpointduration = 3.0f;
        Chargeshot = 0;
        DLLShot = 0;
        Negationboots = 0;
    }

	void Update () {
		if (InMenu) {
			if (InputManager.instance.GetButtonUp("Cancel")) {
				MenuInput menu = FindObjectOfType<MenuInput> ();

				if (menu == null)
					InMenu = false;
			}
		}
	}

	public void NextScene()
	{
        if (GameObject.FindGameObjectWithTag("Player") == null)
        {
            return;
        }
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player> ();
        data.Agility = player.Agility;
        data.Strength = player.Strength;
        data.Endurance = player.Endurance;
        data.Luck = player.Luck;
        data.Intelligence = player.Intelligence;
        data.Dexterity = player.Dexterity;
        data.CurrentHealth = player.CurrHealth;
        data.MaxHealth = player.MaxHealth;
        data.Credits = player.Money;
        data.XP = player.EXP;
        data.newGame = player.newGame;
        data.ChargeShot = Chargeshot;
        data.DLLShot = DLLShot;
        data.Friendwpn = Friendshot;
        data.Waveshot = RecursiveShot;
        data.Companion = player.SelectedCompanion;

        for (int index = 0; index < player.Companions.Length; index++)
            data.companions[index] = player.Companions[index];
	}
	 public static void LoadScene()
	{
       
		if (loadfirst == true) {
			loadfirst = false;
			return;
		}
		if (back)
			back = false;
        if (GameObject.FindGameObjectWithTag("Player") == null)
        {
            return;
        }
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
        
        instance.LoadPlayerstatsScene(data);
	}
     public void LoadPlayerstatsScene(SaveData _data)
     {
         if (loadfirst==true)
         {
             loadfirst = false;
			return;
         }
         if (_data!=null)
         {
			if (GameObject.FindGameObjectWithTag("Player")==null) {
				return;
			}

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
			player.HasDLLs = Convert.ToBoolean(DLLShot);
			Chargeshot = _data.ChargeShot;
			player.HasChargeShot = Convert.ToBoolean(Chargeshot);
			Negationboots = _data.NegationBoots;
			player.HasNegationBoots = Convert.ToBoolean(Negationboots);
			Friendshot = _data.Friendwpn;
			RecursiveShot = _data.Waveshot;
			player.Breakpoint.Duration = _data.breakpointduration;
			player.MultithreadLevel = _data.Multithread;
			GameManager.breakptlevel = _data.breakpointlevel;
			player.SelectedCompanion = data.Companion;
			
			for (int index = 0; index < player.Companions.Length; index++)
				player.Companions[index] = data.companions[index];
			
			if (Friendshot==1) {
				player.Weapons.Add(FindObjectOfType<Weapons> ().Friendshot);
			}
			if (RecursiveShot == 1) {
				player.Weapons.Add(FindObjectOfType<Weapons> ().Waveshot);
			}
         }
         

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
