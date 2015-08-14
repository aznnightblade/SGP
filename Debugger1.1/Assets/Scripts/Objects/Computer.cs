using UnityEngine;
using System.Collections;

public class Computer : MonoBehaviour {

    bool triggerActive = false;
    public string Loadinglevel;
	// Use this for initialization
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
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
        if (triggerActive==true)
        {
            PlayerPrefs.SetString("Nextscene", Loadinglevel);
            Application.LoadLevel("Loadingscreen");
        }
    }
}
