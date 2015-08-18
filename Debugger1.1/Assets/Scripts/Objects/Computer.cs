using UnityEngine;
using System.Collections;

public class Computer : MonoBehaviour {

    bool triggerActive = false;
    public string Loadinglevel;
	public int levelNumber;
	// Use this for initialization
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player" )
        {
            triggerActive = true;
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.tag== "Player")
        {
            triggerActive = false;
        }
    }
    void Update()
    {
        if (triggerActive == true && Input.GetButtonDown("Submit"))
        {
            PlayerPrefs.SetString("Nextscene", Loadinglevel);
            Application.LoadLevel("Loadingscreen");
        }
    }
}
