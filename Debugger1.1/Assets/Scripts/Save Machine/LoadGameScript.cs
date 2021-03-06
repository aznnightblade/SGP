﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class LoadGameScript : MonoBehaviour {

    public string NextScene;
    public Text Load1 = null;
    public Text Load2 = null;
    public Text Load3 = null;
	public Font font ;
	// Use this for initialization
	void Start () {
		Load1.font = font;
		Load2.font = font;
		Load3.font = font;
	}
	
	// Update is called once per frame
	void Update () {
        if (PlayerPrefs.HasKey("test" + "Name"))
        {
            Load1.text = "Current Cleared Stage: " + PlayerPrefs.GetInt("test" + "LEVEL").ToString() + "\nCurrent XP: " + PlayerPrefs.GetInt("test" + "XP").ToString() + " \nCurrent Credits: " + PlayerPrefs.GetInt("test" + "CREDITS").ToString();
        }
        else
        {
            Load1.text = "No File";
        }
        if (PlayerPrefs.HasKey("test2" + "Name"))
        {
            Load2.text = "Current Cleared Stage: " + PlayerPrefs.GetInt("test2" + "LEVEL").ToString() + "\nCurrent XP: " + PlayerPrefs.GetInt("test2" + "XP").ToString() + " \nCurrent Credits: " + PlayerPrefs.GetInt("test2" + "CREDITS").ToString();
        }
        else
        {
            Load2.text = "No File";
        }
        if (PlayerPrefs.HasKey("test3" + "Name"))
        {
            Load3.text = "Current Cleared Stage: " + PlayerPrefs.GetInt("test3" + "LEVEL") + "\nCurrent XP: " + PlayerPrefs.GetInt("test3" + "XP").ToString() + " \nCurrent Credits: " + PlayerPrefs.GetInt("test3" + "CREDITS").ToString();
        }
        else
        {
            Load3.text = "No File";
        }
	}

    public void OnClick()
    {
        if (gameObject.tag=="Load1")
        {
            GameManager.data = SaveData.Load("test");
            GameManager.instance.NextScene();
            GameManager.loadfirst = false;
            PlayerPrefs.SetString("Nextscene", NextScene);
            Application.LoadLevel(NextScene);

        }
        else if (gameObject.tag == "Load2")
        {
            GameManager.data = SaveData.Load("test2");
            GameManager.instance.NextScene();
            GameManager.loadfirst = false;
            PlayerPrefs.SetString("Nextscene", NextScene);
            Application.LoadLevel(NextScene);
        }
        else if (gameObject.tag == "Load3")
        {
            GameManager.data = SaveData.Load("test3");
            GameManager.instance.NextScene();
            GameManager.loadfirst = false;
            PlayerPrefs.SetString("Nextscene", NextScene);
            Application.LoadLevel(NextScene);
        }
       
    }
}
