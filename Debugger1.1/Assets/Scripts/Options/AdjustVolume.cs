using UnityEngine;
using UnityEngine.Audio;
using System.Collections;
using UnityEngine.UI;

public class AdjustVolume : MonoBehaviour
{
    public float sfxvalue;
    public float musicvalue;
    public Slider sfxslider;
    public Slider musicslider;
    public Text sfxtext;
    public Text musictext;
    float timer = 0;
    void Start()
    {
        sfxslider.value = PlayerPrefs.GetFloat("SFX");
        musicslider.value = PlayerPrefs.GetFloat("Music");
    }

    void Update()
    {
        sfxtext.text = sfxslider.value.ToString();
        musictext.text = musicslider.value.ToString();
        timer += Time.deltaTime;

        if (timer>2.0f)
        {
            SoundManager.instance.Music[0].Stop();
            timer = 0;
        }
    }
    public void AdjustSFX()
    {
       

        for (int i = 0; i <  SoundManager.instance.PlayerSoundeffects.Count; i++)
        {
            SoundManager.instance.PlayerSoundeffects[i].volume = sfxslider.value / 100f;
        }
        for (int i = 0; i < SoundManager.instance.EnemySoundeffects.Count; i++)
        {
            SoundManager.instance.EnemySoundeffects[i].volume = sfxslider.value / 100f;
        }
        for (int i = 0; i < SoundManager.instance.BossSoundeffects.Count; i++)
        {
            SoundManager.instance.BossSoundeffects[i].volume = sfxslider.value / 100f;
        }
        for (int i = 0; i < SoundManager.instance.WeaponSoundeffects.Count; i++)
        {
            SoundManager.instance.WeaponSoundeffects[i].volume = sfxslider.value / 100f;
        }
        for (int i = 0; i < SoundManager.instance.MiscSoundeffects.Count; i++)
        {
            SoundManager.instance.MiscSoundeffects[i].volume = sfxslider.value / 100f;
        }

        PlayerPrefs.SetFloat("SFX", sfxslider.value);
        if (!SoundManager.instance.PlayerSoundeffects[0].isPlaying)
        {
            SoundManager.instance.PlayerSoundeffects[0].Play();
        }
    }
    public void AdjustMusic()
    {

        for (int i = 0; i < SoundManager.instance.Music.Count; i++)
        {
            SoundManager.instance.Music[i].volume = musicslider.value / 100f;
        }
        for (int i = 0; i < SoundManager.instance.BossMusic.Count; i++)
        {
            SoundManager.instance.BossMusic[i].volume = musicslider.value / 100f;
        }
        PlayerPrefs.SetFloat("Music", musicslider.value);

        if (!SoundManager.instance.Music[0].isPlaying)
            SoundManager.instance.Music[0].Play();
        
       

    }
}
