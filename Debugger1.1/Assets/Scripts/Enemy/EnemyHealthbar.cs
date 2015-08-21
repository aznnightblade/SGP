using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyHealthbar : MonoBehaviour {

	[SerializeField]
	Image Health = null;
	[SerializeField]
	Statistics target = null;

	// Use this for initialization
	void Start () {
		Health.color = Color.green;

		Health.enabled = false;
	}

	void Update () {
		Vector3 pos = target.transform.position;
		pos.z += 1.0f;

		transform.position = pos;
	}

	public void UpdateFillAmount () {
		Health.fillAmount = (float)target.CurrHealth / target.MaxHealth;

		if (Health.fillAmount < 1.0f)
			Health.enabled = true;
		else
			Health.enabled = false;
	
		if (Health.fillAmount > 0.5f)
			Health.color = Color.Lerp (Color.yellow, Color.green, Health.fillAmount);
		else
			Health.color = Color.Lerp (Color.red, Color.yellow, Health.fillAmount);
	}
}
