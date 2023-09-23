using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    public Slider musicVolumeSlider;
    public Slider soundsVolumeSlider;

    public Toggle fullscreenToggle;


    public void Start()
    {
        if (PlayerPrefs.HasKey("isFullscreen"))
        {
            if(PlayerPrefs.GetInt("isFullscreen") == 1)
            {
                Screen.fullScreen = true;
            }
            else
            {
                Screen.fullScreen = false;
            }
        }
        LoadVisuals();
    }

    public void Open()
    {
        LoadVisuals();
    }

    private void LoadVisuals()
    {
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            musicVolumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
            audioMixer.SetFloat("musicVolume", PlayerPrefs.GetFloat("musicVolume"));
        }
        if (PlayerPrefs.HasKey("soundsVolume"))
        {
            soundsVolumeSlider.value = PlayerPrefs.GetFloat("soundsVolume");
            PlayerPrefs.SetFloat("soundsVolume", soundsVolumeSlider.value);
        }

        fullscreenToggle.isOn = Screen.fullScreen;
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("musicVolume", volume);
        PlayerPrefs.SetFloat("musicVolume", volume);
    }

    public void SetSoundsVolume(float volume)
    {
        audioMixer.SetFloat("soundsVolume", volume);
        PlayerPrefs.SetFloat("soundsVolume", volume);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        if (isFullscreen)
            PlayerPrefs.SetInt("isFullscreen", 1);
        else
            PlayerPrefs.SetInt("isFullscreen", 0);
    }

    public void Close()
    {
        PlayerPrefs.Save();
    }
}
