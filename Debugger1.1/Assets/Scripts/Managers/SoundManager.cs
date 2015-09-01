using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class SoundManager : MonoBehaviour {
    public static SoundManager instance { get; private set; }
    public List<AudioSource> PlayerSoundeffects;
    public List<AudioSource> Music;
    public List<AudioSource> BossMusic;
    public List<AudioSource> EnemySoundeffects;
    public List<AudioSource> BossSoundeffects;
    public List<AudioSource> WeaponSoundeffects;
	public List<AudioSource> MiscSoundeffects;
    public List<AudioSource> MenuSFX;
	// Use this for initialization

	void Start () {

        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        for (int i = 0; i < PlayerSoundeffects.Count; i++)
        {
            PlayerSoundeffects[i].volume = PlayerPrefs.GetFloat("SFX") / 100f;
        }
        for (int i = 0; i < EnemySoundeffects.Count; i++)
        {
            EnemySoundeffects[i].volume = PlayerPrefs.GetFloat("SFX") / 100f;
        }
        for (int i = 0; i < BossSoundeffects.Count; i++)
        {
            BossSoundeffects[i].volume = PlayerPrefs.GetFloat("SFX") / 100f;
        }
        for (int i = 0; i < WeaponSoundeffects.Count; i++)
        {
            WeaponSoundeffects[i].volume = PlayerPrefs.GetFloat("SFX") / 100f;
        }
        for (int i = 0; i <MiscSoundeffects.Count; i++)
        {
            MiscSoundeffects[i].volume = PlayerPrefs.GetFloat("SFX") / 100f;
        }

        for (int i = 0; i < Music.Count; i++)
        {
            Music[i].volume = PlayerPrefs.GetFloat("Music") / 100f;
        }
        for (int i = 0; i < BossMusic.Count; i++)
        {
            BossMusic[i].volume = PlayerPrefs.GetFloat("Music") / 100f;
        }
	}
	
	// Update is called once per frame
	void Update () {
	}
}
