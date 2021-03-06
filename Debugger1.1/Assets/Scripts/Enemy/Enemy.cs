﻿using UnityEngine;
using System.Collections;

public class Enemy : Statistics {

	public enum Mode { Attack, Idle, Patrolling, Friendly, Deactivated, BossRoom };
	protected NavMeshAgent agent = null;
	protected Transform target = null;

	Vector3 direction = Vector3.zero;
	Vector3 previousLookDir = Vector3.zero;
	
	[SerializeField]
	protected Mode currMode = Mode.Attack;
	[SerializeField]
	protected Transform[] Waypoints = null;
	int waypointCounter = 0;
	[SerializeField]
	protected float detectRange = 8.0f;
	[SerializeField]
	protected float maxDistance = 15.0f;

	[SerializeField]
	protected float initialDropRate = 0;
	[SerializeField]
	protected float dropRatePerLuck = 0;

	[SerializeField]
	protected bool IsCapturable = false;

    public Animator anim;
    float timer = 0;
    bool deathbool;
    
	public override void UpdateStats () {
        sprite = GetComponentInChildren<SpriteRenderer>();
		maxHealth = currHealth = initialHealth * GameManager.difficulty + healthPerEndurance * endurance;
		critChance = initialCrit * GameManager.difficulty + critPerLuck * luck;
		defense = initialDefense * GameManager.difficulty + defensePerEndurance * endurance;

        anim = gameObject.GetComponentInChildren<Animator>();
		agent = gameObject.GetComponent<NavMeshAgent> ();

		if (currMode == Mode.Deactivated) {
			agent.updateRotation = false;
			agent.updatePosition = false;
			gameObject.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeAll;
		}

		if (currMode == Mode.Attack || currMode == Mode.BossRoom) {
			target = GameObject.FindGameObjectWithTag ("Player").transform;

			if (agent.isActiveAndEnabled)
				agent.destination = target.position;
		}

		if (currMode == Mode.Patrolling) {
			target = Waypoints[waypointCounter];
			agent.destination = target.position;
		}

		if (currMode != Mode.Friendly) {
			switch (color) {
			case DLLColor.Color.NEUTRAL:
				break;
			case DLLColor.Color.RED:
				gameObject.GetComponentInChildren<SpriteRenderer> ().material.color = Color.red;
				break;
			case DLLColor.Color.GREEN:
				gameObject.GetComponentInChildren<SpriteRenderer> ().material.color = Color.green;
				break;
			case DLLColor.Color.BLUE:
				gameObject.GetComponentInChildren<SpriteRenderer> ().material.color = Color.blue;
				break;
			}
		}
	}

	public virtual void UpdateWaypoints () {
		if (agent.remainingDistance < 1.0f) {
			if (waypointCounter >= Waypoints.Length - 1)
				waypointCounter = 0;
			else
				waypointCounter++;

			target = Waypoints[waypointCounter];
		}
	}

	public virtual void CheckForPlayer () {
		if (currMode != Mode.BossRoom && currMode != Mode.Deactivated) {
			Transform player = GameObject.FindGameObjectWithTag ("Player").transform;

			if (Vector3.Distance (transform.position, player.position) <= detectRange) {
				target = player;
				currMode = Mode.Attack;
			}
		}
	}

	public virtual void CheckForReset () {
		if (currMode != Mode.BossRoom && currMode != Mode.Deactivated) {
			Transform player = GameObject.FindGameObjectWithTag ("Player").transform;

			if (Vector3.Distance (transform.position, player.position) >= maxDistance) {
				if (Waypoints.Length > 0) {
					target = Waypoints [waypointCounter];
					currMode = Mode.Patrolling;
				} else {
					target = null;
					currMode = Mode.Idle;
					agent.destination = transform.position;
				}
			}
		}
	}

	public override void Damage (int damageTaken, Transform bullet) {
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
			currHealth -= damageTaken;
			EnemyHealthbar healthbar = transform.parent.GetComponentInChildren<EnemyHealthbar> ();
            StartCoroutine(collideFlash());
			if (healthbar != null)
				healthbar.UpdateFillAmount();
		}
		
		if (currHealth <= 0) {

			if (currMode != Mode.Friendly) {
				if (IsCapturable && bullet.name.Contains("Friend Shot")) {
					Player player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ();
                    GameObject.FindGameObjectWithTag("HUD").GetComponent<HUDImage>().Changefriend(transform.gameObject);
					if (player.Friend != null)
						Destroy (player.Friend.gameObject);

					target = null;
					Rigidbody rigid = gameObject.GetComponentInChildren<Rigidbody> ();
					rigid.constraints = (RigidbodyConstraints.FreezePositionY | 
					                     RigidbodyConstraints.FreezeRotationX |
					                     RigidbodyConstraints.FreezeRotationZ);
					rigid.drag = 0;
					rigid.angularDrag = 0;
					player.Friend = (Transform)Instantiate (transform.parent, transform.parent.position, Quaternion.identity);
					Enemy friendStats = player.Friend.GetComponentInChildren<Enemy> ();
					friendStats.CurrHealth = friendStats.MaxHealth;
					friendStats.CurrMode = Mode.Friendly;
					player.Friend.GetChild(0).tag = player.tag;
					player.Friend.GetChild (0).gameObject.layer = LayerMask.NameToLayer ("Player");
					player.Friend.gameObject.SetActive (false);
				}
			
				if (gameObject.name == "Joe") {
					GameManager.DLLShot = 1;
				}
			
				if (gameObject.name == "Justin") {
					GameManager.Chargeshot = 1;
				}
			
				if (gameObject.name == "Worm") {
					gameObject.GetComponent<Trojan> ().OnDeath ();
				}
			
				if (gameObject.name == "Destructor" && bullet.name != "Friend Shot(Clone)") {
					gameObject.GetComponent<Destructor> ().Detonate ();
				} else {
                    deathbool = true;
				}
			} else {
				PlayerController player = GameObject.FindGameObjectWithTag("Player Controller").GetComponent<PlayerController> ();
				player.ControlCounter = 0;
				player.PlayerControlledObjects[1] = null;
				Camera.main.GetComponent<CameraFollow> ().Target = GameObject.FindGameObjectWithTag("Player").transform;
				FindObjectOfType<HUDImage> ().Changefriend(null);
				
				Destroy(transform.parent.gameObject);
			}
		} 
		
		hitTimer = hitRegenTimer;
	}

	public override void DestroyObject() {
		if (currMode != Mode.Friendly) {
			Breakpoint breakpoint = GameObject.FindGameObjectWithTag ("Player").GetComponent<Breakpoint> ();
			breakpoint.AddFill ();
		
			if (gameObject.name == "FireWaller")
				gameObject.GetComponent<FireWaller> ().RemoveShields ();

			HealthDrops health = gameObject.GetComponent<HealthDrops> ();
			if(health != null)
				health.Healthdrop ();
		
			Destroy (transform.parent.gameObject);
		} else {
			Damage(9999, transform);
		}
	}

	protected void FaceMouse () {
		if (Time.timeScale > 0.0f) {
			if (InputManager.instance.UsingController == false) {
				previousLookDir = direction;
				direction = Camera.main.WorldToScreenPoint (transform.position) - Input.mousePosition;
			} else {
				previousLookDir = direction;
				direction = new Vector3 (InputManager.instance.GetAxisRaw ("Horizontal2"), InputManager.instance.GetAxisRaw ("Vertical2"), 0);
				
				if (direction == Vector3.zero)
					direction = previousLookDir;
			}
		}
		
		direction.Normalize ();
		float rot = (Mathf.Atan2 (-direction.y, direction.x) * 180 / Mathf.PI) - 90;
		transform.rotation = Quaternion.Euler (0, rot, 0);
	}

	protected void FacePlayer () {
		Vector3 direction = (transform.position - target.position).normalized;
		float rot = (Mathf.Atan2 (-direction.z, direction.x) * 180 / Mathf.PI) - 90;
		transform.rotation = Quaternion.Euler (0, rot, 0);
	}

	public Mode CurrMode {
		get { return currMode; }
		set { currMode = value; }
	}
    public virtual void Death()
    {
        if (deathbool==true)
        {
            currMode = Mode.Deactivated;

            timer += Time.deltaTime;
            if (timer >= .45f)
            {
                DestroyObject();
                timer = 0;
                GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().EXP += EXP;
                GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().Money += Money;
                anim.SetBool("Death", false);
                deathbool = false;
            }
			anim.SetBool("Death", true);
        }

    }
    public virtual void Attack()
    {

    }
    IEnumerator collideFlash()
    {
        Color c = sprite.material.color;
        sprite.material.color = new Color(1, 0, 0, 1);
        yield return new WaitForSeconds(0.1f);
        sprite.material.color = c;
    }

	public bool Deathbool { get { return deathbool; } }
}
