using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

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
	
	Vector3 direction = Vector3.zero;
	[SerializeField]
	protected float maxDistance = 0.0f;
	protected Vector3 startLocation = Vector3.zero;

	// Use this for initialization
	void Start () {
		float rot = (transform.rotation.eulerAngles.y + 90.0f) * (Mathf.PI / 180.0f);
		
		maxDistance = owner.InitialShotDistance + owner.ShotDistancePerDexerity * owner.Dexterity;
		direction = new Vector3 (-Mathf.Cos (rot), 0, Mathf.Sin (rot));
		
		gameObject.GetComponent<Rigidbody> ().velocity = direction * (Velocity + owner.Velocity);
		startLocation = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector3.Distance (startLocation, transform.position) >= maxDistance) {
			Destroy(gameObject);
		}
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

	public Statistics Owner {
		get { return owner; }
		set { owner = value; }
	}
}
