using UnityEngine;
using System.Collections;

public class BasicRanged : Statistics {

	NavMeshAgent agent = null;
	Transform target = null;
	
	[SerializeField]
	Transform bullet = null;

	float delayTimer = 0.0f;
	[SerializeField]
	float maximumShotDistance = 5.0f;

	// Use this for initialization
	void Start () {
		currHealth = maxHealth = initialHealth + healthPerEndurance * endurance;
		critChance = initialCrit + critPerLuck * luck;

		agent = gameObject.GetComponent<NavMeshAgent> ();
		target = GameObject.FindGameObjectWithTag ("Player").transform;
		agent.SetDestination (target.position);
	}
	
	// Update is called once per frame
	void Update () {
		agent.SetDestination (target.position);

		RechargeShields ();

		if (GameManager.CTimeScale == 0.0f) {
			agent.velocity = Vector3.zero;
			agent.updateRotation = false;
		}

		if (GameManager.CTimeScale > 0.0f && !agent.updateRotation) {
			agent.updateRotation = true;
		}

		if (Vector3.Distance (transform.position, target.position) <= maximumShotDistance && delayTimer <= 0.0f) {
            SoundManager.instance.EnemySoundeffects[1].Play();
            FireBullet();

			delayTimer = shotDelay;
		}

		if (delayTimer > 0.0f) {
			delayTimer -= Time.deltaTime * GameManager.CTimeScale;

			if( delayTimer < 0.0f)
				delayTimer = 0.0f;
		}

		if (currHealth <= 0.0f)
			DestroyObject ();
	}

	void FireBullet () {
		Vector3 pos = transform.position;
		Vector3 direction = pos - target.position;
		float rot = -((Mathf.Atan2(direction.z, direction.x) * 180 / Mathf.PI) + 90.0f);

		GameObject newBullet = (Instantiate (bullet, pos, Quaternion.Euler(0, rot, 0)) as Transform).gameObject;
		//GameObject newBullet = Bullet.gameObject;
		newBullet.tag = ("Enemy Bullet");
		newBullet.GetComponent<Weapon> ().Owner = this;
		newBullet.GetComponent<Weapon> ().OwnerMoveDirection = agent.velocity;
	}
}
