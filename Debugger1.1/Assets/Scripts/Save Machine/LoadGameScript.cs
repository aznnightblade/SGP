using UnityEngine;
using System.Collections;

public class LoadGameScript : MonoBehaviour {

    public string NextScene;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnClick()
    {
        if (gameObject.tag=="Load1")
        {
            SaveData temp = SaveData.Load("test");
            GameManager.instance.LoadPlayerstatsScene(temp);
            GameManager.instance.NextScene();
            GameManager.loadfirst = false;
            PlayerPrefs.SetString("Nextscene", NextScene);
            Application.LoadLevel(NextScene);

        }
        else if (gameObject.tag == "Load2")
        {
            SaveData temp = SaveData.Load("test2");
            GameManager.instance.LoadPlayerstatsScene(temp);
            GameManager.instance.NextScene();
            GameManager.loadfirst = false;
            PlayerPrefs.SetString("Nextscene", NextScene);
            Application.LoadLevel(NextScene);
        }
        else if (gameObject.tag == "Load3")
        {
            SaveData temp = SaveData.Load("test3");
            GameManager.instance.LoadPlayerstatsScene(temp);
            GameManager.instance.NextScene();
            GameManager.loadfirst = false;
            PlayerPrefs.SetString("Nextscene", NextScene);
            Application.LoadLevel(NextScene);
        }
       
    }
}
