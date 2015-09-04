using UnityEngine;
using System.Collections;

public class Cheats : MonoBehaviour {
    private string[] godmode;
    private string[] breakpoint;
    private string[] noheatgain;
    private string[] bacardi;
    private int index;
    private int bonus;
    private int wpn;
    private int heat;
    public GameObject bonusstage;
    Player player;
    bool godmodeon = false;
    bool breakpointon = false;
    bool noheat = false;
    bool bonuslevel = false;
    void Start()
    {
        bonusstage.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        
        // Code is "idkfa", user needs to input this in the right order
        godmode = new string[] { "g", "o", "d", "m", "o", "d", "e" };
        breakpoint = new string[] { "t","i","m","e" };
        noheatgain = new string[] { "h", "e", "a", "t" };
        bacardi = new string[] { "1", "5", "1" };

        index = 0;
        wpn = 0;
        heat = 0;
        bonus = 0;
    }

    void Update() {
     // Check if any key is pressed
     if (Input.anyKeyDown) {
         // Check if the next key in the code is pressed
         if (Input.GetKeyDown(godmode[index]))
         {
             // Add 1 to index to check the next key in the code
             index++;
         }
         else if (wpn < 4 && Input.GetKeyDown(breakpoint[wpn]))
         {
             // Add 1 to index to check the next key in the code
             wpn++;
         }
         else if (heat < 4 && Input.GetKeyDown(noheatgain[heat]))
         {
             heat++;
         }
         else if (bonus < 3 && Input.GetKeyDown(bacardi[bonus]))
         {
             bonus++;
         }
         else
         {
             index = 0;
             wpn = 0;
             bonus = 0;
             heat = 0;
         }
     }
     if (godmodeon==true)
     {
        player.CurrHealth = player.MaxHealth;
     }
     if (breakpointon==true)
     {
         player.Breakpoint.AddFill();
     }
     if (noheat==true)
     {
         player.CurrWeapon.HeatGenerated = 0;
     }
     if (bonuslevel==true)
     {
         bonusstage.SetActive(true);
     }
     // If index reaches the length of the cheatCode string, 
     // the entire code was correctly entered
     if (index == godmode.Length) {
         Debug.Log("cheat success");
         index = 0;
         godmodeon = !godmodeon;
         // Cheat code successfully inputted!
         // Unlock crazy cheat code stuff
     }
     else if (wpn == breakpoint.Length)
     {
         Debug.Log("cheat success");
         index = 0;
         breakpointon = !breakpointon;
     }
     else if (heat == noheatgain.Length)
     {
         Debug.Log("cheat success");
         index = 0;
         noheat = !noheat;
     }
     else if (bonus == bacardi.Length)
     {
         Debug.Log("cheat success");
         index = 0;
         bonusstage.SetActive(true);
     }
    }
}
