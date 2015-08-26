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
	protected float acceleration = 0;
	[SerializeField]
	protected float velocity = 0;
	[SerializeField]
	protected float maxVelocity = 0;
	[SerializeField]
	protected float maxTurnRadius = 0;
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
	protected float initialDropRate = 0;
	[SerializeField]
	protected float critPerLuck = 0;
	[SerializeField]
	protected float dropRatePerLuck = 0;
	[SerializeField]
	protected DLLColor.Color color = DLLColor.Color.NEUTRAL;
	[SerializeField]
	int money = 0;
	[SerializeField]
	int exp = 0;
    public SoundManager sounds;
	protected bool IsDisabled = false;
	protected bool IsSlowed = false;
	protected bool CanMove = true;

	public virtual void MoveObject() {

	}

	public virtual void PrimaryAction() {
	
	}

	public virtual void SecondaryAction() {

	}

	public virtual void OnCollisionEnter(Collision col){
	
	}

	public void UpdateStats () {
		maxHealth = currHealth = initialHealth + healthPerEndurance * endurance;
		critChance = initialCrit + critPerLuck * luck;
		defense = initialDefense + defensePerEndurance * endurance;
	}

	// Only call this for enemies
	public virtual void Damage (int damageTaken) {
		if (shield > 0) {
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
			currHealth -= damageTaken;
			EnemyHealthbar healthbar = transform.parent.GetComponentInChildren<EnemyHealthbar> ();

			if (healthbar != null)
				healthbar.UpdateFillAmount();
		}

		if (currHealth <= 0) {
            if (gameObject.name=="Corruption")
            {
                sounds.MiscSoundeffects[4].Play();
            }
            else
            sounds.EnemySoundeffects[6].Play();
			DestroyObject();
		}

		hitTimer = hitRegenTimer;
	}

	public void RechargeShields() {
		if (maxShield > 0) {
			if (shield < maxShield && hitTimer <= 0.0f) {
				shield += Mathf.CeilToInt(maxShield * 0.1f * Time.deltaTime * GameManager.CTimeScale);

				if (shield > maxShield)
					shield = maxShield;

				if (gameObject.tag == "Enemy")
					transform.parent.GetComponentInChildren<EnemyShieldbar> ().UpdateFillAmount ();
			}
		}

		if (hitTimer >= 0.0f) {
			hitTimer -= Time.deltaTime * GameManager.CTimeScale;

			if (hitTimer <= 0.0f)
				hitTimer = 0.0f;
		}
	}

	public void DestroyObject() {
		Breakpoint breakpoint = GameObject.FindGameObjectWithTag ("Player").GetComponent<Breakpoint> ();
		breakpoint.AddFill ();

		if (gameObject.name == "FireWaller")
			gameObject.GetComponent<FireWaller> ().RemoveShields ();

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
	public DLLColor.Color Color {
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
