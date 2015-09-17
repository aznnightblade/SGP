using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Player : Statistics {

	public enum COMPANIONS { Bill, Chris, Daemon, LeRoc, Bahim, None };

	[SerializeField]
	protected float acceleration = 0;
	[SerializeField]
	protected float velocity = 0;
	[SerializeField]
	protected float maxVelocity = 0;

	[SerializeField]
	Transform shotLocation = null;
	[SerializeField]
	Weapon currWeapon = null;
	int currWeaponCounter = 0;
	[SerializeField]
	List<Weapon> weapons = new List<Weapon> ();

	[SerializeField]
	bool hasDLLs = false;
	DLLColor.Color prevColor = DLLColor.Color.BLUE;
	DLLColor.Color nextColor = DLLColor.Color.RED;
	[SerializeField]
	bool hasChargeShot = false;
	[SerializeField]
	bool hasNegationBoots = false;
	[SerializeField]
	bool isHovering = false;
	[SerializeField]
	float hoverTime = 5.0f;
	float hoverTimer = 5.0f;
	[SerializeField]
	Transform friend = null;
	[SerializeField]
	Breakpoint breakpoint = null;
	[SerializeField]
	int multithreadLevel = 1;

	[SerializeField]
	float invulTimePerDamage = 0.1f;
	float invulTimer = 0.0f;

	[SerializeField]
	float totalDeathTime = 1.5f;
	float deathTime = 0.0f;
	Animator anim = null;

	[SerializeField]
	int[] companions = new int[5];
	[SerializeField]
	GameObject[] CompanionObjects = null;
	[SerializeField]
	COMPANIONS selectedCompanion = COMPANIONS.None;
	GameObject activeCompanion = null;
	[SerializeField]
	bool enableCompanion = false;
	bool isCompanionActive = false;

	public int newGame = 1;
    public Text healthText;
    public Image visualHealth;
    public float healthspeed;
    public Text experience;
    public Text gold;

	// Use this for initialization
	void Start () {
        sprite = GetComponentInChildren<SpriteRenderer>();
        if (newGame==1)
        {
            currHealth = maxHealth = initialHealth + healthPerEndurance * endurance;
        } 
		critChance = initialCrit + critPerLuck * luck;
		currWeapon = weapons [0];

		if (enableCompanion && selectedCompanion != COMPANIONS.None) {
			Vector3 pos = transform.position;
			pos.z -= 2.0f;
			activeCompanion = (GameObject) Instantiate (CompanionObjects[(int)selectedCompanion], pos, Quaternion.identity);
			isCompanionActive = true;
		}

		anim = gameObject.GetComponentInChildren<Animator> ();

        if (SoundManager.instance != null)
        {
            for (int i = 0; i < SoundManager.instance.PlayerSoundeffects.Count; i++)
            {
                SoundManager.instance.PlayerSoundeffects[i].volume = PlayerPrefs.GetFloat("SFX") / 100f;
            }
            for (int i = 0; i < SoundManager.instance.WeaponSoundeffects.Count; i++)
            {
                SoundManager.instance.WeaponSoundeffects[i].volume = PlayerPrefs.GetFloat("SFX") / 100f;
            }
        }

        Powerups();
	}
	
	// Update is called once per frame
	void Update () {
		if (invulTimer >= 0.0f) {
			invulTimer -= Time.deltaTime;

			if (invulTimer < 0.0f)
				invulTimer = 0.0f;
		}
        HandleHealth();

		Death ();

		if (isHovering) {
			hoverTimer -= Time.deltaTime * GameManager.CTimeScale2;

			if (hoverTimer <= 0.0f) {
				hoverTimer = 0.0f;

				isHovering = false;
			} 
		} else if (hoverTimer < hoverTime) {
			hoverTimer += Time.deltaTime * GameManager.CTimeScale2;
			
			if (hoverTimer > hoverTime)
				hoverTimer = hoverTime;
		}
        experience.text = "Current Experience: " + EXP;
        gold.text = "Current Credits: " + Money;
	}

	public void SetPosition (Vector3 position)
	{
		transform.position = position;
	}

	public void DamagePlayer (int damageTaken, Transform Entity = null) {
		// If the player is not in invulnerability state, deal damage to the player.
		if (invulTimer <= 0.0f) {
            StartCoroutine(collideFlash());
			currHealth -= damageTaken;
            SoundManager.instance.PlayerSoundeffects[4].Play();

			invulTimer = invulTimePerDamage * damageTaken;
		}
	}

	void Death () {
		if (currHealth <= 0) {
			anim.SetBool("Death", true);
			deathTime += Time.deltaTime;

			if (deathTime >= totalDeathTime) {
				SoundManager.instance.PlayerSoundeffects[5].Play();
				currHealth = maxHealth;
				Application.LoadLevel("Hubworld");
			}
		}
	}

	public float Velocity { 
		get { return velocity; }
		set{ velocity = value; }
	}
	public float MaxVelocity { 
		get { return maxVelocity; }
		set { maxVelocity = value; }
	}
	public float Acceleration { 
		get { return acceleration; }
		set { acceleration = value; }
	}
	public Weapon CurrWeapon { 
		get { return currWeapon; }
		set { currWeapon = value; }
	}
	public int CurrWeaponCounter {
		get { return currWeaponCounter; }
		set { currWeaponCounter = value; }
	}
	public List<Weapon> Weapons { get { return weapons; } }
	public bool HasDLLs {
		get { return hasDLLs; }
		set { hasDLLs = value; }
	}
	public bool HasChargeShot {
		get { return hasChargeShot; }
		set { hasChargeShot = value; }
	}
	public bool HasNegationBoots {
		get { return hasNegationBoots; }
		set { hasNegationBoots = value; }
	}
	public float HoverTime {
		get { return hoverTime; }
		set { hoverTime = value; }
	}
	public DLLColor.Color PrevColor {
		get { return prevColor; }
		set { prevColor = value; }
	}
	public DLLColor.Color NextColor {
		get { return nextColor; }
		set { nextColor = value; }
	}
	public Transform Friend {
		get { return friend; }
		set { friend = value; }
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

	public int[] Companions { get { return companions; } }
	public COMPANIONS SelectedCompanion {
		get { return selectedCompanion; }
		set { selectedCompanion = value; }
	}
	public GameObject ActiveCompanion {
		get { return activeCompanion; }
		set { activeCompanion = value; }
	}
	public bool IsCompanionActive {
		get { return isCompanionActive; }
		set { isCompanionActive = value; }
	}

    private void HandleHealth()
    {
        healthText.text = "HP: " + currHealth;

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
    IEnumerator collideFlash()
    {
        Material m = sprite.material;
        Color32 c = sprite.material.color;
        sprite.material.color = new Color(1, 1, 1, 0);
        yield return new WaitForSeconds(0.1f);
        sprite.material = m;
        sprite.material.color = c;
    }
    void Powerups()
    {
        if (GameManager.indexLevel >= 2)
        {
            GameManager.DLLShot = 1;
        }
        if (GameManager.indexLevel >= 3)
        {
            GameManager.Chargeshot = 1;
        }
        if (GameManager.DLLShot == 1)
        {
            hasDLLs = true;
        }
        if (GameManager.Chargeshot == 1)
        {
            hasChargeShot = true;
        }
        if (GameManager.Friendshot==1)
        {
           
        }
    }
}
