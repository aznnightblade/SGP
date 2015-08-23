using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

	[SerializeField]
	protected GameObject bullet = null;
	[SerializeField]
	protected Statistics owner = null;
	[SerializeField]
	protected float Velocity = 0.0f;
	[SerializeField]
	protected float initialDamage = 0.0f;
	[SerializeField]
	protected float damagePerStrength = 0.0f;
	[SerializeField]
	protected DLLColor.Color color = DLLColor.Color.NEUTRAL;

	[SerializeField]
	float heatGenerated = 0.0f;
	[SerializeField]
	float overheatLevel = 0.0f;
	[SerializeField]
	float heatPerShot = 0.0f;
	[SerializeField]
	float heatLossPerSecond = 0.0f;

	[SerializeField]
	float chargeScale = 1.0f;
	[SerializeField]
	float maxChargeScale = 2.0f;
	[SerializeField]
	float chargePerTick = 0.25f;
	[SerializeField]
	int ticksPerSecond = 16;
	float chargeDelay = 0.0f;
	float shotDelay = 0.0f;
	[SerializeField]
	protected float initialShotDelay = 0;
	[SerializeField]
	protected float reducedDelayPerAgility = 0;

	Vector3 direction = Vector3.zero;
	Vector3 ownerMoveDirection = Vector3.zero;
	[SerializeField]
	protected float maxDistance = 0.0f;
	protected Vector3 startLocation = Vector3.zero;

	Vector3 directionVelocity = Vector3.zero;

	bool onCooldown = false;

	// Use this for initialization
	void Start () {
		float rot = (transform.rotation.eulerAngles.y + 90.0f) * (Mathf.PI / 180.0f);
		
		maxDistance = owner.InitialShotDistance + owner.ShotDistancePerDexerity * owner.Dexterity;
		direction = new Vector3 (-Mathf.Cos (rot), 0, Mathf.Sin (rot));
		
		directionVelocity = direction * Velocity;

		if (directionVelocity.x > 0 && ownerMoveDirection.x > 0 || directionVelocity.x < 0 && ownerMoveDirection.x < 0)
			directionVelocity.x += ownerMoveDirection.x;

		if (directionVelocity.z > 0 && ownerMoveDirection.z > 0 || directionVelocity.z < 0 && ownerMoveDirection.z < 0)
			directionVelocity.z += ownerMoveDirection.z;

		gameObject.GetComponent<Rigidbody> ().velocity = directionVelocity;
		startLocation = transform.position;

		switch ((int)color) {
		case 0:
			gameObject.GetComponentInChildren<SpriteRenderer> ().material.color = Color.white;
			break;
		case 1:
			gameObject.GetComponentInChildren<SpriteRenderer> ().material.color = Color.red;
			break;
		case 2:
			gameObject.GetComponentInChildren<SpriteRenderer> ().material.color = Color.green;
			break;
		case 3:
			gameObject.GetComponentInChildren<SpriteRenderer> ().material.color = Color.blue;
			break;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector3.Distance (startLocation, transform.position) >= maxDistance) {
			Destroy(gameObject);
		}

		if (gameObject.tag == "Enemy Bullet")
			gameObject.GetComponent<Rigidbody> ().velocity = directionVelocity * GameManager.CTimeScale;
	}

	protected int WeaknessCheck(DLLColor.Color bullet, DLLColor.Color target) {
		if (bullet == DLLColor.Color.NEUTRAL || target == DLLColor.Color.NEUTRAL || bullet == target)
			return 0;
		
		if (bullet == DLLColor.Color.RED && target == DLLColor.Color.GREEN ||
		    bullet == DLLColor.Color.GREEN && target == DLLColor.Color.BLUE ||
		    bullet == DLLColor.Color.BLUE && target == DLLColor.Color.RED)
			return 1;
		
		return -1;
	}

	public void ProduceWeaponInfo() {

	}

	public Statistics Owner {
		get { return owner; }
		set { owner = value; }
	}
	public Vector3 OwnerMoveDirection {
		get { return ownerMoveDirection; }
		set { ownerMoveDirection = value; }
	}
	public float HeatGenerated { 
		get { return heatGenerated; }
		set { heatGenerated = value; }
	}

	public float OverheatLevel { get { return overheatLevel; } }
	public float HeatPerShot { get { return heatPerShot; } }
	public float HeatLossPerSecond { get { return heatLossPerSecond; } }
	public float ChargeScale { 
		get { return chargeScale; } 
		set { chargeScale = value; }
	}
	public float MaxChargeScale { get { return maxChargeScale; } }
	public float ChargePerTick { get { return chargePerTick; } }
	public float DelayTime { get { return 1.0f / ticksPerSecond; } }
	public float ChargeDelay {
		get { return chargeDelay; }
		set { chargeDelay = value; }
	}
	public float ShotDelay {
		get { return shotDelay; }
		set { shotDelay = value; }
	}
	public float InitialShotDelay { get { return initialShotDelay; } }
	public float ShotDelayReductionPerAgility { get { return reducedDelayPerAgility; } }
	public bool OnCooldown { 
		get { return onCooldown; } 
		set { onCooldown = value; }
	}
	public DLLColor.Color CurrColor { 
		get { return color; }
		set { color = value; }
	}
}
