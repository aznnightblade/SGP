using UnityEngine;
using System.Collections;

public class Breakpoint : MonoBehaviour {

	[SerializeField]
	float fillAmount = 0.0f;
	[SerializeField]
	float fillPerKill = 0.5f;
	[SerializeField]
	float increasePerIntelligence = 0.1f;
	[SerializeField]
	float maxFill = 3.0f;
	[SerializeField]
	float duration = 3.0f;
	[SerializeField]
	float activeTimer = 0.0f;
	[SerializeField]
	bool isActive = false;
	
	// Update is called once per frame
	void Update () {
		if (activeTimer > 0.0f && GameManager.CTimeScale > 0.0f) {
			GameManager.CTimeScale = 0.0f;
			isActive = true;
		} else if (activeTimer == 0.0f && GameManager.CTimeScale == 0.0f) {
			GameManager.CTimeScale = 1.0f;
			isActive = false;
		}

		if (activeTimer > 0.0f) {
			activeTimer -= Time.deltaTime;

			if (activeTimer < 0.0f)
				activeTimer = 0.0f;
		}
	}

	public void FireBreakpoint () {
		if (fillAmount == maxFill) {
			activeTimer = duration;
			fillAmount = 0.0f;
		}
	}

	public void AddFill () {
		fillAmount += fillPerKill + increasePerIntelligence * GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ().Intelligence;

		if (fillAmount > maxFill)
			fillAmount = maxFill;
	}

	public float FillAmount { get { return fillAmount; } }
	public float MaxFill { get { return maxFill; } }
	public float Duration {
		get { return duration; }
		set { duration = value; }
	}
	public bool IsActive { get { return isActive; } }
}
