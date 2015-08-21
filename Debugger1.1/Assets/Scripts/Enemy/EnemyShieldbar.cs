using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyShieldbar : MonoBehaviour {

	[SerializeField]
	Image Shield = null;
	[SerializeField]
	Statistics target = null;

	// Use this for initialization
	void Start () {
		Shield.color = Color.blue;

		if (target.MaxShield > 0)
			Shield.enabled = true;
		else
			Shield.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = target.transform.position;
		pos.z += 1.25f;
		
		transform.position = pos;
	}

	public void UpdateFillAmount () {
		Shield.fillAmount = (float)target.Shield / target.MaxShield;
		
		if (Shield.fillAmount > 0.0f)
			Shield.enabled = true;
		else
			Shield.enabled = false;
	}
}
