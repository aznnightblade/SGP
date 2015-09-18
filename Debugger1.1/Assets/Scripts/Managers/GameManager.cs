using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	public static GameManager instance;
	public static int difficulty = 1;
	public static string nextlevelname;
	public static int indexLevel = 2;
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
    public static float breakpointduration = 3;
	public static Vector2 ScreenResolution = Vector2.zero;
    public static bool deletefile = false;
	void Awake(){
		if (instance == null) {
			instance = this;
            
			DontDestroyOnLoad (gameObject);
            if (FindObjectOfType<Player>()==null)
            {
                return;
            }
			if (FindObjectOfType<Player> ().newGame == 1 && first) {
                data = new SaveData();
                data.Agility= GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().Agility = 1;
                data.Strength = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().Strength = 1;
                data.Endurance =GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().Endurance = 1;
                data.Luck=GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().Luck = 1;
                data.Intelligence= GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().Intelligence = 1;
                data.Dexterity = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().Dexterity = 1;
                data.CurrentHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CurrHealth = 50;
                data.MaxHealth=GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().MaxHealth = 50;
                data.Credits = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().Money = 100;
                data.XP = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().EXP = 0;
                data.newGame = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().newGame = 0;
                data.Multithread = 1;
                data.breakpointlvl = 3.0f;
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
        if (GameObject.FindGameObjectWithTag("Player") == null)
        {
            return;
        }
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
             if (GameObject.FindGameObjectWithTag("Player")==null)
             {
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
             Chargeshot = _data.ChargeShot;
             Friendshot = _data.Friendwpn;
             RecursiveShot = _data.Waveshot;
             GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().Breakpoint.Duration = _data.breakpointlvl;
             GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().MultithreadLevel = _data.Multithread;
             if (Friendshot==1)
             {
                 player.Weapons.Add(gameObject.GetComponentInChildren<Weapons>().Friendshot);
             }
             if (RecursiveShot == 1)
             {
                 player.Weapons.Add(gameObject.GetComponentInChildren<Weapons>().Waveshot);
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
