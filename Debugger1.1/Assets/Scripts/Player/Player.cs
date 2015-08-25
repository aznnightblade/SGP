﻿using UnityEngine;
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
	// Use this for initialization
	void Start () {
        newGame = GameManager.NewGamefile;
        if (newGame==1)
        {
            currHealth = maxHealth = initialHealth + healthPerEndurance * endurance;
        } 
		critChance = initialCrit + critPerLuck * luck;
		currWeapon = weapons [0];
        sounds = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();


        for (int i = 0; i < sounds.PlayerSoundeffects.Count; i++)
        {
            sounds.PlayerSoundeffects[i].volume = PlayerPrefs.GetFloat("SFX")/ 100f;
        }
        for (int i = 0; i < sounds.WeaponSoundeffects.Count; i++)
        {
            sounds.WeaponSoundeffects[i].volume = PlayerPrefs.GetFloat("SFX") / 100f;
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

	}

	public void SetPosition (Vector3 position)
	{
		transform.position = position;
	}

	public void DamagePlayer (int damageTaken) {
		// If the player is not in invulnerability state, deal damage to the player.
		if (invulTimer <= 0.0f) {
			currHealth -= damageTaken;
            sounds.PlayerSoundeffects[4].Play();

			if (currHealth <= 0) {
                sounds.PlayerSoundeffects[5].Play();
			}

			invulTimer = invulTimePerDamage * damageTaken;
		}
	}

	public Weapon CurrWeapon { get { return currWeapon; } }
	public Weapon[] Weapons { get { return weapons; } }
	public bool HasDLLs {
		get { return hasDLLs; }
		set { hasDLLs = value; }
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
