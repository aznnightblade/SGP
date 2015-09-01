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
	bool hasDLLs = false;
	DLLColor.Color prevColor = DLLColor.Color.BLUE;
	DLLColor.Color nextColor = DLLColor.Color.RED;
	[SerializeField]
	bool hasNegationBoots = false;
	[SerializeField]
	bool isHovering = false;
	[SerializeField]
	float hoverTime = 5.0f;
	float hoverTimer = 5.0f;
	[SerializeField]
	Breakpoint breakpoint = null;
	[SerializeField]
	int multithreadLevel = 1;
	[SerializeField]
	float invulTimePerDamage = 0.1f;
	float invulTimer = 0.0f;
	public int newGame = 1;
    public Text healthText;
    public Image visualHealth;
    public float healthspeed;
    public Text experience;
    public Text gold;
	// Use this for initialization
	void Start () {
       
        if (newGame==1)
        {
            currHealth = maxHealth = initialHealth + healthPerEndurance * endurance;
        } 
		critChance = initialCrit + critPerLuck * luck;
		currWeapon = weapons [0];


        for (int i = 0; i < SoundManager.instance.PlayerSoundeffects.Count; i++)
        {
            SoundManager.instance.PlayerSoundeffects[i].volume = PlayerPrefs.GetFloat("SFX") / 100f;
        }
        for (int i = 0; i < SoundManager.instance.WeaponSoundeffects.Count; i++)
        {
            SoundManager.instance.WeaponSoundeffects[i].volume = PlayerPrefs.GetFloat("SFX") / 100f;
        }
	}
	
	// Update is called once per frame
	void Update () {
		if (invulTimer >= 0.0f) {
			invulTimer -= Time.deltaTime;

			if (invulTimer < 0.0f)
				invulTimer = 0.0f;
		}
        HandleHealth();
        if (currHealth <= 0)
        {
            SoundManager.instance.PlayerSoundeffects[5].Play();
            currHealth = maxHealth;
            Application.LoadLevel("Hubworld");
        }

		if (isHovering) {
			hoverTimer -= Time.deltaTime * GameManager.CTimeScale2;

			if (hoverTimer <= 0.0f) {
				hoverTimer = 0.0f;

				isHovering = false;
			} else if (hoverTimer < hoverTime) {
				hoverTimer += Time.deltaTime * GameManager.CTimeScale2;

				if (hoverTimer > hoverTime)
					hoverTimer = hoverTime;
			}
		}
        experience.text = "Current Experience: " + EXP;
        gold.text = "Current Credits: " + Money;
	}

	public void SetPosition (Vector3 position)
	{
		transform.position = position;
	}

	public void DamagePlayer (int damageTaken) {
		// If the player is not in invulnerability state, deal damage to the player.
		if (invulTimer <= 0.0f) {
			currHealth -= damageTaken;
            SoundManager.instance.PlayerSoundeffects[4].Play();

			invulTimer = invulTimePerDamage * damageTaken;
		}
	}

	public Weapon CurrWeapon { get { return currWeapon; } }
	public Weapon[] Weapons { get { return weapons; } }
	public bool HasDLLs {
		get { return hasDLLs; }
		set { hasDLLs = value; }
	}
	public bool HasNegationBoots {
		get { return hasNegationBoots; }
		set { hasNegationBoots = value; }
	}
	public DLLColor.Color PrevColor {
		get { return prevColor; }
		set { prevColor = value; }
	}
	public DLLColor.Color NextColor {
		get { return nextColor; }
		set { nextColor = value; }
	}
	public Breakpoint Breakpoint { get { return breakpoint; } }
	public int MultithreadLevel {
		get { return multithreadLevel; }
		set { multithreadLevel = value; }
	}
	public Transform ShotLocation { get { return shotLocation; } }

	public bool IsHovering {
		get { return isHovering; }
		set { isHovering = value; }
	}

    private void HandleHealth()
    {
        healthText.text = "HP: " + currHealth;

        float currentValue = MapValues(currHealth, 0, maxHealth, 0, 1);

        visualHealth.fillAmount = currHealth / maxHealth;

        if (currHealth > maxHealth / 2) //more than 50% hp
        {
            visualHealth.color = new Color32((byte)MapValues(currHealth, maxHealth / 2, maxHealth, 255, 0), 255, 0, 255);
        }
        else //less than 50% hp
        {
            visualHealth.color = new Color32(255, (byte)MapValues(currHealth, 0, maxHealth / 2, 0, 255), 0, 255);
        }
    }
    private float MapValues(float x, float inMin, float inMax, float outMin, float outMax)
    {
        return (x - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;

    }
}
