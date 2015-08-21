using UnityEngine;
using System.Collections;
using UnityEngine.UI;
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
    float maxTimer;
    public Image _breakpoint;
    public Image timer;
    public GameObject timer_background;
    public GameObject freezetint;
	// Update is called once per frame

 
    void Update () {
		if (activeTimer > 0.0f && GameManager.CTimeScale > 0.0f) {
			GameManager.CTimeScale = 0.0f;
			isActive = true;
            timer_background.SetActive(true);
            freezetint.SetActive(true);
		} else if (activeTimer == 0.0f && GameManager.CTimeScale == 0.0f) {
			GameManager.CTimeScale = 1.0f;
			isActive = false;
            timer_background.SetActive(false);
            freezetint.SetActive(false);
		}

		if (activeTimer > 0.0f) {
			activeTimer -= Time.deltaTime;

			if (activeTimer < 0.0f)
				activeTimer = 0.0f;
		}

        _breakpoint.fillAmount = fillAmount/maxFill;
        timer.fillAmount = activeTimer/maxTimer;
	}

	public void FireBreakpoint () {
		if (fillAmount == maxFill) {
			maxTimer = activeTimer = duration;
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
