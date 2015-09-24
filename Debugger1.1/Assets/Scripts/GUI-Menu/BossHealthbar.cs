using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BossHealthbar : MonoBehaviour {

    public Statistics boss;
    public Image healthbar;
    public GameObject bar;
	// Use this for initialization
	void Start () {

        healthbar.color = Color.green;
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void Healthbar()
    {
        healthbar.fillAmount = (float)boss.CurrHealth / boss.MaxHealth;
        if (healthbar.fillAmount > 0.5f)
            healthbar.color = Color.Lerp(Color.yellow, Color.green, healthbar.fillAmount);
        else
            healthbar.color = Color.Lerp(Color.red, Color.yellow, healthbar.fillAmount);
        if (boss.CurrHealth<=0)
        {
            bar.SetActive(false);
        }
    }
}
