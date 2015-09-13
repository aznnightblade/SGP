using UnityEngine;
using System.Collections;

public class Statistics : MonoBehaviour {
	
	[SerializeField]
	protected int currHealth = 0;
	[SerializeField]
	protected int maxHealth = 0;
	[SerializeField]
	protected int shield = 0;
	[SerializeField]
	protected int maxShield = 0;
	[SerializeField]
	protected float hitRegenTimer = 2.0f;
	[SerializeField]
	protected float hitTimer = 0.0f;
	[SerializeField]
	protected int strength = 0;
	[SerializeField]
	protected float initialDamage = 0.0f;
	[SerializeField]
	protected float damagePerStrength = 0.0f;
	[SerializeField]
	protected int endurance = 0;
	[SerializeField]
	protected int initialHealth = 0;
	[SerializeField]
	protected int healthPerEndurance = 0;
	[SerializeField]
	protected float defense = 0;
	[SerializeField]
	protected int initialDefense = 0;
	[SerializeField]
	protected int defensePerEndurance = 0;
	[SerializeField]
	protected int agility = 0;
	[SerializeField]
	protected float shotDelay = 0;
	[SerializeField]
	protected int dexterity = 0;
	[SerializeField]
	protected float initialShotDistance = 0;
	[SerializeField]
	protected float increaseDistancePerDexerity = 0;
	[SerializeField]
	protected int intelligence = 0;
	[SerializeField]
	protected float initialCooldownReduction = 0;
	[SerializeField]
	protected float reductionPerIntelligence = 0;
	[SerializeField]
	protected int luck = 0;
	[SerializeField]
	protected float critChance = 0;
	[SerializeField]
	protected float initialCrit = 0;
	[SerializeField]
	protected float critPerLuck = 0;
	[SerializeField]
	protected DLLColor.Color color = DLLColor.Color.NEUTRAL;
	[SerializeField]
	int money = 0;
	[SerializeField]
	int exp = 0;
    
	public Statistics () {

	}

	public Statistics (Statistics obj) {
		strength = obj.strength;
		agility = obj.agility;
		endurance = obj.endurance;
		dexterity = obj.dexterity;
		intelligence = obj.intelligence;
		luck = obj.luck;
		initialDamage = obj.initialDamage;
		damagePerStrength = obj.damagePerStrength;
		initialShotDistance = obj.initialShotDistance;
		increaseDistancePerDexerity = obj.increaseDistancePerDexerity;
		initialCrit = obj.initialCrit;
		critPerLuck = obj.critPerLuck;
	}

    public SpriteRenderer sprite;
	public virtual void Damage (int damageTaken, Transform bullet) {
		if (shield > 0 && bullet.gameObject.layer != LayerMask.NameToLayer("Waveshot Bullet")) {
			if (shield >= damageTaken)
				shield -= damageTaken;
			else {
				damageTaken -= shield;
				shield = 0;
				currHealth -= damageTaken;
				transform.parent.GetComponentInChildren<EnemyHealthbar> ().UpdateFillAmount ();
			}
			
			transform.parent.GetComponentInChildren<EnemyShieldbar> ().UpdateFillAmount ();
		} else {
            if (gameObject.name=="Bill"||gameObject.name=="Cris"||gameObject.name=="Le Roc"||gameObject.name=="Damon")
            {
                SoundManager.instance.CompanionSFX[0].Play();
            }
			currHealth -= damageTaken;
			EnemyHealthbar healthbar = transform.parent.GetComponentInChildren<EnemyHealthbar> ();
			
			if (healthbar != null)
				healthbar.UpdateFillAmount();
		}

		if (currHealth <= 0) {
			if (gameObject.name=="Corruption") {
				SoundManager.instance.MiscSoundeffects[4].Play();
			} else
				SoundManager.instance.EnemySoundeffects[6].Play();

			Destroy (gameObject);
		}
	}

	public virtual void UpdateStats () {
		if (gameObject.tag == "Player") {
			maxHealth = currHealth = initialHealth + healthPerEndurance * endurance;
			critChance = initialCrit + critPerLuck * luck;
			defense = initialDefense + defensePerEndurance * endurance;
		} else {
			maxHealth = currHealth = initialHealth * GameManager.difficulty + healthPerEndurance * endurance;
			critChance = initialCrit * GameManager.difficulty + critPerLuck * luck;
			defense = initialDefense * GameManager.difficulty + defensePerEndurance * endurance;
		}
	}

	public void RechargeShields() {
		if (maxShield > 0) {
			if (shield < maxShield && hitTimer <= 0.0f) {
				shield += Mathf.CeilToInt(maxShield * 0.1f * Time.deltaTime * GameManager.CTimeScale);

				if (shield > maxShield)
					shield = maxShield;
			
				EnemyShieldbar shieldBar = transform.parent.GetComponentInChildren<EnemyShieldbar> ();

				if (shieldBar != null)
					shieldBar.UpdateFillAmount ();
			}
		}

		if (hitTimer >= 0.0f) {
			hitTimer -= Time.deltaTime * GameManager.CTimeScale;

			if (hitTimer <= 0.0f)
				hitTimer = 0.0f;
		}
	}

	public virtual void DestroyObject() {
		Destroy (transform.parent.gameObject);
	}

	public int Strength { 
		get { return strength; }
		set { strength = value; }
	}
	public int Agility {
		get { return agility; }
		set { agility = value; }
	}
	public int Endurance { 
		get { return endurance; }
		set { endurance = value; }
	}
	public int Intelligence { 
		get { return intelligence; }
		set { intelligence = value; }
	}
	public int Dexterity { 
		get { return dexterity; }
		set { dexterity = value; }
	}
	public int Luck { 
		get { return luck; }
		set { luck = value; }
	}
	public DLLColor.Color CurrColor {
		get { return color; }
		set { color = value; }
	}
	public int CurrHealth { 
		get { return currHealth; }
		set { currHealth = value; }
	}
	public int MaxHealth { 
		get { return maxHealth; }
		set { maxHealth = value; }
	}
	public int Shield {
		get { return shield; }
		set { shield = value; }
	}
	public int MaxShield {
		get { return maxShield; }
		set { maxShield = value; }
	}
	public float InitialDamage {
		get { return initialDamage; }
		set { initialDamage = value; }
	}
	public float DamagePerStrength {
		get { return damagePerStrength; }
		set { damagePerStrength = value; }
	}
	public float Defense {
		get { return defense; }
		set { defense = value; }
	}
	public float CritChance {
		get { return critChance; }
		set { critChance = value; }
	}
	public float InitialShotDistance { get { return initialShotDistance; } }
	public float ShotDistancePerDexerity { get { return increaseDistancePerDexerity; } }
	public int EXP { 
		get { return exp; }
		set { exp = value; }
	}
	public int Money { 
		get { return money; }
		set { money = value; }
	}
}
