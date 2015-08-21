using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Player : Statistics {

	[SerializeField]
	Transform shotLocation = null;
	[SerializeField]
	Weapon currWeapon = null;
	[SerializeField]
	Weapon[] weapons = null;
	[SerializeField]
	Breakpoint breakpoint = null;
	[SerializeField]
	int multithreadLevel = 1;
	[SerializeField]
	float invulTimePerDamage = 0.1f;
	float invulTimer = 0.0f;

    public Text healthText;
    public Image visualHealth;
    public float healthspeed;
	// Use this for initialization
	void Start () {
		currHealth = maxHealth = initialHealth + healthPerEndurance * endurance;
		critChance = initialCrit + critPerLuck * luck;
		currWeapon = weapons [0];
	}
	
	// Update is called once per frame
	void Update () {
		if (invulTimer >= 0.0f) {
			invulTimer -= Time.deltaTime;

			if (invulTimer < 0.0f)
				invulTimer = 0.0f;
		}
        HandleHealth();
	}

	public void DamagePlayer (int damageTaken) {
		// If the player is not in invulnerability state, deal damage to the player.
		if (invulTimer <= 0.0f) {
			currHealth -= damageTaken;

			if (currHealth <= 0) {

			}

			invulTimer = invulTimePerDamage * damageTaken;
		}
	}

	public Weapon CurrWeapon { get { return currWeapon; } }
	public Weapon[] Weapons { get { return weapons; } }
	public Breakpoint Breakpoint { get { return breakpoint; } }
	public int MultithreadLevel {
		get { return multithreadLevel; }
		set { multithreadLevel = value; }
	}
	public Transform ShotLocation { get { return shotLocation; } }

    private void HandleHealth()
    {
        healthText.text = "HP: " + CurrHealth;

        float currentValue = MapValues(CurrHealth, 0, MaxHealth, 0, 1);

        visualHealth.fillAmount = CurrHealth / MaxHealth;

        if (CurrHealth > MaxHealth / 2) //more than 50% hp
        {
            visualHealth.color = new Color32((byte)MapValues(CurrHealth, MaxHealth / 2, MaxHealth, 255, 0), 255, 0, 255);
        }
        else //less than 50% hp
        {
            visualHealth.color = new Color32(255, (byte)MapValues(CurrHealth, 0, MaxHealth / 2, 0, 255), 0, 255);
        }
    }
    private float MapValues(float x, float inMin, float inMax, float outMin, float outMax)
    {
        return (x - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;

    }
}
