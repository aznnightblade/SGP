using UnityEngine;
using UnityEngine.Audio;
using System.Collections;
using UnityEngine.UI;

public class AdjustVolume : MonoBehaviour
{
    public SoundManager sounds;
    public float sfxvalue;
    public float musicvalue;
    public Slider sfxslider;
    public Slider musicslider;
    public Text sfxtext;
    public Text musictext;
    bool Play = false;
    bool PlayMusic = false;
    void Start()
    {
        sfxslider.value = PlayerPrefs.GetFloat("SFX");
        musicslider.value = PlayerPrefs.GetFloat("Music");
        sounds = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
    }

    void Update()
    {
        sfxtext.text = sfxslider.value.ToString();
        musictext.text = musicslider.value.ToString();
    }
    public void AdjustSFX()
    {
       

        for (int i = 0; i < sounds.PlayerSoundeffects.Count; i++)
        {
            sounds.PlayerSoundeffects[i].volume = sfxslider.value/100f;
        }
        for (int i = 0; i < sounds.EnemySoundeffects.Count; i++)
        {
            sounds.EnemySoundeffects[i].volume= sfxslider.value/100f;
        }
        for (int i = 0; i < sounds.BossSoundeffects.Count; i++)
        {
            sounds.BossSoundeffects[i].volume = sfxslider.value / 100f;
        }
        for (int i = 0; i < sounds.WeaponSoundeffects.Count; i++)
        {
            sounds.WeaponSoundeffects[i].volume = sfxslider.value / 100f;
        }
        for (int i = 0; i < sounds.MiscSoundeffects.Count; i++)
        {
            sounds.MiscSoundeffects[i].volume = sfxslider.value / 100f;
        }

        PlayerPrefs.SetFloat("SFX", sfxslider.value);
        if (!sounds.PlayerSoundeffects[0].isPlaying && Play==true)
            sounds.PlayerSoundeffects[0].Play();

        Play = true;
    }
    public void AdjustMusic()
    {
       
        for (int i = 0; i < sounds.Music.Count; i++)
        {
            sounds.Music[i].volume = musicslider.value / 100f;
        }
        for (int i = 0; i < sounds.BossMusic.Count; i++)
        {
            sounds.BossMusic[i].volume = musicslider.value / 100f;
        }
        PlayerPrefs.SetFloat("Music", musicslider.value);

        if (!sounds.Music[0].isPlaying)
            sounds.Music[0].Play();
    }
}
