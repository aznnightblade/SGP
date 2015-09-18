using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class HUDImage : MonoBehaviour {

    Player player;
    public Texture[] weapon = null;
    public RawImage DLLcolor = null;
    public RawImage[] weapons = null;
    public Texture[] Friends = null;
    public RawImage CurrFriend = null;
    public Text boottimer = null;
    public RawImage companion = null;
    public Texture defaultcompanion = null;
    public Texture nocompanion = null;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Changeweapon(int index)
    {
        weapons[1].texture = weapon[index];
        if (index - 1 < 0)
        {
            weapons[0].texture = weapon[player.Weapons.Count - 1];
        }
        else
            weapons[0].texture = weapon[index-1];
        if (index + 1 > player.Weapons.Count - 1)
        {
            weapons[2].texture = weapon[0];
        }
        else
            weapons[2].texture = weapon[index + 1];
    }
    public void Changecolor(DLLColor.Color dllcolor)
    {
        switch(dllcolor)
        {
            case DLLColor.Color.NEUTRAL:
                DLLcolor.color = Color.white;
                break;
            case DLLColor.Color.BLUE:
                DLLcolor.color = Color.blue;
                break;
            case DLLColor.Color.RED:
                DLLcolor.color = Color.red;
                break;
            case DLLColor.Color.GREEN:
                DLLcolor.color = Color.green;
                break;
                
        }
    }
    public void Changefriend(GameObject friend)
    {
        if (friend.name.Contains("Pusher"))
        {
            CurrFriend.texture = Friends[7];
        }
        if (friend.name.Contains("Basic Melee"))
        {
            CurrFriend.texture = Friends[0];
        }
        if (friend.name.Contains("Basic Range"))
        {
            CurrFriend.texture = Friends[1];
        }
        if (friend.name.Contains("Severe Ranged"))
        {
            CurrFriend.texture = Friends[2];
        }
        if (friend.name.Contains("Shielded Pusher"))
        {
            CurrFriend.texture = Friends[3];
        }
        if (friend.name.Contains("moderate ranged")) 
        {
            CurrFriend.texture = Friends[4];
        }
        if (friend.name.Contains("firewaller"))
        {
            CurrFriend.texture = Friends[5];
        }
        if (friend.name.Contains("Destructor"))
        {
            CurrFriend.texture = Friends[6];
        }
        switch(friend.GetComponent<Enemy>().CurrColor)
        {
            case DLLColor.Color.NEUTRAL:
                CurrFriend.color = Color.white;
                break;
            case DLLColor.Color.BLUE:
                CurrFriend.color = Color.blue;
                break;
            case DLLColor.Color.GREEN:
                CurrFriend.color = Color.green;
                break;
            case DLLColor.Color.RED:
                CurrFriend.color = Color.red;
                break;
        }
    }

    public void Changecompanion(Player.COMPANIONS comp)
    {
        switch (comp)
        {
            case Player.COMPANIONS.Bill:
                companion.texture = defaultcompanion;
                companion.color = Color.green;
                break;
            case Player.COMPANIONS.Chris:
                companion.texture = defaultcompanion;
                companion.color = Color.blue;
                break;
            case Player.COMPANIONS.Daemon:
                companion.texture = defaultcompanion;
                companion.color = Color.red;
                break;
            case Player.COMPANIONS.LeRoc:
                companion.texture = defaultcompanion;
                companion.color = new Color(128,0,128);
                break;
            case Player.COMPANIONS.Bahim:
                companion.texture = defaultcompanion;
                companion.color = Color.white;
                break;
            case Player.COMPANIONS.None:
                companion.texture = nocompanion;
                break;
           
        }
    }
}
