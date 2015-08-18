using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	public static GameManager instance;
	public static int indexLevel = 1;
	public SaveData playersGame;
	static Player player = null;

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
	}
	// Use this for initialization
	public static void ExitScenes()
	{
		player = GameObject.FindGameObjectWithTag ("Player").GetComponentInChildren<Player> ();
	}
	public static void LoadScene(Player _player)
	{
		_player = player;
	}
	
	public static void levelComplete(int _index)
	{
		indexLevel = _index;
	}
}
