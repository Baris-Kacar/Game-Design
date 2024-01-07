using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Volume_Settings : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    private void Start() {
        if (PlayerPrefs.HasKey("musicVolume")) {
            LoadVolume();
        } else {
            SetMusicVolume();
            SetSFXVolume();
        }
    }
    public void SetMusicVolume() {
        float musicvolume = musicSlider.value;
        myMixer.SetFloat("music", Mathf.Log10(musicvolume) * 20);
        PlayerPrefs.SetFloat("musicVolume", musicvolume);
    }
    public void SetSFXVolume() {
        float sfxvolume = sfxSlider.value;
        myMixer.SetFloat("sfx", Mathf.Log10(sfxvolume) * 20);
        PlayerPrefs.SetFloat("sfxVolume", sfxvolume);
    }
    public void LoadVolume() {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume");
        SetMusicVolume();
        SetSFXVolume();
    }
}
