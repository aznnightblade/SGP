using UnityEngine;
using System.Collections;

public class Dampener : Statistics {
	public bool Toggle = false;
    [SerializeField]
    Sprite[] images = null;
	[SerializeField]
	void Update() {
		
	}
    public override void Damage (int damageTaken, Transform bullet)
    {
        Weapon Bullet = bullet.GetComponent<Weapon>();
        if (Bullet.ChargeScale < 1.25f)
        {
            SoundManager.instance.MiscSoundeffects[6].Play();
            damageTaken = 0;
        }
        else
        {
            SoundManager.instance.MiscSoundeffects[5].Play();
            currHealth -= damageTaken;
            EnemyHealthbar healthbar = transform.parent.GetComponentInChildren<EnemyHealthbar>();

            if (healthbar != null)
                healthbar.UpdateFillAmount();
        }
        if (currHealth < 0)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = images[1];
            Toggle = true;
        }
    }
    public void Repair()
    {
        currHealth = maxHealth;
        Toggle = false;
    }
	public override void OnCollisionEnter(Collision col) {

		if (col.gameObject.tag == "Player Bullet") {
			if (currHealth <= 0) {
                SoundManager.instance.MiscSoundeffects[7].Play();
				Toggle = true;
			}


		}
	}

}