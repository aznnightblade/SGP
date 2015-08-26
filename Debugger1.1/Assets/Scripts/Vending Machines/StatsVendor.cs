using UnityEngine;
using System.Collections;

public class StatsVendor : MonoBehaviour {

    
    bool triggerActive = false;
    public GameObject Panel;
    public GameObject Strength;
    public GameObject Dexterity;
    public GameObject Intelligence;
    public GameObject Luck;
    public GameObject Endurance;
    public GameObject Agility;
    public SoundManager sounds;
    Transform player;
    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        sounds = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
    }

    // Update is called once per frame
    void Update()
    {

        if (triggerActive == true && InputManager.instance.GetButtonDown("Submit"))
        {
            Panel.SetActive(true);
            Strength.SetActive(true);
            Dexterity.SetActive(true);
            Intelligence.SetActive(true);
            Luck.SetActive(true);
            Endurance.SetActive(true);
            Agility.SetActive(true);
            player.GetComponentInParent<PlayerController>().enabled = false;
            player.GetComponentInParent<Rigidbody>().velocity = Vector3.zero;
            player.GetComponentInParent<Rigidbody>().freezeRotation = true;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            triggerActive = true;
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            triggerActive = false;

        }
    }

    public void Onclick()
    {
        sounds.MiscSoundeffects[8].Play();
        Panel.SetActive(false);
        Strength.SetActive(false);
        Dexterity.SetActive(false);
        Intelligence.SetActive(false);
        Luck.SetActive(false);
        Endurance.SetActive(false);
        Agility.SetActive(false);
        player.GetComponentInParent<PlayerController>().enabled = true;
    }
}