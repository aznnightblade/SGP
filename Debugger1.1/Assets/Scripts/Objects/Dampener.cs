using UnityEngine;
using System.Collections;

public class Dampener : Statistics {
	public bool Toggle = false;
    [SerializeField]
    Sprite[] images = null;

    public override void Damage (int damageTaken, Transform bullet)
    {
		if (currHealth > 0) {

            if (currHealth<= maxHealth*.75f)
            {
                gameObject.GetComponentInChildren<SpriteRenderer> ().sprite = images [1];
            }
            if (currHealth <= maxHealth * .50f)
            {
                gameObject.GetComponentInChildren<SpriteRenderer>().sprite = images[2];
            }
            if (currHealth <= maxHealth * .25f)
            {
                gameObject.GetComponentInChildren<SpriteRenderer>().sprite = images[3];
            }
			Weapon Bullet = bullet.GetComponent<Weapon> ();
			if (Bullet.ChargeScale < 1.25f) {
				SoundManager.instance.MiscSoundeffects [6].Play ();
				damageTaken = 0;
			} else {
				SoundManager.instance.MiscSoundeffects [5].Play ();
				currHealth -= damageTaken;
				EnemyHealthbar healthbar = transform.parent.GetComponentInChildren<EnemyHealthbar> ();

				if (healthbar != null)
					healthbar.UpdateFillAmount ();
			}
			if (currHealth <= 0) {
                gameObject.GetComponentInChildren<SpriteRenderer>().color = new Color(0, 0, 0, 0);
				Toggle = true;
			}
		}
    }
    public void Repair()
    {
        currHealth = maxHealth;
        gameObject.GetComponentInChildren<SpriteRenderer>().color = new Color(1, 1, 1, 1);
		gameObject.GetComponentInChildren<SpriteRenderer> ().sprite = images [0];
        Toggle = false;
    }

	void OnCollisionEnter(Collision col) {
		if (col.gameObject.tag == "Player Bullet") {
			if (currHealth <= 0) {
                SoundManager.instance.MiscSoundeffects[7].Play();
				Toggle = true;
			}
		}
	}

}