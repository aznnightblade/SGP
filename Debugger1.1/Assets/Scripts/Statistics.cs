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
	protected int endurance = 0;
	[SerializeField]
	protected int initialHealth = 0;
	[SerializeField]
	protected int healthPerEndurance = 0;
	[SerializeField]
	protected int defense = 0;
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

	public void DestroyObject() {
		Destroy (gameObject);
	}

	public int Strength { get; set; }
	public int Agility { get; set; }
	public int Endurance { get; set; }
	public int Intelligence { get; set; }
	public int Dexterity { get; set; }
	public int Luck { get; set; }
	public DLLColor.Color Color {
		get { return color; }
		set { color = value; }
	}
	public int CurrHealth { get; set; }
	public int MaxHealth { get; set; }
	public int Shield { get; set; }
	public int MaxShield { get; set; }
	public int Defense { get; set; }
	public int CritChance { get; set; }
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
}
