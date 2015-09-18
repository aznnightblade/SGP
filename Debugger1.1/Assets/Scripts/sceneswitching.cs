using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class sceneswitching : MonoBehaviour {
    public GameObject Panel1;
    public GameObject Panel2;
    bool changescene = false;
    public string next;
	// Use this for initialization
	void Start () {
        Panel1.SetActive(true);
        Panel2.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if ((InputManager.instance.GetButtonDown("Submit") || InputManager.instance.GetButtonDown("Fire1")) && changescene == true)
        {
            PlayerPrefs.SetString("Nextscene", next);
            Application.LoadLevel("Loadingscreen");
        }
		if ((InputManager.instance.GetButtonDown("Submit") || InputManager.instance.GetButtonDown("Fire1")))
        {
            Panel1.SetActive(false);
            Panel2.SetActive(true);
            changescene = true;
			GameManager.instance.Initizialize();
        }
      
	}
}
